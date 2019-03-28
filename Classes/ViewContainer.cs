
using System;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class ViewContainer : ContainerBase
    {
        private List<View> m_Views;

        [NonSerialized]
        private Base m_Base;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewContainer"/> class.
        /// </summary>
        public ViewContainer()
        {
            m_Views = new List<View>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewContainer"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public ViewContainer(Base target)
            : this()
        {
            m_Base = target;
        }

        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>The views.</value>
        public List<View> Views
        {
            get { return m_Views; }
            set { m_Views = value; }
        }

        /// <summary>
        /// Gets the modified views.
        /// </summary>
        /// <value>The modified views.</value>
        [XmlIgnore]
        public List<View> ModifiedViews
        {
            get
            {
                List<View> ret = new List<View>();

                foreach (View tbl in m_Views)
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
        /// Gets the dropped views.
        /// </summary>
        /// <value>The dropped views.</value>
        [XmlIgnore]
        public List<View> DroppedViews
        {
            get
            {
                List<View> ret = new List<View>();

                foreach (View tbl in m_Views)
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
        /// Gets the created views.
        /// </summary>
        /// <value>The created views.</value>
        [XmlIgnore]
        public List<View> CreatedViews
        {
            get
            {
                List<View> ret = new List<View>();

                foreach (View tbl in m_Views)
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

            Node.Text = "Views";
            Node.Tag = this;

            if (Filter.Text != String.Empty) Node.Text += " (Filtered)";

            Base.SetNodeImageFromQualifier(Node, Qualifier, IsGhost);

            foreach (View CurrentView in Views)
            {
                if
                (
                    (CurrentView.Qualifier == Modification.None && Filter.HideUnmodified == false) ||
                    (CurrentView.Qualifier == Modification.Modified && Filter.HideModified == false) ||
                    (CurrentView.Qualifier == Modification.Created && Filter.HideCreated == false) ||
                    (CurrentView.Qualifier == Modification.Deleted && Filter.HideDropped == false)
                )
                {
                    if (Filter.Text != String.Empty)
                    {
                        if (CurrentView.Name.ToLower().Contains(Filter.Text.ToLower()))
                        {
                            Node.Nodes.Add(CurrentView.toTreeNode());
                        }
                    }
                    else
                    {
                        Node.Nodes.Add(CurrentView.toTreeNode());
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
        public Modification qualifyVersus(ViewContainer target, Direction dir)
        {
            Qualifier = Modification.None;

            foreach (View src in Views)
            {
                // progress.Value++;

                bool found = false;

                foreach (View tgt in target.Views)
                {
                    if (src.Name == tgt.Name)
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
        }
    }
}
