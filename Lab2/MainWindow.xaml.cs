using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
        int pageIndex = 1;
        private int numberOfRecPerPage = 15;
        
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                var viewModel = new ViewModel();
                DataContext = viewModel; //А надо ли?
                ExcelData.ItemsSource = viewModel.Threats.Take(15);
            }catch (Exception ex)
            {
                var answer = MessageBox.Show("Файл не найден. Скачать файл?", "", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.No)
                    this.Close();
                else
                {
                    string remoteUri = "https://bdu.fstec.ru/files/documents/";
                    string fileName = "thrlist.xlsx", myStringWebResource = null;
                    
                    WebClient myWebClient = new WebClient();
                    
                    myStringWebResource = remoteUri + fileName;
                    
                    myWebClient.DownloadFile(myStringWebResource, fileName);
                    var viewModel = new ViewModel();
                    DataContext = viewModel; //А надо ли?
                    ExcelData.ItemsSource = viewModel.Threats.Take(15);
                }

                    
            }             

            Number_of_records.ItemsSource = new List<string> { "15", "25", "50", "Все"};
            Prev_Btn.IsEnabled = false;
            First_btn.IsEnabled = false;
        }
        /*private static void LoadData(int count)
        {
            threatData = ExcelWorker.LoadFile(count, lastIndex);
            threatData.CollectionChanged += ThreatCollectionChanged;

        }*/
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        //Переработать, чтобы id изменялось у записей (или добавлять сразу в конец?)
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            int id = GetId();
            //ExcelData.Items.Add(new Threat(id));
            ((ViewModel)DataContext).Threats.Add(new Threat(id));
            Navigate(PagingMode.Last);
            //ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip(((ViewModel)DataContext).Threats.Count - numberOfRecPerPage);
            //Current_page.Content = ((ViewModel)DataContext).Threats.Count.ToString() + " из " + ((ViewModel)DataContext).Threats.Count.ToString();
            //ExcelData.Items.Refresh();
            
        }
        private int GetId()
        {
            //Threat data = (Threat)ExcelData.Items[ExcelData.Items.Count - 1];
            //return data.Id + 1;
            return ((ViewModel)DataContext).Threats.Count + 1;
        }

        //Изменение данных в таблице (привести к MVVM)
        private static void ThreatCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    if (e.NewItems?[0] is Threat newThreat)
                    {

                    }
                        
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    if (e.OldItems?[0] is Threat oldThreat)
                    {

                    }
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    if ((e.NewItems?[0] is Threat replacingThreat) &&
                        (e.OldItems?[0] is Threat replacedThreat))
                    {

                    }
                        
                    break;
            }
        }

        #region Pagination
        //Привести все к MVVM
        private void First_btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PagingMode.First);
        }

        private void Prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PagingMode.Previous);
        }

        private void Number_of_records_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate(PagingMode.PageCountChange);
        }

        private void Last_btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PagingMode.Last);
        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PagingMode.Next);
        }

        private void Navigate(PagingMode mode)
        {
            int count;
            switch (mode)
            {
                case PagingMode.Next:
                    Prev_Btn.IsEnabled = true;
                    First_btn.IsEnabled = true;
                    if (((ViewModel) DataContext).Threats.Count > (pageIndex * numberOfRecPerPage))
                    {
                        if (((ViewModel)DataContext).Threats.Skip(pageIndex *
                                                                  numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            ExcelData.ItemsSource = null;
                            ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip((pageIndex *
                                                                numberOfRecPerPage) - numberOfRecPerPage)
                                .Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                                    (((ViewModel)DataContext).Threats.Skip(pageIndex *
                                                 numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            Next_btn.IsEnabled = false;
                            Last_btn.IsEnabled = false;
                        }
                        else
                        {
                            ExcelData.ItemsSource = null;
                            ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip(pageIndex *
                                                               numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                                    (((ViewModel)DataContext).Threats.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            pageIndex++;
                        }

                        Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
                    }

                    else
                    {
                        Next_btn.IsEnabled = false;
                        Last_btn.IsEnabled = false;
                    }

                    break;
                case PagingMode.Previous:
                    Next_btn.IsEnabled = true;
                    Last_btn.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        ExcelData.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage);
                            count = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage).Count();
                            Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
                            Prev_Btn.IsEnabled = false;
                            First_btn.IsEnabled = false;
                        }
                        else
                        {
                            ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip
                                (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(pageIndex * numberOfRecPerPage, ((ViewModel)DataContext).Threats.Count);
                            Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
                        }
                    }
                    else
                    {
                        Prev_Btn.IsEnabled = false;
                        First_btn.IsEnabled = false;
                    }

                    break;

                case PagingMode.First:
                    pageIndex = 2;
                    Navigate(PagingMode.Previous);
                    break;
                case PagingMode.Last:
                    pageIndex = ((ViewModel)DataContext).Threats.Count / numberOfRecPerPage;
                    Navigate(PagingMode.Next);
                    break;

                case PagingMode.PageCountChange:
                    pageIndex = 1;
                    if (!int.TryParse(Number_of_records.SelectedItem.ToString(), out numberOfRecPerPage))
                    {
                        numberOfRecPerPage = ((ViewModel) DataContext).Threats.Count;
                        Next_btn.IsEnabled = false;
                        Last_btn.IsEnabled = false;
                        Prev_Btn.IsEnabled = false;
                        First_btn.IsEnabled = false;
                    }
                    else
                    {
                        Next_btn.IsEnabled = true;
                        Last_btn.IsEnabled = true;
                        Prev_Btn.IsEnabled = false;
                        First_btn.IsEnabled = false;
                    }
                    ExcelData.ItemsSource = null;
                    ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage);
                    count = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage).Count();
                    Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;

                    break;
            }
            ExcelData.Items.Refresh();
        }


        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Save();
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Remove();
            ExcelData.Items.Refresh();
        }
    }
}
