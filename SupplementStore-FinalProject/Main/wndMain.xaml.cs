using SupplementStore_FinalProject.Items;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SupplementStore_FinalProject
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        //wndSearch wndSearch;
        wndItems wndItems;
        clsMainLogic mainLogic;

        private bool isNewInv = false;
        public wndMain()
        {
            InitializeComponent();

            //wndSearch = new wndSearch();
            wndItems = new wndItems();
            mainLogic = new clsMainLogic();

            PopulateItemsComboBox();
        }

        ////////////////////////////////////////////////////////////
        //////////////////////// NAVIGATION ////////////////////////
        ////////////////////////////////////////////////////////////
        public void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Open the search window
            //wndSearch.ShowDialog();

            PopulateItemsComboBox();
        }

        public void btnItems_Click(object sender, RoutedEventArgs e)
        {
            // Open the items window
            wndItems.ShowDialog();

            PopulateItemsComboBox();
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        ///////////////////// INVOICE SETTINGS /////////////////////
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handles the click event for the new invoice button.
        /// Creates a new invoice and enables the input fields for editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnNewInv_Click(object sender, RoutedEventArgs e)
        {
            // Update the UI to reflect the new invoice
            txtInvNumber.Text = "TBD";
            txtInvCost.Text = "0.00";
            grpMain.IsEnabled = true;
            isNewInv = true;
        }

        /// <summary>
        /// Handles the click event for the edit invoice button.
        /// Allows edit to the invoice details by enabling the input fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnEditInv_Click(object sender, RoutedEventArgs e)
        {
            grpMain.IsEnabled = true;
        }

        public void btnSaveInv_Click(object sender, RoutedEventArgs e)
        {
            // Untested
            if(isNewInv)
            {
                mainLogic.CreateNewInvoice(txtInvDate.Text, txtInvCost.Text);
                isNewInv = false;
            }
            else
            {
                mainLogic.UpdateInvoiceDetails(txtInvNumber.Text, txtInvDate.Text, txtInvCost.Text);
            }
            grpMain.IsEnabled = false;
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        ////////////////////// ITEM SETTINGS ///////////////////////
        ////////////////////////////////////////////////////////////
        public void cmbItemsChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbItems.SelectedItem != null)
            {
                //string itemDescription = cmbItems.SelectedItem.ToString();
                //string itemCode = mainLogic.GetItemCode(itemDescription); ///////// KEEPS CRASHING HERE
                //string itemCost = mainLogic.GetItemCost(itemCode);
                //txtItemCost.Text = itemCost;
            }
        }

        public void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            // Untested
            mainLogic.AddInvoiceItem(txtInvNumber.Text, mainLogic.GetItemCode(cmbItems.SelectedItem.ToString()), txtItemCost.Text);
            txtInvCost.Text = mainLogic.UpdateTotalCost(txtInvCost.Text, txtItemCost.Text, true);
        }

        public void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            // Untested
            mainLogic.RemoveInvoiceItem(txtInvNumber.Text, mainLogic.GetItemCode(cmbItems.SelectedItem.ToString()));
            txtInvCost.Text = mainLogic.UpdateTotalCost(txtInvCost.Text, txtItemCost.Text, false);
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        //////////////////// PRIVATE FUNCTIONS /////////////////////
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Populates the items combo box with all available items from the database.
        /// </summary>
        private void PopulateItemsComboBox()
        {
            cmbItems.Items.Clear();

            string items = mainLogic.GetAllItems();
            for (int i = 0; i < items.Split('\n').Length - 1; i++)
            {
                cmbItems.Items.Add(items.Split('\n')[i]);
            }

        }

        public void PopulateInvoiceDetails(string invoiceNum)
        {
            // This function will retrieve the invoice number from the search
            // window and populate the main window with the invoice details.
            // Understanding of how the search window works in needed before
            // this function can be implemented.

            //string details = mainLogic.GetInvoice(invoiceNum);
            //for (int i = 0; i < details.Split('\n').Length - 1; i++)
            //{
            //    // parses the details and populates the UI fields accordingly
            //}
        }

        // Must have comments about how after the search window
        // is opened, how the main window will extract the Invoice
        // ID from the search window.  Also needs another comment
        // that describes how the items combo box will be refreshed
        // if any items are modified/added on the items window.

        // should allow the user to create new invoices.

        // If a new invoice is created.
        // An invoice date will be assigned by the user.
        // Different items will be entered by the user.The items
        // will be selected from a drop-down box and the cost for that
        // item will be put into a read only textbox.  This will be
        // the default cost of an item. Once the item is selected,
        // the user can add the item.  As many items as needed should
        // be able to be added.All items entered should be displayed
        // for viewing in a list (something like a DataGrid).  Items
        // may be deleted from the list.A running total of the cost
        // of all items should be displayed as items are entered or
        // deleted.

        // Once all the items are entered the user can save the
        // invoice.Once the Invoice is saved, query the max invoice
        // number from the database, to display for the invoice number
        // (for our project, this will work, since the last inserted
        // invoice, will be the max).  This will lock the data in the
        // invoice for viewing only.From here the user may choose to
        // Edit the Invoice.

        /*
        - I should be able to select an item and click the Add item 
        button multiple time for the same item.

        - When I create a new invoice, the total charge updates in the search screen 
        becasue it is being saved to the db.

        - Main window does not has business logic behind the UI.

        - When creating an invoice, the Invoice number should be given

        - Requirement to have a menu control to get to the search and edit screens.

        - After an invoice is selected it should be displayed in read only mode 
        and an edit button needs to be clicked.

        - After a new invoice is created and saved, it should be displayed in 
        read only mode.

        - Main window items need to be refreshed after items have been 
        updated/deleted/added on the items window.

        - Shouldn't be able to save an invoice without entering a date.

        - An invoice date will also be assigned by the user.

        - After an invoice is searched for and selected, the main window 
        should display it in a read only mode, then an edit button needs 
        to be clicked to edit it.
        */

    }
}