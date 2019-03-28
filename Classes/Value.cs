
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServerDatabaseDiff
{
    public class Value
    {
        String m_Type;
        String m_Data;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Value"/> class.
        /// </summary>
        public Value()
        {
            m_Type = String.Empty;
            m_Data = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public String Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public String Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        #endregion
    }
}
