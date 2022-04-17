using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
        private enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };
        private const string remoteURL = "https://bdu.fstec.ru/files/documents/";
        private int page = -1;
        private ShortViewModel viewModel;
        private ExcelWorker excelWorker;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                excelWorker = new ExcelWorker();
                viewModel = new ShortViewModel(excelWorker);
                DataContext = viewModel;
            }
            catch (Exception ex)
            {
                var answer = MessageBox.Show("Файл не найден. Скачать файл?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.No)
                {
                    this.Close();
                    //return;
                }

                else
                {

                    string fileName = "thrlist.xlsx", myStringWebResource = null;

                    using (WebClient myWebClient = new WebClient())
                    {
                        myStringWebResource = remoteURL + fileName;

                        myWebClient.DownloadFile(myStringWebResource, fileName);
                    }

                    excelWorker = new ExcelWorker();
                    viewModel = new ShortViewModel(excelWorker);
                    DataContext = viewModel;
                    MessageBox.Show("Файл сохранен в " + Directory.GetFiles(Directory.GetCurrentDirectory().ToString(), "thrlist.xlsx", SearchOption.AllDirectories)[0].ToString());

                }


            }

            Number_of_records.ItemsSource = new List<string> { "15", "25", "50", "Все" };
            Prev_Btn.IsEnabled = false;
            First_btn.IsEnabled = false;
            viewModel.ItemsPerPage = int.Parse(Number_of_records.Text);
        }

        #region Pagination
        private void First_btn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FirstPage();
            ExcelData.ItemsSource = viewModel.Threats;
            ExcelData.Items.Refresh();
            page = 0;
            Prev_Btn.IsEnabled = false;
            First_btn.IsEnabled = false;
            Last_btn.IsEnabled = true;
            Next_btn.IsEnabled = true;

        }

        private void Prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ItemsPerPage = int.Parse(Number_of_records.SelectedValue.ToString());

            viewModel.PrevPage(--page, out bool isFirst);

            ExcelData.ItemsSource = viewModel.Threats;
            ExcelData.Items.Refresh();
            Next_btn.IsEnabled = true;
            Last_btn.IsEnabled = true;
            if (isFirst)
            {
                First_btn.IsEnabled = false;
                Prev_Btn.IsEnabled = false;
            }
        }

        private void Number_of_records_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (int.TryParse(Number_of_records.SelectedItem.ToString(), out int value))
            {
                viewModel.ItemsPerPage = value;
                Next_btn.IsEnabled = true;
                Last_btn.IsEnabled = true;
                page = 0;
                viewModel.FirstPage();
                ExcelData.ItemsSource = viewModel.Threats;
                ExcelData.Items.Refresh();
            }
            else
            {
                viewModel.ItemsPerPage = 0;
                Next_btn.IsEnabled = false;
                Prev_Btn.IsEnabled = false;
                First_btn.IsEnabled = false;
                Last_btn.IsEnabled = false;
                page = 1;
                viewModel.ShowAll();
            }

        }

        private void Last_btn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LastPage();
            Next_btn.IsEnabled = false;
            Last_btn.IsEnabled = false;
            First_btn.IsEnabled = true;
            Prev_Btn.IsEnabled = true;
            ExcelData.ItemsSource = viewModel.Threats;
            page = excelWorker.Data.Count / viewModel.ItemsPerPage;
            ExcelData.Items.Refresh();

        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ItemsPerPage = int.Parse(Number_of_records.SelectedValue.ToString());

            viewModel.NextPage(++page, out bool isLast);

            ExcelData.ItemsSource = viewModel.Threats;
            ExcelData.Items.Refresh();
            if (isLast)
            {
                Next_btn.IsEnabled = false;
                Last_btn.IsEnabled = false;
            }
            Prev_Btn.IsEnabled = true;
            First_btn.IsEnabled = true;
        }

        #endregion

        private void ExcelData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new FullInfo(viewModel.SelectedThreat.Id, excelWorker).Show();
        }

        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            new ChangeReport(excelWorker);
            ExcelData.Items.Refresh();
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                excelWorker.Save();
            }catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
            }
            
            
        }
    }
}
