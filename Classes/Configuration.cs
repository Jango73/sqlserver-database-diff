using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SQLServerDatabaseDiff
{
    [Serializable]
    public class Configuration
    {
        #region Fields

        int _Version;

        ConnectionAdapter _Connection1;
        ConnectionAdapter _Connection2;
        ConnectionAdapter _Connection3;

        Boolean _ShowReportOnAnalyze;
        Boolean _HideUnmodifiedByDefault;
        Boolean _AnalyzeTables;
        Boolean _AnalyzeViews;
        Boolean _AnalyzeProcedures;
        Boolean _AnalyzeFunctions;
        Boolean _AnalyzeData;

        Boolean _UpgradeAllowTableCreate;
        Boolean _UpgradeAllowTableDrop;
        Boolean _UpgradeAllowTableAlter;

        Boolean _UpgradeAllowViewCreate;
        Boolean _UpgradeAllowViewDrop;
        Boolean _UpgradeAllowViewAlter;

        Boolean _UpgradeAllowFieldCreate;
        Boolean _UpgradeAllowFieldDrop;
        Boolean _UpgradeAllowFieldAlter;

        Boolean _UpgradeAllowConstraintCreate;
        Boolean _UpgradeAllowConstraintDrop;
        Boolean _UpgradeAllowConstraintAlter;

        Boolean _UpgradeAllowProcedureCreate;
        Boolean _UpgradeAllowProcedureDrop;
        Boolean _UpgradeAllowProcedureAlter;

        Boolean _UpgradeAllowFunctionCreate;
        Boolean _UpgradeAllowFunctionDrop;
        Boolean _UpgradeAllowFunctionAlter;

        String _BackupPath;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            _Version = 1;

            _Connection1 = new ConnectionAdapter();
            _Connection2 = new ConnectionAdapter();
            _Connection3 = new ConnectionAdapter();

            _ShowReportOnAnalyze = true;
            _HideUnmodifiedByDefault = false;

            _AnalyzeTables = true;
            _AnalyzeViews = true;
            _AnalyzeProcedures = true;
            _AnalyzeFunctions = true;
            _AnalyzeData = false;

            _UpgradeAllowTableCreate = true;
            _UpgradeAllowTableDrop = true;
            _UpgradeAllowTableAlter = true;

            _UpgradeAllowViewCreate = true;
            _UpgradeAllowViewDrop = true;
            _UpgradeAllowViewAlter = true;

            _UpgradeAllowFieldCreate = true;
            _UpgradeAllowFieldDrop = true;
            _UpgradeAllowFieldAlter = true;

            _UpgradeAllowConstraintCreate = true;
            _UpgradeAllowConstraintDrop = true;
            _UpgradeAllowConstraintAlter = true;

            _UpgradeAllowProcedureCreate = true;
            _UpgradeAllowProcedureDrop = true;
            _UpgradeAllowProcedureAlter = true;

            _UpgradeAllowFunctionCreate = true;
            _UpgradeAllowFunctionDrop = true;
            _UpgradeAllowFunctionAlter = true;

            _BackupPath = String.Empty;
        }

        #endregion

        #region Properties

        public int Version { get { return _Version; } set { _Version = value; } }

        public Boolean ShowReportOnAnalyze { get { return _ShowReportOnAnalyze; } set { _ShowReportOnAnalyze = value; } }
        public Boolean HideUnmodifiedByDefault { get { return _HideUnmodifiedByDefault; } set { _HideUnmodifiedByDefault = value; } }

        public Boolean AnalyzeTables { get { return _AnalyzeTables; } set { _AnalyzeTables = value; } }
        public Boolean AnalyzeViews { get { return _AnalyzeViews; } set { _AnalyzeViews = value; } }
        public Boolean AnalyzeProcedures { get { return _AnalyzeProcedures; } set { _AnalyzeProcedures = value; } }
        public Boolean AnalyzeFunctions { get { return _AnalyzeFunctions; } set { _AnalyzeFunctions = value; } }
        public Boolean AnalyzeData { get { return _AnalyzeData; } set { _AnalyzeData = value; } }

        public ConnectionAdapter Connection1 { get { return _Connection1; } set { _Connection1 = value; } }
        public ConnectionAdapter Connection2 { get { return _Connection2; } set { _Connection2 = value; } }
        public ConnectionAdapter Connection3 { get { return _Connection3; } set { _Connection3 = value; } }

        public Boolean UpgradeAllowTableCreate { get { return _UpgradeAllowTableCreate; } set { _UpgradeAllowTableCreate = value; } }
        public Boolean UpgradeAllowTableDrop { get { return _UpgradeAllowTableDrop; } set { _UpgradeAllowTableDrop = value; } }
        public Boolean UpgradeAllowTableAlter { get { return _UpgradeAllowTableAlter; } set { _UpgradeAllowTableAlter = value; } }

        public Boolean UpgradeAllowViewCreate { get { return _UpgradeAllowViewCreate; } set { _UpgradeAllowViewCreate = value; } }
        public Boolean UpgradeAllowViewDrop { get { return _UpgradeAllowViewDrop; } set { _UpgradeAllowViewDrop = value; } }
        public Boolean UpgradeAllowViewAlter { get { return _UpgradeAllowViewAlter; } set { _UpgradeAllowViewAlter = value; } }

        public Boolean UpgradeAllowFieldCreate { get { return _UpgradeAllowFieldCreate; } set { _UpgradeAllowFieldCreate = value; } }
        public Boolean UpgradeAllowFieldDrop { get { return _UpgradeAllowFieldDrop; } set { _UpgradeAllowFieldDrop = value; } }
        public Boolean UpgradeAllowFieldAlter { get { return _UpgradeAllowFieldAlter; } set { _UpgradeAllowFieldAlter = value; } }

        public Boolean UpgradeAllowConstraintCreate { get { return _UpgradeAllowConstraintCreate; } set { _UpgradeAllowConstraintCreate = value; } }
        public Boolean UpgradeAllowConstraintDrop { get { return _UpgradeAllowConstraintDrop; } set { _UpgradeAllowConstraintDrop = value; } }
        public Boolean UpgradeAllowConstraintAlter { get { return _UpgradeAllowConstraintAlter; } set { _UpgradeAllowConstraintAlter = value; } }

        public Boolean UpgradeAllowProcedureCreate { get { return _UpgradeAllowProcedureCreate; } set { _UpgradeAllowProcedureCreate = value; } }
        public Boolean UpgradeAllowProcedureDrop { get { return _UpgradeAllowProcedureDrop; } set { _UpgradeAllowProcedureDrop = value; } }
        public Boolean UpgradeAllowProcedureAlter { get { return _UpgradeAllowProcedureAlter; } set { _UpgradeAllowProcedureAlter = value; } }

        public Boolean UpgradeAllowFunctionCreate { get { return _UpgradeAllowFunctionCreate; } set { _UpgradeAllowFunctionCreate = value; } }
        public Boolean UpgradeAllowFunctionDrop { get { return _UpgradeAllowFunctionDrop; } set { _UpgradeAllowFunctionDrop = value; } }
        public Boolean UpgradeAllowFunctionAlter { get { return _UpgradeAllowFunctionAlter; } set { _UpgradeAllowFunctionAlter = value; } }

        public String BackupPath { get { return _BackupPath; } set { _BackupPath = value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="c">The c.</param>
        public static void Serialize(string file, Configuration c)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(c.GetType());
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Deserializes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static Configuration Deserialize(string file)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Configuration));
            StreamReader reader = File.OpenText(file);
            Configuration c = (Configuration)xs.Deserialize(reader);
            reader.Close();
            return c;
        }

        #endregion
    }
}
