using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AskCaro_console.CsvFile
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        public static DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("Question", typeof(string));
            table.Columns.Add("Answer", typeof(string));
            table.Columns.Add("Tags", typeof(string));
            table.Columns.Add("More", typeof(string));

            // Here we add five DataRows.
            table.Rows.Add("Hi", "Hi ", "dialogue",null);
            table.Rows.Add("Hi, Helen! How’s it going?", "Fine, thanks", "dialogue", "null");
            table.Rows.Add(" I’ll see you later then. Good luck! ", "Thanks. See you later.", "dialogue", "null");
            table.Rows.Add("Thank you! Have a good one! ", "Thank you for talking to me. Have a nice day!", "dialogue", "null");
            return table;
        }
        public static void WriteToCsvFile(this DataTable dataTable, string filePath)
        {
            StringBuilder fileContent = new StringBuilder();

            foreach (var col in dataTable.Columns)
            {
                fileContent.Append(col.ToString() + "\t");
            }

            fileContent.Replace("\t", System.Environment.NewLine, fileContent.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append(column.ToString() + "\t");
                }

                fileContent.Replace("\t", System.Environment.NewLine, fileContent.Length - 1, 1);
            }

            System.IO.File.WriteAllText(filePath, fileContent.ToString());
        }
    }
}
