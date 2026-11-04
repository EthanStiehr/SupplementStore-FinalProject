using System;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace SupplementStore_FinalProject.Items
{
    /// <summary>
    /// logic for supplement "Items Window"
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// holds business logic for supplement management
        /// </summary>
        private readonly clsItemsLogic logic = new clsItemsLogic();

        /// <summary>
        /// notify "Main Window" if supplements were changed
        /// </summary>
        public bool ItemsModified { get; private set; } = false;

        /// <summary>
        /// creates "Items Window" and loads supplements
        /// </summary>
        public wndItems()
        {
            InitializeComponent();
            LoadItems();
        }

        /// <summary>
        /// loads all supplements
        /// </summary>
        private void LoadItems()
        {
            try
            {
                dgItems.ItemsSource =
                    logic.GetItems().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Database Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// displays selected supplement in item field
        /// </summary>
        private void dgItems_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem is DataRowView row)
                {
                    txtItemCode.Text =
                        row["ItemCode"].ToString();

                    txtDescription.Text =
                        row["ItemDescription"].ToString();

                    decimal cost =
                        Convert.ToDecimal(row["ItemCost"]);

                    txtCost.Text = cost.ToString(
                        "F2", CultureInfo.CurrentCulture);

                    // item code is the PK
                    // users cannot change it for existing supplement
                    txtItemCode.IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Selection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// checks and converts entered cost
        /// </summary>
        private decimal GetCost()
        {
            try
            {
                if (!decimal.TryParse(
                    txtCost.Text,
                    NumberStyles.Currency,
                    CultureInfo.CurrentCulture,
                    out decimal cost))
                {
                    throw new Exception(
                        "Please enter a valid item cost.");
                }

                if (decimal.Round(cost, 2) != cost)
                {
                    throw new Exception(
                        "Item cost cannot have more than two decimal places.");
                }

                return cost;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// adss new supplement to inv
        /// </summary>
        private void btnAdd_Click(
            object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem != null)
                {
                    MessageBox.Show(
                        "Click 'Clear' before adding a new supplement.");
                    return;
                }

                logic.AddItem(
                    txtItemCode.Text.Trim(),
                    txtDescription.Text.Trim(),
                    GetCost());

                ItemsModified = true;

                LoadItems();
                ClearFields();

                MessageBox.Show(
                    "Supplement added successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Add Item Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// updates description and cost of selected supplement
        /// </summary>
        private void btnUpdate_Click(
            object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                {
                    MessageBox.Show(
                        "Select a supplement to update.");
                    return;
                }

                // item code is "locked" when supplement is selected
                // the description and cost will still update
                logic.UpdateItem(
                    txtItemCode.Text.Trim(),
                    txtDescription.Text.Trim(),
                    GetCost());

                ItemsModified = true;

                LoadItems();
                ClearFields();

                MessageBox.Show(
                    "Supplement updated successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Update Item Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// deletes supplement if not used on invoice
        /// </summary>
        private void btnDelete_Click(
            object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgItems.SelectedItem == null)
                {
                    MessageBox.Show(
                        "Select a supplement to delete.");
                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this supplement?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                {
                    return;
                }

                logic.DeleteItem(
                    txtItemCode.Text.Trim());

                ItemsModified = true;

                LoadItems();
                ClearFields();

                MessageBox.Show(
                    "Supplement deleted successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Delete Item Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// clears selected supplement and txt
        /// </summary>
        private void ClearFields()
        {
            try
            {
                dgItems.SelectedItem = null;

                txtItemCode.Clear();
                txtDescription.Clear();
                txtCost.Clear();

                txtItemCode.IsReadOnly = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// holds 'Clear' btn
        /// </summary>
        private void btnClear_Click(
            object sender, RoutedEventArgs e)
        {
            try
            {
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Clear Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// closes "Items Window" and returns to "Main"
        /// </summary>
        private void btnClose_Click(
            object sender, RoutedEventArgs e)
        {
            try
            {
                // "Main Window" will open this using ShowDialog()

                // if supplement is added, updated, or deleted,
                // ItemsModified will be - true

                // when window closes, main will access ItemsModified

                // ItemsModified is true = "Main" will reload
                // supplements into box
                
                // "Main" also reload current invoice
                // because item's description could change

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Close Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
