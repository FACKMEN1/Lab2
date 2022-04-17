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
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для ChangeReport.xaml
    /// </summary>
    public partial class ChangeReport : Window
    {
        internal ChangeReport(ExcelWorker excelWorker)
        {
            InitializeComponent();
            
            try
            {
                var viewModel = new ChangeReportViewModel(excelWorker);
                DataContext = viewModel;
                if (viewModel.NewThreats.Count + viewModel.ChangeThreats.Count + viewModel.RemoveThreats.Count == 0)
                {
                    MessageBox.Show("У вас уже последняя версия.");
                    //Close();
                }
                else
                {
                    MessageBox.Show("Обновленно " + (viewModel.NewThreats.Count + viewModel.ChangeThreats.Count + viewModel.RemoveThreats.Count).ToString() + " записей");
                    if (viewModel.NewThreats.Count <= 0)
                    {
                        Based.Children.Remove(NewThreats);
                        NewThreats = null;
                        Based.Children.Remove(NewRecords);
                        NewRecords = null;
                        
                    }
                    if (viewModel.RemoveThreats.Count <= 0)
                    {
                        Based.Children.Remove(RemoveThreats);
                        RemoveThreats = null;
                        Based.Children.Remove(RemoveRecords);
                        RemoveRecords = null;
                    }
                    if (viewModel.ChangeThreats.Count <= 0)
                    {
                        Based.Children.Remove(ChangeThreats);
                        ChangeThreats = null;
                        Based.Children.Remove(ChangeRecords);
                        ChangeRecords = null;
                    }
                    
                    Show();
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                Close();
            }
            
        }
    }
}
