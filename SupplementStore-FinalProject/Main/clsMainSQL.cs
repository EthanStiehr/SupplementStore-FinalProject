namespace SupplementStore_FinalProject
{
    /// <summary>
    /// Holds SQL statements for "Main Window"
    /// </summary>
    public class clsMainSQL
    {
        /// <summary>
        /// Inserts new invoice
        /// </summary>
        public string InsertInvoice(string invoiceDate, string totalCharge)
        {
            return "INSERT INTO Invoices " +
                   "(InvoiceNum, InvoiceDate, TotalCharge) " +
                   "VALUES (" + invoiceDate + ", " + totalCharge + ")";
        }

        /// <summary>
        /// Updates invoice's date, customer name and address
        /// PK cannot be changed
        /// </summary>
        public string UpdateInvoice(string invoiceNum, string invoiceDate, string totalCharge)
        {
            return "UPDATE Invoices SET InvoiceDate =" + invoiceDate + ", " +
                   "TotalCharge = " + totalCharge + " WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Deletes invoice by invoice number
        /// </summary>
        public string DeleteInvoice(string invoiceNum)
        {
            return "DELETE FROM Invoices WHERE InvoiceNum = " + invoiceNum;
        }

        /// <summary>
        /// Selects all items ordered by item code
        /// </summary>
        public string SelectAllItems()
        {
            return "SELECT ItemCode, ItemDescription, ItemCost " +
                   "FROM Items ORDER BY ItemCode";
        }

        /// <summary>
        /// Inserts new invoice item
        /// </summary>
        public string InsertInvoiceItem(string invoiceNum, string itemCode, string itemCost)
        {
            return "INSERT INTO InvoiceItems " +
                   "(InvoiceNum, ItemCode, ItemCost) " +
                   "VALUES (" + invoiceNum + ", " + itemCode + ", " + itemCost + ")";
        }

        /// <summary>
        /// Gets the item code for a given item description
        /// </summary>
        public string GetItemCode(string itemDescription)
        {
            return "SELECT ItemCode FROM Items WHERE ItemDescription = '" + itemDescription + "'";
        }

        /// <summary>
        /// Deletes invoice item
        /// </summary>
        public string DeleteInvoiceItem(string invoiceNum, string itemCode)
        {
            return "DELETE FROM InvoiceItems WHERE InvoiceNum = " + invoiceNum + " AND ItemCode = " + itemCode;
        }

        /// <summary>
        /// Gets unique invoice numbers holding an item
        /// </summary>
        public string GetInvoiceTotal(string invoiceNum)
        {
            return "SELECT TotalCharge FROM Invoices WHERE InvoiceNum = '" + invoiceNum + "'";
        }

        /// <summary>
        /// Gets the cost of an item for a given item code
        /// </summary>
        public string GetItemCost(string itemCode)
        {
            return "SELECT ItemCost FROM Items WHERE ItemCode = '" + itemCode + "'";
        }

    }
}