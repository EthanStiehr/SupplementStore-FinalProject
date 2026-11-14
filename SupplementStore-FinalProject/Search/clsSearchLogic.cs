using System;
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace SupplementStore_FinalProject.Search
{
    public class clsSearchLogic
    {
        /// <summary>
        /// Holdes the SQL statements used by the class
        /// </summary>
        private readonly clsSearchSQL sql = new clsSearchSQL();

        /// <summary>
        /// Creates the connection to the Supplement Store DB
        /// </summary>
        private OleDbConnection GetConnection()
        {
            string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SupplementStore.accdb");

            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException("The SupplementStore.accdb database was not found", databasePath);
            }

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + 
                                      "Data Source=" + databasePath + ";";

            return new OleDbConnection(connectionString);
        }

        /// <summary>
        /// Gets all distinct invoice numbers 
        /// </summary>
        public DataTable GetInvoiceNums()
        {
            using (OleDbConnection connection = GetConnection())
            using (OleDbDataAdapter adapter =
                new OleDbDataAdapter(
                    sql.SelectDistinctInvoiceNums(), connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        /// <summary>
        /// Gets all the distinct Invoice dates
        /// </summary>
        public DataTable GetInvoiceDates()
        {
            using (OleDbConnection connection = GetConnection())
            using (OleDbDataAdapter adapter =
                new OleDbDataAdapter(
                    sql.SelectDistinctInvoiceDates(), connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        /// <summary>
        /// Gets all the distinct invoice costs
        /// </summary>
        public DataTable GetInvoiceCosts()
        {
            using (OleDbConnection connection = GetConnection())
            using (OleDbDataAdapter adapter =
                new OleDbDataAdapter(
                    sql.SelectDistinctInvoiceCosts(), connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public DataTable GetInvoices(string invoiceNum, DateTime? invoiceDate, decimal? totalCharge)
        {
            string sSQL;

            // This determines which SQL statements to use based on the filters
            bool hasInvoiceNum = !string.IsNullOrWhiteSpace(invoiceNum);
            bool hasInvoiceDate = invoiceDate.HasValue;
            bool hasTotalCharge = totalCharge.HasValue;

            if (hasInvoiceNum && hasInvoiceDate && hasTotalCharge)
            {
                sSQL = sql.SelectInvoicesByNumDateAndCost();
            }
            else if (hasInvoiceNum && hasInvoiceDate)
            {
                sSQL = sql.SelectInvoicesByNumAndDate();
            }
            else if (hasInvoiceNum && hasTotalCharge)
            {
                sSQL = sql.SelectInvoicesByNumAndCost();
            }
            else if (hasInvoiceDate && hasTotalCharge)
            {
                sSQL = sql.SelectInvoicesByDateAndCost();
            }
            else if (hasInvoiceNum)
            {
                sSQL = sql.SelectInvoicesByNum();
            }
            else if (hasInvoiceDate)
            {
                sSQL = sql.SelectInvoicesByDate();
            }
            else if (hasTotalCharge)
            {
                sSQL = sql.SelectInvoicesByCost();
            }
            else
            {
                sSQL = sql.SelectAllInvoices();
            }

            using (OleDbConnection connection = GetConnection())
            using (OleDbCommand command = new OleDbCommand(sSQL, connection))
            {
                if (hasInvoiceNum)
                {
                    command.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(invoiceNum);
                }

                if (hasInvoiceDate)
                {
                    command.Parameters.Add("?", OleDbType.Date).Value = invoiceDate.Value;
                }

                if (hasTotalCharge)
                {
                    command.Parameters.Add("?", OleDbType.Currency).Value = totalCharge.Value;
                }

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }

        }
    }
}
