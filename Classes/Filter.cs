using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServerDatabaseDiff
{
    public class Filter
    {
        private String m_Text;
        private Boolean m_HideUnmodified;
        private Boolean m_HideModified;
        private Boolean m_HideCreated;
        private Boolean m_HideDropped;

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        public Filter()
        {
            m_Text = String.Empty;
            m_HideUnmodified = false;
            m_HideModified = false;
            m_HideCreated = false;
            m_HideDropped = false;
        }

        public String Text { get { return m_Text; } set { m_Text = value; } }
        public Boolean HideUnmodified { get { return m_HideUnmodified; } set { m_HideUnmodified = value; } }
        public Boolean HideModified { get { return m_HideModified; } set { m_HideModified = value; } }
        public Boolean HideCreated { get { return m_HideCreated; } set { m_HideCreated = value; } }
        public Boolean HideDropped { get { return m_HideDropped; } set { m_HideDropped = value; } }
    }
}
