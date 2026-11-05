using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

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

        //







    }
}