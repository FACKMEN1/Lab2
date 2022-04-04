using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ExcelWorker
    {
        private static FileInfo filePath = new FileInfo($"../../thrlist.xlsx");
        private static List<Data> data = new List<Data>();
        public List<Data> Data { get { return data; } }
        public static async Task<List<Data>> LoadFile(int count, int range)
        {
            List<Data> list = new List<Data>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var package = new ExcelPackage(filePath);
            await package.LoadAsync(filePath);
            var ws = package.Workbook.Worksheets[0];
            int row = count - range + 2;
            int col = 1;
            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false && range > 0)
            {
                int id = int.Parse(ws.Cells[row, col].Value.ToString());
                string threatName = ws.Cells[row, ++col].Value.ToString();
                string threatDescription = ws.Cells[row, ++col].Value.ToString();
                string threatSource = ws.Cells[row, ++col].Value.ToString();
                string threatObject = ws.Cells[row, ++col].Value.ToString();
                bool privacyViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool integrityBreach = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool accessViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
              
                DateTime addDate = DateTime.FromOADate(double.Parse(ws.Cells[row, ++col].Value.ToString()));
                DateTime changeDate = DateTime.FromOADate(double.Parse(ws.Cells[row, ++col].Value.ToString()));
                list.Add(new Data(id, threatName, threatDescription, threatSource, threatObject, privacyViolation, integrityBreach, accessViolation, addDate, changeDate));
                row++;
                col = 1;
                range--;
            }
            return list;
            
        }
        
    }
}
