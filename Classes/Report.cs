
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SQLServerDatabaseDiff
{
    class Report
    {
        private Base[] m_Bases;
        private Configuration m_Configuration;

        public const int Base1 = 0;
        public const int Base2 = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        /// <param name="NewBases">The new bases.</param>
        public Report(Base[] NewBases, Configuration NewConfiguration)
        {
            m_Bases = NewBases;
            m_Configuration = NewConfiguration;
        }

        /// <summary>
        /// Generates a report for the comparison of the databases
        /// </summary>
        public void writeReport(TextWriter writer)
        {
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine(String.Format("Comparison of {0} versus {1} (Structure only)", m_Bases[Base1].Connection.Name, m_Bases[Base2].Connection.Name));
            writer.WriteLine(String.Empty);

            List<Table> diff_tables = m_Bases[Base1].TableCont.ModifiedTables;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Tables that are different ({0}) :", diff_tables.Count.ToString());
            foreach (Table tbl in diff_tables)
            {
                String txt = tbl.Name;
                if (tbl.FieldCont.DifferentFieldsCount > 0)
                {
                    List<Field> diff_fields = tbl.FieldCont.DifferentFieds;
                    txt += " (";
                    foreach (Field f in diff_fields)
                    {
                        txt += " " + f.Name;
                    }
                    txt += ")";
                }
                writer.WriteLine(txt);
            }
            writer.WriteLine(String.Empty);

            diff_tables = m_Bases[Base1].TableCont.DroppedTables;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Tables that exist only in {0} ({1}) :", m_Bases[Base1].Connection.Name, diff_tables.Count.ToString());
            foreach (Table tbl in diff_tables)
            {
                writer.WriteLine(tbl.Name);
            }
            writer.WriteLine(String.Empty);

            List<Table> diff_tables_drp = m_Bases[Base2].TableCont.DroppedTables;
            List<Table> diff_tables_crt = m_Bases[Base2].TableCont.CreatedTables;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Tables that exist only in {0} ({1}) :", m_Bases[Base2].Connection.Name, (diff_tables_drp.Count + diff_tables_crt.Count).ToString());
            foreach (Table tbl in diff_tables_drp)
            {
                writer.WriteLine(tbl.Name);
            }
            foreach (Table tbl in diff_tables_crt)
            {
                writer.WriteLine(tbl.Name);
            }
            writer.WriteLine(String.Empty);

            List<View> diff_views = m_Bases[Base1].ViewCont.ModifiedViews;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Views that are different ({0}) :", diff_views.Count.ToString());
            foreach (View tbl in diff_views)
            {
                String txt = tbl.Name;
                if (tbl.FieldCont.DifferentFieldsCount > 0)
                {
                    List<Field> diff_fields = tbl.FieldCont.DifferentFieds;
                    txt += " (";
                    foreach (Field f in diff_fields)
                    {
                        txt += " " + f.Name;
                    }
                    txt += ")";
                }
                writer.WriteLine(txt);
            }
            writer.WriteLine(String.Empty);

            diff_views = m_Bases[Base1].ViewCont.DroppedViews;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Views that exist only in {0} ({1}) :", m_Bases[Base1].Connection.Name, diff_views.Count.ToString());
            foreach (View tbl in diff_views)
            {
                writer.WriteLine(tbl.Name);
            }
            writer.WriteLine(String.Empty);

            List<View> diff_views_drp = m_Bases[Base2].ViewCont.DroppedViews;
            List<View> diff_views_crt = m_Bases[Base2].ViewCont.CreatedViews;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Views that exist only in {0} ({1}) :", m_Bases[Base2].Connection.Name, (diff_views_drp.Count + diff_views_crt.Count).ToString());
            foreach (View tbl in diff_views_drp)
            {
                writer.WriteLine(tbl.Name);
            }
            foreach (View tbl in diff_views_crt)
            {
                writer.WriteLine(tbl.Name);
            }
            writer.WriteLine(String.Empty);

            List<Procedure> diff_procs = m_Bases[Base1].ProcedureCont.ModifiedProcedures;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Procedures that are different ({0}) :", diff_procs.Count.ToString());
            foreach (Procedure proc in diff_procs)
            {
                writer.WriteLine(proc.Name);
            }
            writer.WriteLine(String.Empty);

            diff_procs = m_Bases[Base1].ProcedureCont.DroppedProcedures;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Procedures that exist only in {0} ({1}) :", m_Bases[Base1].Connection.Name, diff_procs.Count.ToString());
            foreach (Procedure proc in diff_procs)
            {
                writer.WriteLine(proc.Name);
            }
            writer.WriteLine(String.Empty);

            List<Procedure> diff_procs_drp = m_Bases[Base2].ProcedureCont.DroppedProcedures;
            List<Procedure> diff_procs_crt = m_Bases[Base2].ProcedureCont.CreatedProcedures;
            writer.WriteLine("----------------------------------------------------------------");
            writer.WriteLine("Procedures that exist only in {0} ({1}) :", m_Bases[Base2].Connection.Name, (diff_procs_drp.Count + diff_procs_crt.Count).ToString());
            foreach (Procedure proc in diff_procs_drp)
            {
                writer.WriteLine(proc.Name);
            }
            foreach (Procedure proc in diff_procs_crt)
            {
                writer.WriteLine(proc.Name);
            }
            writer.WriteLine(String.Empty);
        }

        /// <summary>
        /// Generates a script that transforms base A to base B
        /// </summary>
        public void writeUpgradeScript(TextWriter writer, String UseBase)
        {
            writer.WriteLine("USE " + UseBase + ";");
            writer.WriteLine("DECLARE @tmp AS NVARCHAR(4000);");

            // Handle tables

            List<Table> tables;

            tables = m_Bases[Base1].TableCont.DroppedTables;
            foreach (Table table in tables) writeUpgradeScript_Table(writer, table);

            tables = m_Bases[Base2].TableCont.DroppedTables;
            foreach (Table table in tables) writeUpgradeScript_Table(writer, table);

            tables = m_Bases[Base1].TableCont.CreatedTables;
            foreach (Table table in tables) writeUpgradeScript_Table(writer, table);

            tables = m_Bases[Base2].TableCont.CreatedTables;
            foreach (Table table in tables) writeUpgradeScript_Table(writer, table);

            tables = m_Bases[Base1].TableCont.ModifiedTables;
            foreach (Table table in tables) writeUpgradeScript_Table(writer, table);

            tables = m_Bases[Base2].TableCont.ModifiedTables;
            foreach (Table table in tables) writeUpgradeScript_Table(writer, table);

            tables = m_Bases[Base1].TableCont.CreatedTables;
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.PrimaryKey, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Unique, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Check, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.ForeignKey, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Referential, true);

            tables = m_Bases[Base2].TableCont.CreatedTables;
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.PrimaryKey, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Unique, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Check, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.ForeignKey, true);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Referential, true);

            tables = m_Bases[Base1].TableCont.ModifiedTables;
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.PrimaryKey, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Unique, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Check, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.ForeignKey, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Referential, false);

            tables = m_Bases[Base2].TableCont.ModifiedTables;
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.PrimaryKey, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Unique, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Check, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.ForeignKey, false);
            foreach (Table table in tables) writeUpgradeScript_TableConstraints(writer, table, Constraint.ConstraintType.Referential, false);

            // Handle views

            List<View> views;

            views = m_Bases[Base1].ViewCont.DroppedViews;
            foreach (View view in views) writeUpgradeScript_View(writer, view);

            views = m_Bases[Base2].ViewCont.DroppedViews;
            foreach (View view in views) writeUpgradeScript_View(writer, view);

            views = m_Bases[Base1].ViewCont.CreatedViews;
            foreach (View view in views) writeUpgradeScript_View(writer, view);

            views = m_Bases[Base2].ViewCont.CreatedViews;
            foreach (View view in views) writeUpgradeScript_View(writer, view);

            views = m_Bases[Base1].ViewCont.ModifiedViews;
            foreach (View view in views) writeUpgradeScript_View(writer, view);

            views = m_Bases[Base2].ViewCont.ModifiedViews;
            foreach (View view in views) writeUpgradeScript_View(writer, view);

            // Handle procedures

            List<Procedure> procs;

            procs = m_Bases[Base1].ProcedureCont.DroppedProcedures;
            foreach (Procedure proc in procs) writeUpgradeScript_Procedure(writer, proc);

            procs = m_Bases[Base2].ProcedureCont.DroppedProcedures;
            foreach (Procedure proc in procs) writeUpgradeScript_Procedure(writer, proc);

            procs = m_Bases[Base1].ProcedureCont.CreatedProcedures;
            foreach (Procedure proc in procs) writeUpgradeScript_Procedure(writer, proc);

            procs = m_Bases[Base2].ProcedureCont.CreatedProcedures;
            foreach (Procedure proc in procs) writeUpgradeScript_Procedure(writer, proc);

            procs = m_Bases[Base1].ProcedureCont.ModifiedProcedures;
            foreach (Procedure proc in procs) writeUpgradeScript_Procedure(writer, proc);

            procs = m_Bases[Base2].ProcedureCont.ModifiedProcedures;
            foreach (Procedure proc in procs) writeUpgradeScript_Procedure(writer, proc);

            // Handle functions

            List<Function> funcs;

            funcs = m_Bases[Base1].FunctionCont.DroppedFunctions;
            foreach (Function func in funcs) writeUpgradeScript_Function(writer, func);

            funcs = m_Bases[Base2].FunctionCont.DroppedFunctions;
            foreach (Function func in funcs) writeUpgradeScript_Function(writer, func);

            funcs = m_Bases[Base1].FunctionCont.CreatedFunctions;
            foreach (Function func in funcs) writeUpgradeScript_Function(writer, func);

            funcs = m_Bases[Base2].FunctionCont.CreatedFunctions;
            foreach (Function func in funcs) writeUpgradeScript_Function(writer, func);

            funcs = m_Bases[Base1].FunctionCont.ModifiedFunctions;
            foreach (Function func in funcs) writeUpgradeScript_Function(writer, func);

            funcs = m_Bases[Base2].FunctionCont.ModifiedFunctions;
            foreach (Function func in funcs) writeUpgradeScript_Function(writer, func);
        }

        /// <summary>
        /// Writes the upgrade script_ table.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="table">The table.</param>
        private void writeUpgradeScript_Table(TextWriter writer, Table table)
        {
            if (table.Qualifier == Qualifiable.Modification.Created && m_Configuration.UpgradeAllowTableCreate)
            {
                String line = String.Format("CREATE TABLE [{0}] (", table.Name);

                bool First = true;

                foreach (Field f in table.FieldCont.Fields)
                {
                    if (First == false) line += ", ";
                    line += String.Format("[{0}] {1}", f.Name, f.Type);
                    if (f.Nullable == false) line += " NOT NULL";
                    if (f.Identity) line += " IDENTITY";
                    First = false;
                }

                line += ");";

                writer.WriteLine(line);
            }
            else if (table.Qualifier == Qualifiable.Modification.Deleted && m_Configuration.UpgradeAllowTableDrop)
            {
                writer.WriteLine("DROP TABLE [" + table.Name + "];");
            }
            else if (table.Qualifier == Qualifiable.Modification.Modified && m_Configuration.UpgradeAllowTableAlter)
            {
                foreach (Field f in table.FieldCont.Fields)
                {
                    if (f.Qualifier == Qualifiable.Modification.Created && m_Configuration.UpgradeAllowFieldCreate)
                    {
                        String line = String.Format("ALTER TABLE [{0}] ADD [{1}] {2}", table.Name, f.Name, f.Type);
                        if (f.Nullable == false) line += " NOT NULL";
                        line += ";";

                        writer.WriteLine(line);
                    }
                    else if (f.Qualifier == Qualifiable.Modification.Deleted && m_Configuration.UpgradeAllowFieldDrop)
                    {
                        String line = String.Format("ALTER TABLE [{0}] DROP COLUMN [{1}];", table.Name, f.Name);

                        writer.WriteLine(line);
                    }
                    else if (f.Qualifier == Qualifiable.Modification.Modified && m_Configuration.UpgradeAllowFieldAlter)
                    {
                        String line = String.Format("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2}", table.Name, f.Name, f.Type);
                        if (f.Nullable == false) line += " NOT NULL";
                        line += ";";

                        writer.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Writes the upgrade script_ table constraints.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="table">The table.</param>
        /// <param name="type">The type.</param>
        /// <param name="ForceCreation">if set to <c>true</c> [force creation].</param>
        private void writeUpgradeScript_TableConstraints(TextWriter writer, Table table, Constraint.ConstraintType type, Boolean ForceCreation)
        {
            foreach (Constraint cnst in table.ConstraintCont.Constraints)
            {
                if ((cnst.Qualifier == Qualifiable.Modification.Created || ForceCreation) && m_Configuration.UpgradeAllowConstraintCreate)
                {
                    String line = String.Empty;

                    switch (cnst.Type)
                    {
                        case Constraint.ConstraintType.PrimaryKey:
                            {
                                if (type == Constraint.ConstraintType.PrimaryKey)
                                {
                                    line = String.Format("ALTER TABLE [{0}] WITH NOCHECK ADD CONSTRAINT [{1}] PRIMARY KEY ({2});", table.Name, cnst.Name, Constraint.getFieldList(cnst.FieldNames));
                                }
                            }
                            break;
                        case Constraint.ConstraintType.Check:
                            {
                                if (type == Constraint.ConstraintType.Check)
                                {
                                    line = String.Format("ALTER TABLE [{0}] WITH NOCHECK ADD CONSTRAINT [{1}] CHECK {2};", table.Name, cnst.Name, cnst.Text);
                                }
                            }
                            break;
                        case Constraint.ConstraintType.Unique:
                            {
                                if (type == Constraint.ConstraintType.Unique)
                                {
                                    line = String.Format("ALTER TABLE [{0}] WITH NOCHECK ADD CONSTRAINT [{1}] UNIQUE ({2});", table.Name, cnst.Name, Constraint.getFieldList(cnst.FieldNames));
                                }
                            }
                            break;
                        case Constraint.ConstraintType.ForeignKey:
                            {
                                if (type == Constraint.ConstraintType.ForeignKey)
                                {
                                    line = String.Format
                                        (
                                            "ALTER TABLE [{0}] WITH NOCHECK ADD CONSTRAINT [{1}] FOREIGN KEY ({2}) REFERENCES [{3}] ({4});",
                                            table.Name, cnst.Name, Constraint.getFieldList(cnst.FieldNames),
                                            cnst.ReferencedTable, Constraint.getFieldList(cnst.ReferencedFieldNames)
                                        );
                                }
                            }
                            break;
                    }

                    if (line != String.Empty)
                    {
                        writer.WriteLine(line);
                    }
                }
                else if (cnst.Qualifier == Qualifiable.Modification.Deleted && m_Configuration.UpgradeAllowConstraintDrop)
                {
                    String line = String.Format("ALTER TABLE [{0}] DROP CONSTRAINT [{1}];", table.Name, cnst.Name);

                    writer.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Writes the upgrade script_ data.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="table">The table.</param>
        private void writeUpgradeScript_Data(TextWriter writer, Table table)
        {
        }

        /// <summary>
        /// Writes the upgrade script_ view.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="view">The view.</param>
        private void writeUpgradeScript_View(TextWriter writer, View view)
        {
            if (view.Qualifier == Qualifiable.Modification.Created && m_Configuration.UpgradeAllowViewCreate)
            {
                String txt = view.getDefinition();
                txt = txt.Replace("'", "''");
                writer.WriteLine("SET @tmp = '" + txt + "'");
                writer.WriteLine("exec sp_executesql @tmp;");
            }
            else if (view.Qualifier == Qualifiable.Modification.Modified && m_Configuration.UpgradeAllowViewAlter)
            {
                writer.WriteLine(String.Format("DROP VIEW [{0}];", view.Name));
                String txt = view.getDefinition();
                txt = txt.Replace("'", "''");
                writer.WriteLine("SET @tmp = '" + txt + "'");
                writer.WriteLine("exec sp_executesql @tmp;");
            }
            else if (view.Qualifier == Qualifiable.Modification.Deleted && m_Configuration.UpgradeAllowViewDrop)
            {
                writer.WriteLine(String.Format("DROP VIEW [{0}];", view.Name));
            }
        }

        /// <summary>
        /// Writes the upgrade script_ procedure.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="proc">The proc.</param>
        private void writeUpgradeScript_Procedure(TextWriter writer, Procedure proc)
        {
            if (proc.Qualifier == Qualifiable.Modification.Created && m_Configuration.UpgradeAllowProcedureCreate)
            {
                String txt = proc.getDefinition();
                txt = txt.Replace("'", "''");
                writer.WriteLine("SET @tmp = '" + txt + "'");
                writer.WriteLine("exec sp_executesql @tmp;");
            }
            else if (proc.Qualifier == Qualifiable.Modification.Modified && m_Configuration.UpgradeAllowProcedureAlter)
            {
                writer.WriteLine(String.Format("DROP PROCEDURE [{0}];", proc.Name));
                String txt = proc.getDefinition();
                txt = txt.Replace("'", "''");
                writer.WriteLine("SET @tmp = '" + txt + "'");
                writer.WriteLine("exec sp_executesql @tmp;");
            }
            else if (proc.Qualifier == Qualifiable.Modification.Deleted && m_Configuration.UpgradeAllowProcedureDrop)
            {
                writer.WriteLine(String.Format("DROP PROCEDURE [{0}];", proc.Name));
            }
        }

        /// <summary>
        /// Writes the upgrade script_ function.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="func">The func.</param>
        private void writeUpgradeScript_Function(TextWriter writer, Function func)
        {
            if (func.Qualifier == Qualifiable.Modification.Created && m_Configuration.UpgradeAllowFunctionCreate)
            {
                String txt = func.getDefinition();
                txt = txt.Replace("'", "''");
                writer.WriteLine("SET @tmp = '" + txt + "'");
                writer.WriteLine("exec sp_executesql @tmp;");
            }
            else if (func.Qualifier == Qualifiable.Modification.Modified && m_Configuration.UpgradeAllowFunctionAlter)
            {
                writer.WriteLine(String.Format("DROP FUNCTION [{0}];", func.Name));
                String txt = func.getDefinition();
                txt = txt.Replace("'", "''");
                writer.WriteLine("SET @tmp = '" + txt + "'");
                writer.WriteLine("exec sp_executesql @tmp;");
            }
            else if (func.Qualifier == Qualifiable.Modification.Deleted && m_Configuration.UpgradeAllowFunctionDrop)
            {
                writer.WriteLine(String.Format("DROP FUNCTION [{0}];", func.Name));
            }
        }
    }
}
