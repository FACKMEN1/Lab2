using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        int pageIndex = 1;
        //private int numberOfRecPerPage = 15;
        private ViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                viewModel = new ViewModel();
                DataContext = viewModel; //А надо ли?

                //ExcelData.ItemsSource = viewModel.Threats.Take(15);
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
                    //ExcelData.ItemsSource = viewModel.Threats.Take(15);
                    
                }

                    
            }
            //Не реагирует на изменение
            ((ViewModel)DataContext).Threats.CollectionChanged += ThreatCollectionChanged;
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
            string id = GetId();
            //ExcelData.Items.Add(new Threat(id));
            ((ViewModel)DataContext).Threats.Add(new Threat(id));
            //Navigate(PagingMode.Last);
            //ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip(((ViewModel)DataContext).Threats.Count - numberOfRecPerPage);
            //Current_page.Content = ((ViewModel)DataContext).Threats.Count.ToString() + " из " + ((ViewModel)DataContext).Threats.Count.ToString();
            //ExcelData.Items.Refresh();
            
        }
        private string GetId()
        {
            //Threat data = (Threat)ExcelData.Items[ExcelData.Items.Count - 1];
            //return data.Id + 1;
            return (((ViewModel)DataContext).Threats.Count + 1).ToString();
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var threat = e.Row.Item as Threat;
            ((ViewModel)DataContext).EditThreat(threat);
        }

        //Изменение данных в таблице (привести к MVVM)
        private static void ThreatCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    if (e.NewItems?[0] is Threat newThreat)
                    {
                        ((ViewModel)GetWindow((DependencyObject)sender).DataContext).changeList[0].Add(newThreat);
                        
                    }
                        
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    if (e.OldItems?[0] is Threat oldThreat)
                    {
                        //((ViewModel)GetWindow((DependencyObject)sender).DataContext).changeList[1].Add(oldThreat as Threat);

                    }
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    if ((e.NewItems?[0] is Threat replacingThreat) &&
                        (e.OldItems?[0] is Threat replacedThreat))
                    {                     
                        ((ViewModel)GetWindow((DependencyObject)sender).DataContext).changeList[2].Add(replacedThreat);
                        ((ViewModel)GetWindow((DependencyObject)sender).DataContext).changeList[2].Add(replacingThreat);
                    }
                        
                    break;
            }
        }

        #region Pagination
        //Привести все к MVVM
        private void First_btn_Click(object sender, RoutedEventArgs e)
        {
            //Navigate(PagingMode.First);

        }

        private void Prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            //Navigate(PagingMode.Previous);
        }

        private void Number_of_records_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Navigate(PagingMode.PageCountChange);
            //int numberOfRecPerPage;
            //if (!int.TryParse(Number_of_records.SelectedItem.ToString(), out numberOfRecPerPage))
            //{
            //    //numberOfRecPerPage = ((ViewModel)DataContext).Threats.Count;
            //    ((ViewModel)DataContext).UpdateNumberOfRec(numberOfRecPerPage);
                
            //    Next_btn.IsEnabled = false;
            //    Last_btn.IsEnabled = false;
            //    Prev_Btn.IsEnabled = false;
            //    First_btn.IsEnabled = false;
            //}
            //else
            //{
            //    Next_btn.IsEnabled = true;
            //    Last_btn.IsEnabled = true;
            //    Prev_Btn.IsEnabled = false;
            //    First_btn.IsEnabled = false;
            //}
            //int count = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage).Count();
            //Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
            //((ViewModel)DataContext).Threats.CollectionChanged += ThreatCollectionChanged;
        }

        private void Last_btn_Click(object sender, RoutedEventArgs e)
        {
            //Navigate(PagingMode.Last);
        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            //Navigate(PagingMode.Next);

        }

        //private void Navigate(PagingMode mode)
        //{
        //    int count;
        //    switch (mode)
        //    {
        //        case PagingMode.Next:
        //            Prev_Btn.IsEnabled = true;
        //            First_btn.IsEnabled = true;
        //            if (((ViewModel) DataContext).Threats.Count > (pageIndex * numberOfRecPerPage))
        //            {
        //                if (((ViewModel)DataContext).Threats.Skip(pageIndex *
        //                                                          numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
        //                {
        //                    ExcelData.ItemsSource = null;
        //                    ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip((pageIndex *
        //                                                        numberOfRecPerPage) - numberOfRecPerPage)
        //                        .Take(numberOfRecPerPage);
        //                    count = (pageIndex * numberOfRecPerPage) +
        //                            (((ViewModel)DataContext).Threats.Skip(pageIndex *
        //                                         numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
        //                    Next_btn.IsEnabled = false;
        //                    Last_btn.IsEnabled = false;
        //                }
        //                else
        //                {
        //                    ExcelData.ItemsSource = null;
        //                    ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip(pageIndex *
        //                                                       numberOfRecPerPage).Take(numberOfRecPerPage);
        //                    count = (pageIndex * numberOfRecPerPage) +
        //                            (((ViewModel)DataContext).Threats.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
        //                    pageIndex++;
        //                }

        //                Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
        //            }

        //            else
        //            {
        //                Next_btn.IsEnabled = false;
        //                Last_btn.IsEnabled = false;
        //            }

        //            break;
        //        case PagingMode.Previous:
        //            Next_btn.IsEnabled = true;
        //            Last_btn.IsEnabled = true;
        //            if (pageIndex > 1)
        //            {
        //                pageIndex -= 1;
        //                ExcelData.ItemsSource = null;
        //                if (pageIndex == 1)
        //                {
        //                    ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage);
        //                    count = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage).Count();
        //                    Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
        //                    Prev_Btn.IsEnabled = false;
        //                    First_btn.IsEnabled = false;
        //                }
        //                else
        //                {
        //                    ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Skip
        //                        (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
        //                    count = Math.Min(pageIndex * numberOfRecPerPage, ((ViewModel)DataContext).Threats.Count);
        //                    Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;
        //                }
        //            }
        //            else
        //            {
        //                Prev_Btn.IsEnabled = false;
        //                First_btn.IsEnabled = false;
        //            }

        //            break;

        //        case PagingMode.First:
        //            pageIndex = 2;
        //            Navigate(PagingMode.Previous);
        //            break;
        //        case PagingMode.Last:
        //            pageIndex = ((ViewModel)DataContext).Threats.Count / numberOfRecPerPage;
        //            Navigate(PagingMode.Next);
        //            break;

        //        case PagingMode.PageCountChange:
        //            pageIndex = 1;
        //            if (!int.TryParse(Number_of_records.SelectedItem.ToString(), out numberOfRecPerPage))
        //            {
        //                numberOfRecPerPage = ((ViewModel) DataContext).Threats.Count;
        //                Next_btn.IsEnabled = false;
        //                Last_btn.IsEnabled = false;
        //                Prev_Btn.IsEnabled = false;
        //                First_btn.IsEnabled = false;
        //            }
        //            else
        //            {
        //                Next_btn.IsEnabled = true;
        //                Last_btn.IsEnabled = true;
        //                Prev_Btn.IsEnabled = false;
        //                First_btn.IsEnabled = false;
        //            }
        //            //ExcelData.ItemsSource = null;
        //            //ExcelData.ItemsSource = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage);
        //            //((ViewModel)DataContext).UpdateNumberOfRec(numberOfRecPerPage);
        //            count = ((ViewModel)DataContext).Threats.Take(numberOfRecPerPage).Count();
        //            Current_page.Content = count + " из " + ((ViewModel)DataContext).Threats.Count;

        //            break;
        //    }
        //    ExcelData.Items.Refresh();
        //}


        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ((ViewModel)DataContext).Save();
                var message = "";
                for (int i = 0; i < ((ViewModel)DataContext).changeList.Count; i++)
                {
                    if(((ViewModel)DataContext).changeList[i].Count != 0)
                    {
                        //message += 
                        foreach (var change in ((ViewModel)DataContext).changeList[i])
                        {
                            //message +=
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения данных. " + ex.Message);
            }
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Remove();
            ExcelData.Items.Refresh();
        }

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            
            //((ViewModel)DataContext)?.AllThreats();
            ExcelData.Columns.Clear();
            
            var id = new DataGridTextColumn();
            id.Header = "Id";
            id.Binding = new Binding("Id");
            var name = new DataGridTextColumn();
            name.Header = "Наименование угрозы";
            name.Binding = new Binding("Name");
            var description = new DataGridTextColumn();
            description.Header = "Описание";
            description.Binding = new Binding("Description");
            description.CanUserReorder = false;
            description.Width = DataGridLength.SizeToHeader;
            //description.ElementStyle.
            var source = new DataGridTextColumn();
            source.Header = "Источник угрозы";
            source.Binding = new Binding("ThreatSource");
            source.CanUserReorder = false;
            var threatObject = new DataGridTextColumn();
            threatObject.Header = "Объект воздействия";
            threatObject.Binding = new Binding("ThreatObject");
            threatObject.CanUserReorder = false;
            var privacyViolation = new DataGridCheckBoxColumn();
            privacyViolation.Header = "Нарушение конфиденциальности";
            privacyViolation.Binding = new Binding("PrivacyViolation");
            privacyViolation.CanUserReorder = false;
            var integrityBreach = new DataGridCheckBoxColumn();
            integrityBreach.Header = "Нарушение целостности";
            integrityBreach.Binding = new Binding("IntegrityBreach");
            integrityBreach.CanUserReorder = false;
            var accessViolation = new DataGridCheckBoxColumn();
            accessViolation.Header = "Нарушение доступности";
            accessViolation.Binding = new Binding("AccessViolation");
            accessViolation.CanUserReorder = false;
            var addDate = new DataGridTextColumn();
            addDate.Header = "Дата включения угрозы в БнД УБИ";
            addDate.Binding = new Binding("AddDate");
            addDate.CanUserReorder = false;
            addDate.Binding.StringFormat = "dd.MM.yyyy";
            var changeDate = new DataGridTextColumn();
            changeDate.Header = "Дата последнего изменения данных";
            changeDate.Binding = new Binding("ChangeDate");
            changeDate.CanUserReorder = false;
            changeDate.Binding.StringFormat = "dd.MM.yyyy";

            ExcelData.Columns.Add(id);
            ExcelData.Columns.Add(name);
            ExcelData.Columns.Add(description);
            ExcelData.Columns.Add(source);
            ExcelData.Columns.Add(threatObject);
            ExcelData.Columns.Add(privacyViolation);
            ExcelData.Columns.Add(accessViolation);
            ExcelData.Columns.Add(addDate);
            ExcelData.Columns.Add(changeDate);
            DataContext = viewModel;
            Add_btn.IsEnabled = true;
            Delete_btn.IsEnabled = true;
        }

        private void ShortChecked_Checked(object sender, RoutedEventArgs e)
        {
            //((ViewModel)DataContext).GetShortThreats();
            //ExcelData.ItemsSource = ((ViewModel)DataContext).ShortThreats;
            ExcelData.Columns.Clear();
            var id = new DataGridTextColumn();
            var name = new DataGridTextColumn();
            id.Header = "Id";
            id.Binding = new Binding("Id");
            id.IsReadOnly = true;
            id.Width = DataGridLength.Auto;
            name.Header = "Наименование угрозы";
            name.Binding = new Binding("Name");
            name.IsReadOnly = true;
            name.Width = DataGridLength.Auto;
            ExcelData.Columns.Add(id);
            ExcelData.Columns.Add(name);
            DataContext = new ShortViewModel(viewModel.Threats.ToList());
            Add_btn.IsEnabled = false;
            Delete_btn.IsEnabled = false;
            
        }
    }
}
