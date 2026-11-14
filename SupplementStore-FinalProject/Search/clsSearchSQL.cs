using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplementStore_FinalProject.Search
{
    public class clsSearchSQL
    {
        /// <summary>
        /// Selects all invoices
        /// </summary>
        public string SelectAllInvoices()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices ORDER BY InvoiceNum";
        }

        /// <summary>
        /// Selects invoices by invoice number
        /// </summary>
        public string SelectInvoicesByNum()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE InvoiceNum = ?";
        }

        /// <summary>
        /// Selects invoices by invoice date
        /// </summary>
        public string SelectInvoicesByDate()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE InvoiceDate = ?";
        }

        /// <summary>
        /// Selects invoices by total charge
        /// </summary>
        public string SelectInvoicesByCost()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE TotalCharge = ?";
        }

        /// <summary>
        /// Selects invoices by invoice number and date
        /// </summary>
        public string SelectInvoicesByNumAndDate()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE InvoiceNum = ? " +
                   "AND InvoiceDate = ?";
        }

        /// <summary>
        /// Selects invoices by invoice number and total charge
        /// </summary>
        public string SelectInvoicesByNumAndCost()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE InvoiceNum = ? " +
                   "AND TotalCharge = ?";
        }

        /// <summary>
        /// Selects invoices by invoice date and total charge
        /// </summary>
        public string SelectInvoicesByDateAndCost()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE InvoiceDate = ? " +
                   "AND TotalCharge = ?";
        }

        /// <summary>
        /// Selects invoices by invoice number, date and total charge
        /// </summary>
        public string SelectInvoicesByNumDateAndCost()
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                   "FROM Invoices WHERE InvoiceNum = ? " +
                   "AND InvoiceDate = ? AND TotalCharge = ?";
        }

        /// <summary>
        /// Selects distinct invoice numbers for the I.N. comboBox
        /// </summary>
        public string SelectDistinctInvoiceNums()
        {
            return "SELECT DISTINCT InvoiceNum FROM Invoices " +
                   "ORDER BY InvoiceNum";
        }

        /// <summary>
        /// Selects distinct invoice dates for the I.D. comboBox
        /// </summary>
        public string SelectDistinctInvoiceDates()
        {
            return "SELECT DISTINCT InvoiceDate FROM Invoices " +
                   "ORDER BY InvoiceDate";
        }

        /// <summary>
        /// Selects distinct total charges for the T.C. comboBox
        /// </summary>
        public string SelectDistinctInvoiceCosts()
        {
            return "SELECT DISTINCT TotalCharge FROM Invoices " +
                   "ORDER BY TotalCharge";
        }
    }
}
