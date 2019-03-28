
using System;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class FunctionContainer : ContainerBase
    {
        #region Fields

        private List<Function> m_Functions;

        [NonSerialized]
        private Base m_Base;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionContainer"/> class.
        /// </summary>
        public FunctionContainer()
        {
            m_Functions = new List<Function>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionContainer"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public FunctionContainer(Base target)
            : this()
        {
            m_Base = target;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the functions.
        /// </summary>
        /// <value>The functions.</value>
        public List<Function> Functions
        {
            get { return m_Functions; }
            set { m_Functions = value; }
        }

        /// <summary>
        /// Gets the modified functions.
        /// </summary>
        /// <value>The modified functions.</value>
        [XmlIgnore]
        public List<Function> ModifiedFunctions
        {
            get
            {
                List<Function> ret = new List<Function>();

                foreach (Function func in m_Functions)
                {
                    if (func.Twin != null && (func.Qualifier == Qualifiable.Modification.Modified || func.Twin.Qualifier == Qualifiable.Modification.Modified))
                    {
                        ret.Add(func);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Gets the dropped functions.
        /// </summary>
        /// <value>The dropped functions.</value>
        [XmlIgnore]
        public List<Function> DroppedFunctions
        {
            get
            {
                List<Function> ret = new List<Function>();

                foreach (Function func in m_Functions)
                {
                    if (func.Qualifier == Qualifiable.Modification.Deleted)
                    {
                        ret.Add(func);
                    }
                }

                return ret;
            }
        }

        /// <summary>
        /// Gets the created functions.
        /// </summary>
        /// <value>The created functions.</value>
        [XmlIgnore]
        public List<Function> CreatedFunctions
        {
            get
            {
                List<Function> ret = new List<Function>();

                foreach (Function func in m_Functions)
                {
                    if (func.Qualifier == Qualifiable.Modification.Created)
                    {
                        ret.Add(func);
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

            String FunctionFilter = Filter.Text;

            node.Text = "Functions";
            node.Tag = this;

            if (FunctionFilter != String.Empty) node.Text += " (Filtered)";

            Base.SetNodeImageFromQualifier(node, Qualifier, IsGhost);

            foreach (Function func in Functions)
            {
                if
                (
                    (func.Qualifier == Modification.None && Filter.HideUnmodified == false) ||
                    (func.Qualifier == Modification.Modified && Filter.HideModified == false) ||
                    (func.Qualifier == Modification.Created && Filter.HideCreated == false) ||
                    (func.Qualifier == Modification.Deleted && Filter.HideDropped == false)
                )
                {
                    if (Filter.Text != String.Empty)
                    {
                        if (func.Name.ToLower().Contains(Filter.Text.ToLower()))
                        {
                            node.Nodes.Add(func.toTreeNode());
                        }
                    }
                    else
                    {
                        node.Nodes.Add(func.toTreeNode());
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
        public Modification qualifyVersus(FunctionContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            foreach (Function src in Functions)
            {
                // progress.Value++;

                bool found = false;

                foreach (Function tgt in target.Functions)
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

                    if (src.Twin == null) target.Functions.Add(new Function(src));

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
            foreach (Function Func in m_Functions)
            {
                Func.ResolveLinks(NewParent);
            }
        }

        #endregion
    }
}
