using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cryptography
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        public string Encrypt(string korisnik, string plaintext)
        {
            string ciphertext = null;
            int j = 0, k = 0;
            char[] ptca = plaintext.ToCharArray();
            char[,] railarray = new char[2, 100];
            for (int i = 0; i < ptca.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    railarray[0, j] = ptca[i];
                    ++j;
                }
                else
                {
                    railarray[1, k] = ptca[i];
                    ++k;
                }
            }
            railarray[0, j] = '\0';
            railarray[1, k] = '\0';
            for (int x = 0; x < 2; ++x)
            {
                for (int y = 0; railarray[x, y] != '\0'; ++y)
                    ciphertext += railarray[x, y];
            }

            Sql sql = new Sql();
            sql.DodajTextISifru(korisnik, plaintext, ciphertext);
            return ciphertext;
        }
        public class TableDataRow
        {
            public TableDataRow(List<string> cells)
            {
                Cells = cells;
            }

            public List<string> Cells { get; }
        }

        public class TableData
        {
            public TableData(List<TableDataRow> rows)
            {
                Rows = rows;
            }

            public List<TableDataRow> Rows { get; }
        }


        public static class DataGridHelper
        {
            private static void TableDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var dataGrid = d as DataGrid;
                var tableData = e.NewValue as TableData;
                if (dataGrid != null && tableData != null)
                {
                    dataGrid.Columns.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        DataGridColumn column = new DataGridTextColumn
                        {
                            Binding = new Binding($"Cells[{i}]"),
                        };
                        dataGrid.Columns.Add(column);
                    }

                    dataGrid.ItemsSource = tableData.Rows;
                }
            }

            public static TableData GetTableData(DependencyObject obj)
            {
                return (TableData)obj.GetValue(TableDataProperty);
            }

            public static void SetTableData(DependencyObject obj, TableData value)
            {
                obj.SetValue(TableDataProperty, value);
            }

            // Using a DependencyProperty as the backing store for TableData.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty TableDataProperty =
                DependencyProperty.RegisterAttached("TableData",
                    typeof(TableData),
                    typeof(DataGridHelper),
                    new PropertyMetadata(null, TableDataChanged));
        }
    }
}
