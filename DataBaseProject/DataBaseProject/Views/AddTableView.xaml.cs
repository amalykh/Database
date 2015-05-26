using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBaseProject.Views
{
    /// <summary>
    /// Interaction logic for AddTableView.xaml
    /// </summary>
    public partial class AddTableView : Window
    {
        public AddTableView()
        {
            InitializeComponent();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            if (e.Column.Header.ToString() == "Name")
            {
                e.Column.Width = 100;
            }

            if (e.Column.Header.ToString() == "Reference")
            {
                e.Column.Width = 100;
            }

            if (e.Column.Header.ToString() == "PrimaryKey")
            {
                e.Column.Header = "Primary key";
            }

            if (e.Column.Header.ToString() == "Type")
            {
                e.Column.Width = 100;

                /*var column = new DataGridComboBoxColumn();

                DataGrid.

                List<string> l = new List<string>();
                l.Add("awa");
                l.Add("Eva");

                column.ItemsSource = l;
                column.Width = e.Column.Width;
                column.Header = e.Column.Header;
                column.IsReadOnly = e.Column.IsReadOnly;
                column.HeaderStyle = e.Column.HeaderStyle;
                column.HeaderTemplate = e.Column.HeaderTemplate;
                column.Visibility = e.Column.Visibility;

                e.Column = column;*/

                /*Binding comboBind = new Binding("Account");
                comboBind.Mode = BindingMode.OneWay;

                FrameworkElementFactory comboFactory = new FrameworkElementFactory(typeof(ComboBox));
                comboFactory.SetValue(ComboBox.IsTextSearchEnabledProperty, true);
                //comboFactory.SetValue(ComboBox.ItemsSourceProperty, this.Accounts);
                comboFactory.SetBinding(ComboBox.SelectedItemProperty, comboBind);

                DataTemplate comboTemplate = new DataTemplate();
                comboTemplate.VisualTree = comboFactory;

                e.Column.CellStyle.HeaderTemplate = comboTemplate;*/
            }
        }
    }
}
