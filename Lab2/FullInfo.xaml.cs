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
    /// Логика взаимодействия для FullInfo.xaml
    /// </summary>
    public partial class FullInfo : Window
    {
        internal FullInfo(string id, ExcelWorker excelWorker)
        {
            InitializeComponent();
            DataContext = new FullInfoViewModel(id, excelWorker);
        }
    }
}
