using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ExcelWorker : INotifyPropertyChanged
    {
        private static string filePath = $"../../thrlist.xlsx";
        private static List<Threat> data = new List<Threat>();

        /*private Threat selectedThreat;
        public Threat SelectedThreat
        {
            get { return selectedThreat; }
            set
            {
                selectedThreat = value;
                OnPropertyChanged("SelectedThreat");
            }
        }*/

        public List<Threat> Data { get { return data; } }

        public event PropertyChangedEventHandler PropertyChanged;

        //Считывается количество строк в файле начиная с переданного индекса.
        public static ObservableCollection<Threat> LoadFile(int count, int lastIndex) 
        {
            ObservableCollection<Threat> list = new ObservableCollection<Threat>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var package = new ExcelPackage(filePath);
            //await package.LoadAsync(filePath);
            package.Load(new FileStream(filePath, FileMode.Open));
            var ws = package.Workbook.Worksheets[0];
            int row = count - lastIndex + 1;
            int col = 1;

            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false && count > 0)
            {
                int id = int.Parse(ws.Cells[row, col].Value.ToString());
                string threatName = ws.Cells[row, ++col].Value.ToString();
                string threatDescription = ws.Cells[row, ++col].Value.ToString();
                string threatSource = ws.Cells[row, ++col].Value.ToString();
                string threatObject = ws.Cells[row, ++col].Value.ToString();
                bool privacyViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool integrityBreach = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool accessViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
              
                string addDate = DateTime.FromOADate(double.Parse(ws.Cells[row, ++col].Value.ToString())).ToString("d");
                string changeDate = DateTime.FromOADate(double.Parse(ws.Cells[row, ++col].Value.ToString())).ToString("d");
                list.Add(new Threat(id, threatName, threatDescription, threatSource, threatObject, privacyViolation, integrityBreach, accessViolation, addDate, changeDate));
                row++;
                col = 1;
                count--;
            }
            return list;
            
        }

        //Считывается весь файл.
        public static ObservableCollection<Threat> LoadFile() 
        {
            ObservableCollection<Threat> list = new ObservableCollection<Threat>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var package = new ExcelPackage(filePath);
            //await package.LoadAsync(filePath);
            package.Load(new FileStream(filePath, FileMode.Open));
            var ws = package.Workbook.Worksheets[0];
            int row = 3;
            int col = 1;

            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
            {
                int id = int.Parse(ws.Cells[row, col].Value.ToString());
                string threatName = ws.Cells[row, ++col].Value.ToString();
                string threatDescription = ws.Cells[row, ++col].Value.ToString();
                string threatSource = ws.Cells[row, ++col].Value.ToString();
                string threatObject = ws.Cells[row, ++col].Value.ToString();
                bool privacyViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool integrityBreach = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool accessViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));

                string addDate = DateTime.FromOADate(double.Parse(ws.Cells[row, ++col].Value.ToString())).ToString("d");
                string changeDate = DateTime.FromOADate(double.Parse(ws.Cells[row, ++col].Value.ToString())).ToString("d");
                list.Add(new Threat(id, threatName, threatDescription, threatSource, threatObject, privacyViolation, integrityBreach, accessViolation,
                    addDate, changeDate));
                row++;
                col = 1;
            }

            return list;

        }

        public static List<string> GetHeaders(ExcelWorksheet ws)
        {
            var list = new List<string>();
            
            
            int row = 2;
            int col = 1;
            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
                list.Add(ws.Cells[row, col++].Value.ToString());

            return list;
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
