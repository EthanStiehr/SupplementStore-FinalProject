using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace SupplementStore_FinalProject.Items
{
    /// <summary>
    /// holds business logic for "Items Window"
    /// </summary>
    public class clsItemsLogic
    {
        /// <summary>
        /// holds SQL statements used by class
        /// </summary>
        private readonly clsItemsSQL sql = new clsItemsSQL();

        /// <summary>
        /// creates connection to DB
        /// </summary>
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
        /// grabs all supplements from DB
        /// </summary>
        public DataTable GetItems()
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbDataAdapter adapter =
                    new OleDbDataAdapter(sql.SelectAllItems(), connection))
                {
                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    return table;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// checks if item code already exists
        /// </summary>
        public bool ItemExists(string code)
        {
            try
            {
                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.ItemExists(), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = code;

                    connection.Open();

                    int count = Convert.ToInt32(
                        command.ExecuteScalar());

                    return count > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// checks supplement's info
        /// </summary>
        public void ValidateItem(
            string code, string description, decimal cost)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception(
                        "Enter an item code.");
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new Exception(
                        "Enter an item description.");
                }

                if (code.Length > 20)
                {
                    throw new Exception(
                        "Item code cannot exceed 20 characters.");
                }

                if (description.Length > 255)
                {
                    throw new Exception(
                        "Description cannot exceed 255 characters.");
                }

                if (cost < 0)
                {
                    throw new Exception(
                        "Item cost cannot be negative.");
                }

                if (cost > 922337203685477.5807m)
                {
                    throw new Exception(
                        "Item cost exceeds the allowed currency range.");
                }

                if (decimal.Round(cost, 2) != cost)
                {
                    throw new Exception(
                        "Item cost cannot have more than two decimal places.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// adds new supplement to DB
        /// </summary>
        public void AddItem(
            string code, string description, decimal cost)
        {
            try
            {
                ValidateItem(code, description, cost);

                if (ItemExists(code))
                {
                    throw new Exception(
                        "An item with this code already exists.");
                }

                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.InsertItem(), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = code;

                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = description;

                    command.Parameters.Add("?", OleDbType.Currency)
                        .Value = cost;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// updates existing supplement without changing code
        /// </summary>
        public void UpdateItem(
            string code, string description, decimal cost)
        {
            try
            {
                ValidateItem(code, description, cost);

                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.UpdateItem(), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = description;

                    command.Parameters.Add("?", OleDbType.Currency)
                        .Value = cost;

                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = code;

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "The selected item was not found.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// gets unique invoice numbers holding an item
        /// </summary>
        public List<string> GetItemInvoices(string code)
        {
            try
            {
                List<string> invoices = new List<string>();

                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.SelectItemInvoices(), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = code;

                    connection.Open();

                    using (OleDbDataReader reader =
                        command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            invoices.Add(
                                reader["InvoiceNum"].ToString());
                        }
                    }
                }

                return invoices;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// deletes item if it is not used on invoice
        /// </summary>
        public void DeleteItem(string code)
        {
            try
            {
                List<string> invoices = GetItemInvoices(code);

                if (invoices.Count > 0)
                {
                    throw new Exception(
                        "This supplement cannot be deleted.\n\n" +
                        "It appears on invoice(s): " +
                        string.Join(", ", invoices));
                }

                using (OleDbConnection connection = GetConnection())
                using (OleDbCommand command =
                    new OleDbCommand(sql.DeleteItem(), connection))
                {
                    command.Parameters.Add("?", OleDbType.VarWChar)
                        .Value = code;

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception(
                            "The selected item was not found.");
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
