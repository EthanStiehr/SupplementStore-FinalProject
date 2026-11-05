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
        public wndMain()
        {
            InitializeComponent();

            //wndSearch = new wndSearch();
            wndItems = new wndItems();
        }

        ////////////////////////////////////////////////////////////
        //////////////////////// NAVIGATION ////////////////////////
        ////////////////////////////////////////////////////////////
        public void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Open the search window
            //wndSearch.ShowDialog();

            RefreshItemsComboBox();
        }

        public void btnItems_Click(object sender, RoutedEventArgs e)
        {
            // Open the items window
            wndItems.ShowDialog();

            RefreshItemsComboBox();
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        ///////////////////// INVOICE SETTINGS /////////////////////
        ////////////////////////////////////////////////////////////
        public void btnEditInv_Click(object sender, RoutedEventArgs e)
        {
            grpMain.IsEnabled = true;
        }

        public void btnSaveInv_Click(object sender, RoutedEventArgs e)
        {
            // Save the invoice details to database

            grpMain.IsEnabled = false;
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        ////////////////////// ITEM SETTINGS ///////////////////////
        ////////////////////////////////////////////////////////////
        public void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            // Adding item to database
        }

        public void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            // Remove item from database
        }
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////
        //////////////////// PRIVATE FUNCTIONS /////////////////////
        ////////////////////////////////////////////////////////////
        private void RefreshItemsComboBox()
        {
            // Reload the Items from the Database
        }

        // Must have comments about how after the search window
        // is opened, how the main window will extract the Invoice
        // ID from the search window.  Also needs another comment
        // that describes how the items combo box will be refreshed
        // if any items are modified/added on the items window.

        // should allow the user to create new invoices or edit
        // existing invoices.  There should be just one window for
        // all functionality of the main window.  So, the main window
        // will NOT open other windows to add/edit invoices.  It will
        // also have a menu (at the top, use a menu control) that will
        // have two functionalities.  The first will allow the user to
        // update a def table that contains the items.  The next will
        // be to open a search screen used to search for invoices.

        // If a new invoice is created the user may enter data
        // pertaining to that invoice.Since an auto-generated number
        // is used in the database for the invoice number, when a user
        // creates a new invoice, just display “TBD” for the Invoice
        // Number.An invoice date will also be assigned by the user.
        // Next different items will be entered by the user.The items
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