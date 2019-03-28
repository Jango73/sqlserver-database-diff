
using System;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class TableContainer : ContainerBase
    {
        private List<Table> m_Tables;

        [NonSerialized]
        private Base m_Base;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableContainer"/> class.
        /// </summary>
        public TableContainer()
        {
            m_Tables = new List<Table>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableContainer"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public TableContainer(Base target)
            : this()
        {
            m_Base = target;
        }

        /// <summary>
        /// Gets or sets the tables.
        /// </summary>
        /// <value>The tables.</value>
        public List<Table> Tables
        {
            get { return m_Tables; }
            set { m_Tables = value; }
        }

        /// <summary>
        /// Computes a hash code for this object.
        /// </summary>
        /// <returns>
        /// Hash code.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = (result * 397) ^ ("TableContainer").ToString().GetHashCode();
                return result;
            }
        }

        /// <summary>
        /// Détermine si l'<see cref="T:System.Object"></see> spécifié est égal à l'<see cref="T:System.Object"></see> en cours.
        /// </summary>
        /// <param name="obj"><see cref="T:System.Object"></see> à comparer au <see cref="T:System.Object"></see> en cours.</param>
        /// <returns>
        /// true si l'<see cref="T:System.Object"></see> spécifié est égal à l'<see cref="T:System.Object"></see> en cours ; sinon, false.
        /// </returns>
        public override Boolean Equals(object obj)
        {
            return (GetHashCode() == obj.GetHashCode());
        }

        /// <summary>
        /// Gets the modified tables.
        /// </summary>
        /// <value>The modified tables.</value>
        [XmlIgnore]
        public List<Table> ModifiedTables
        {
            get
            {
                List<Table> ret = new List<Table>();

                foreach (Table tbl in m_Tables)
                {
                    if (tbl.Twin != null && (tbl.Qualifier == Modification.Modified || tbl.Twin.Qualifier == Modification.Modified))
                    {
                        ret.Add(tbl);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Gets the dropped tables.
        /// </summary>
        /// <value>The dropped tables.</value>
        [XmlIgnore]
        public List<Table> DroppedTables
        {
            get
            {
                List<Table> ret = new List<Table>();

                foreach (Table tbl in m_Tables)
                {
                    if (tbl.Qualifier == Modification.Deleted)
                    {
                        ret.Add(tbl);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Gets the created tables.
        /// </summary>
        /// <value>The created tables.</value>
        [XmlIgnore]
        public List<Table> CreatedTables
        {
            get
            {
                List<Table> ret = new List<Table>();

                foreach (Table tbl in m_Tables)
                {
                    if (tbl.Qualifier == Modification.Created)
                    {
                        ret.Add(tbl);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode Node = new TreeNode();

            Node.Text = "Tables";
            Node.Tag = this;

            if (Filter.Text != String.Empty) Node.Text += " (Filtered)";

            Base.SetNodeImageFromQualifier(Node, Qualifier, IsGhost);

            foreach (Table CurrentTable in Tables)
            {
                if
                (
                    (CurrentTable.Qualifier == Modification.None && Filter.HideUnmodified == false) ||
                    (CurrentTable.Qualifier == Modification.Modified && Filter.HideModified == false) ||
                    (CurrentTable.Qualifier == Modification.Created && Filter.HideCreated == false) ||
                    (CurrentTable.Qualifier == Modification.Deleted && Filter.HideDropped == false)
                )
                {
                    if (Filter.Text != String.Empty)
                    {
                        if (CurrentTable.Name.ToLower().Contains(Filter.Text.ToLower()))
                        {
                            Node.Nodes.Add(CurrentTable.toTreeNode());
                        }
                    }
                    else
                    {
                        Node.Nodes.Add(CurrentTable.toTreeNode());
                    }
                }
            }

            return Node;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(TableContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            foreach (Table src in Tables)
            {
                // progress.Value++;

                bool found = false;

                foreach (Table tgt in target.Tables)
                {
                    if (src.Name == tgt.Name && tgt.IsGhost == false)
                    {
                        Modification mod = src.qualifyVersus(tgt, dir);

                        if (Qualifier == Modification.None && mod != Modification.None)
                        {
                            Qualifier = Modification.Modified;
                        }

                        src.Twin = tgt;

                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    if (dir == Direction.Out)
                    {
                        src.Qualifier = Modification.Deleted;
                    }
                    else
                    {
                        src.Qualifier = Modification.Created;
                    }

                    if (src.Twin == null) target.Tables.Add(new Table(m_Base, src));

                    Qualifier = Modification.Modified;
                }
                else
                {
                }
            }

            return Qualifier;
        }

        /// <summary>
        /// Resolves the links.
        /// </summary>
        /// <param name="Twin">The twin.</param>
        public void ResolveLinks(Base NewParent)
        {
            foreach (Table Tbl in m_Tables)
            {
                Tbl.ResolveLinks(NewParent);
            }
        }
    }
}
