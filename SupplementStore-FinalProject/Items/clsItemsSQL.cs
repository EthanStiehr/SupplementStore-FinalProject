
namespace SupplementStore_FinalProject.Items
{
    /// <summary>
    /// holds SQL statements for "Items Window"
    /// </summary>
    public class clsItemsSQL
    {
        /// <summary>
        /// selects supplements ordered by item code
        /// </summary>
        public string SelectAllItems()
        {
            return "SELECT ItemCode, ItemDescription, ItemCost " +
                   "FROM Items ORDER BY ItemCode";
        }

        /// <summary>
        /// inserts new supplement
        /// </summary>
        public string InsertItem()
        {
            return "INSERT INTO Items " +
                   "(ItemCode, ItemDescription, ItemCost) " +
                   "VALUES (?, ?, ?)";
        }

        /// <summary>
        /// updates supplement's desc and cost
        /// PK cannot be changed
        /// </summary>
        public string UpdateItem()
        {
            return "UPDATE Items SET ItemDescription = ?, " +
                   "ItemCost = ? WHERE ItemCode = ?";
        }

        /// <summary>
        /// delete supplement by item code
        /// </summary>
        public string DeleteItem()
        {
            return "DELETE FROM Items WHERE ItemCode = ?";
        }

        /// <summary>
        /// selects invoices containing a supplement
        /// </summary>
        public string SelectItemInvoices()
        {
            return "SELECT DISTINCT InvoiceNum " +
                   "FROM InvoiceItems WHERE ItemCode = ? " +
                   "ORDER BY InvoiceNum";
        }

        /// <summary>
        /// checks if an item code is a dupe
        /// </summary>
        public string ItemExists()
        {
            return "SELECT COUNT(*) FROM Items WHERE ItemCode = ?";
        }
    }
}
