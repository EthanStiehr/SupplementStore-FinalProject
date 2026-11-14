using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace SupplementStore_FinalProject
{
    /// <summary>
    /// Holds business logic for "Main Window"
    /// </summary>
    public class clsMainLogic
    {
        private readonly clsMainSQL sql = new clsMainSQL();

        /// <summary>
        /// Gets a connection to the database
        /// </summary>
        /// <returns>A connection to the database</returns>
        private OleDbConnection GetConnection()
        {
            try
            {
                string databasePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "SupplementStore.accdb");

                if (!File.Exists(databasePath))
                {
                    throw new FileNotFoundException(
                        "The SupplementStore.accdb database was not found.",
                        databasePath);
                }

                string connectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + databasePath + ";";

                return new OleDbConnection(connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a list of all items
        /// </summary>
        /// <returns>A string containing all item descriptions</returns>
        public string GetAllItems()
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbDataAdapter adapter =
                    new OleDbDataAdapter(sql.SelectAllItems(), connection))
                {
                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in table.Rows)
                    {
                        sb.AppendLine(row["ItemDescription"].ToString());
                    }

                    return sb.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the details of an existing invoice
        /// </summary>
        /// <param name="invoiceNum">The invoice number</param>
        /// <param name="invoiceDate">The invoice date</param>
        /// <param name="totalCharge">The total charge</param>
        public void UpdateInvoiceDetails(string invoiceNum, string invoiceDate, string totalCharge)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbDataAdapter adapter =
                    new OleDbDataAdapter(sql.UpdateInvoice(invoiceNum, invoiceDate, totalCharge), connection)) { }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddInvoiceItem(string invoiceNum, string itemCode, string itemCost)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbDataAdapter adapter =
                    new OleDbDataAdapter(sql.InsertInvoiceItem(invoiceNum, itemCode, itemCost), connection)) { }

            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetItemCode(string itemDescription)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.GetItemCode(itemDescription), connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception("Item code not found for the given description.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveInvoiceItem(string invoiceNum, string itemCode)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbDataAdapter adapter =
                    new OleDbDataAdapter(sql.DeleteInvoiceItem(invoiceNum, itemCode), connection)) { }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateNewInvoice(string invoiceDate, string totalCharge)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbDataAdapter adapter =
                    new OleDbDataAdapter(sql.InsertInvoice(invoiceDate, totalCharge), connection)) { }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateTotalCost(string currentTotal, string itemCost, bool isAdding)
        {
            try
            {
                decimal currentTotalDecimal = decimal.Parse(currentTotal);
                decimal itemCostDecimal = decimal.Parse(itemCost);
                if (isAdding)
                {
                    currentTotalDecimal += itemCostDecimal;
                }
                else
                {
                    currentTotalDecimal -= itemCostDecimal;
                }
                return currentTotalDecimal.ToString("F2");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetItemCost(string itemCode)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.GetItemCost(itemCode), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar).Value = itemCode;
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception("Item cost not found for the given item code.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetInvoiceTotal(string invoiceNum)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.GetInvoiceTotal(invoiceNum), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar).Value = invoiceNum;
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception("Total charge not found for the given invoice number.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}