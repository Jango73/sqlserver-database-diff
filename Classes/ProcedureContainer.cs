
using System;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class ProcedureContainer : ContainerBase
    {
        #region Fields

        private List<Procedure> m_Procedures;

        [NonSerialized]
        private Base m_Base;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureContainer"/> class.
        /// </summary>
        public ProcedureContainer()
        {
            m_Procedures = new List<Procedure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureContainer"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public ProcedureContainer(Base target)
            : this()
        {
            m_Base = target;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the procedures.
        /// </summary>
        /// <value>The procedures.</value>
        public List<Procedure> Procedures
        {
            get { return m_Procedures; }
            set { m_Procedures = value; }
        }

        /// <summary>
        /// Gets the modified procedures.
        /// </summary>
        /// <value>The modified procedures.</value>
        [XmlIgnore]
        public List<Procedure> ModifiedProcedures
        {
            get
            {
                List<Procedure> ret = new List<Procedure>();

                foreach (Procedure proc in m_Procedures)
                {
                    if (proc.Twin != null && (proc.Qualifier == Qualifiable.Modification.Modified || proc.Twin.Qualifier == Qualifiable.Modification.Modified))
                    {
                        ret.Add(proc);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Gets the dropped procedures.
        /// </summary>
        /// <value>The dropped procedures.</value>
        [XmlIgnore]
        public List<Procedure> DroppedProcedures
        {
            get
            {
                List<Procedure> ret = new List<Procedure>();

                foreach (Procedure proc in m_Procedures)
                {
                    if (proc.Qualifier == Qualifiable.Modification.Deleted)
                    {
                        ret.Add(proc);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Gets the created procedures.
        /// </summary>
        /// <value>The created procedures.</value>
        [XmlIgnore]
        public List<Procedure> CreatedProcedures
        {
            get
            {
                List<Procedure> ret = new List<Procedure>();

                foreach (Procedure proc in m_Procedures)
                {
                    if (proc.Qualifier == Qualifiable.Modification.Created)
                    {
                        ret.Add(proc);
                    }
                }

                return ret;
            }
        }

        #endregion

        #region Methods

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
                result = (result * 397) ^ ("ProcedureContainer").ToString().GetHashCode();
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
        /// Makes a tree node from this object.
        /// </summary>
        /// <returns></returns>
        public override TreeNode toTreeNode()
        {
            TreeNode node = new TreeNode();

            String ProcedureFilter = Filter.Text;

            node.Text = "Procedures";
            node.Tag = this;

            if (ProcedureFilter != String.Empty) node.Text += " (Filtered)";

            Base.SetNodeImageFromQualifier(node, Qualifier, IsGhost);

            foreach (Procedure proc in Procedures)
            {
                if
                (
                    (proc.Qualifier == Modification.None && Filter.HideUnmodified == false) ||
                    (proc.Qualifier == Modification.Modified && Filter.HideModified == false) ||
                    (proc.Qualifier == Modification.Created && Filter.HideCreated == false) ||
                    (proc.Qualifier == Modification.Deleted && Filter.HideDropped == false)
                )
                {
                    if (Filter.Text != String.Empty)
                    {
                        if (proc.Name.ToLower().Contains(Filter.Text.ToLower()))
                        {
                            node.Nodes.Add(proc.toTreeNode());
                        }
                    }
                    else
                    {
                        node.Nodes.Add(proc.toTreeNode());
                    }
                }
            }

            return node;
        }

        /// <summary>
        /// Qualifies this object against another one.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="dir">The direction.</param>
        /// <returns></returns>
        public Modification qualifyVersus(ProcedureContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            foreach (Procedure src in Procedures)
            {
                // progress.Value++;

                bool found = false;

                foreach (Procedure tgt in target.Procedures)
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

                    if (src.Twin == null) target.Procedures.Add(new Procedure(src));

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
            foreach (Procedure Proc in m_Procedures)
            {
                Proc.ResolveLinks(NewParent);
            }
        }

        #endregion
    }
}
