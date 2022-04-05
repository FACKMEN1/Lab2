using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ObservableCollection<Data> excelData;
        private static int range = 20;
        public MainWindow()
        {
            LoadData(range);
            InitializeComponent();  
            
            ExcelData.ItemsSource = excelData;
            
            
        }
        private static void LoadData(int count)
        {
            excelData = ExcelWorker.LoadFile(count, range -1);
            

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            
            ExcelData.Items.Refresh();
            
        }
    }
}
