using System;
using System.Data;
using System.Windows;

namespace SupplementStore_FinalProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        private clsSearchLogic searchLogic = new clsSearchLogic();

        public int? SelectedInvoiceNum { get; private set; }

        private bool isLoading = false;

        public wndSearch()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                isLoading = true;

                dgInvoices.ItemsSource = searchLogic.GetInvoices(null, null, null).DefaultView;
                
                // Populate the three filter
                cboInvoiceNum.ItemsSource =
                    searchLogic.GetInvoiceNums().DefaultView;
                cboInvoiceNum.DisplayMemberPath = "InvoiceNum";
                cboInvoiceNum.SelectedValuePath = "InvoiceNum";

                cboInvoiceDate.ItemsSource =
                    searchLogic.GetInvoiceDates().DefaultView;
                cboInvoiceDate.DisplayMemberPath = "InvoiceDate";
                cboInvoiceDate.SelectedValuePath = "InvoiceDate";

                cboTotalCost.ItemsSource =
                    searchLogic.GetInvoiceCosts().DefaultView;
                cboTotalCost.DisplayMemberPath = "TotalCharge";
                cboTotalCost.SelectedValuePath = "TotalCharge";

                // Start with no filters
                cboInvoiceNum.SelectedIndex = -1;
                cboInvoiceDate.SelectedIndex = -1;
                cboTotalCost.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoices: " + ex.Message);
            }
            finally { isLoading = false; }
        }

        private void Filter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (isLoading)
                return;

            try
            {
                string invoiceNum = null;
                DateTime? invoiceDate = null;
                decimal? totalCharge = null;

                if (cboInvoiceNum.SelectedValue != null)
                    invoiceNum = cboInvoiceNum.SelectedValue.ToString();

                if (cboInvoiceDate.SelectedValue != null)
                    invoiceDate = Convert.ToDateTime(
                        cboInvoiceDate.SelectedValue);

                if (cboTotalCost.SelectedValue != null)
                    totalCharge = Convert.ToDecimal(
                        cboTotalCost.SelectedValue);

                dgInvoices.ItemsSource =
                    searchLogic.GetInvoices(
                        invoiceNum, invoiceDate, totalCharge).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering invoices: " + ex.Message);
            }
        }

        // Selects the highlighted Invoice
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgInvoices.SelectedItem == null)
            {
                MessageBox.Show("Please select an invoice first.");
                return;
            }

            DataRowView selectedRow =
                (DataRowView)dgInvoices.SelectedItem;

            SelectedInvoiceNum =
                Convert.ToInt32(selectedRow["InvoiceNum"]);

            DialogResult = true;
            Close();
        }

        // Closes without selecting an invoice
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedInvoiceNum = null;
            DialogResult = false;
            Close();
        }
    }
}
