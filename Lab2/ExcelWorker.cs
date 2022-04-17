using Microsoft.Win32;
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
    internal class ExcelWorker
    {
        private List<Threat> data;

        public string filePath;

        public List<Threat> Data { get { return data; } }

        public ExcelWorker()
        {
            filePath = Directory.GetFiles(Directory.GetCurrentDirectory().ToString(), "thrlist.xlsx", SearchOption.AllDirectories)[0];
            data = LoadFile();

        }
        public void Refresh()
        {
            filePath = Directory.GetFiles(Directory.GetCurrentDirectory().ToString(), "thrlist.xlsx", SearchOption.AllDirectories)[0];
            data = LoadFile();
        }

        //Считывается весь файл.
        public List<Threat> LoadFile()
        {
            var list = new List<Threat>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var package = new ExcelPackage(filePath);

            var file = new FileStream(filePath, FileMode.Open);
            package.Load(file);
            var ws = package.Workbook.Worksheets[0];
            int row = 3;
            int col = 1;

            while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
            {
                string id = ws.Cells[row, col].Value.ToString();
                string threatName = ws.Cells[row, ++col].Value.ToString();
                string threatDescription = ws.Cells[row, ++col].Value.ToString();
                string threatSource = ws.Cells[row, ++col].Value.ToString();
                string threatObject = ws.Cells[row, ++col].Value.ToString();
                bool privacyViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool integrityBreach = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));
                bool accessViolation = Convert.ToBoolean(int.Parse(ws.Cells[row, ++col].Value.ToString()));

                list.Add(new Threat(id, threatName, threatDescription, threatSource, threatObject, privacyViolation, integrityBreach, accessViolation));
                row++;
                col = 1;
            }
            file.Close();
            ws.Dispose();
            package.Dispose();
            
            return list;

        }

        public void Save()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = @"thrlist.xlsx";
            save.Filter = "XLSX File (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            save.RestoreDirectory = true;
            
            try
            {
                save.ShowDialog();
                var package = new ExcelPackage();
                package.Workbook.Worksheets.Add("Sheet");
                var ws = package.Workbook.Worksheets[0];
                int row = 1;
                int col = 1;
                ws.Cells[row, col, row, col + 4].Style.Font.Bold = true;
                ws.Cells[row, col, row, col + 4].Merge = true;
                ws.Cells[row, col, row, col + 4].Value = "Общая информация";
                ws.Cells[row, col, row, col + 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                col += 5;
                ws.Cells[row, col, row, col + 2].Style.Font.Bold = true;
                ws.Cells[row, col, row, col + 2].Merge = true;
                ws.Cells[row, col, row, col + 2].Value = "Последствия";
                ws.Cells[row, col, row, col + 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                col = 1;
                row++;
                
                ws.Cells[row, col].Value = "Идентификатор УБИ";
                ws.Cells[row, col++].AutoFitColumns();
                ws.Cells[row, col].Value = "Наименование УБИ";
                ws.Cells[row, col++].AutoFitColumns();
                ws.Cells[row, col].Value = "Описание";
                ws.Cells[row, col++].AutoFitColumns();
                ws.Cells[row, col].Value = "Источник угрозы (характеристика и потенциал нарушителя)";
                ws.Cells[row, col++].AutoFitColumns();
                ws.Cells[row, col].Value = "Объект воздействия";
                ws.Cells[row, col++].AutoFitColumns();
                ws.Cells[row, col++].Value = "Нарушение конфиденциальности";
                
                ws.Cells[row, col++].Value = "Нарушение целостности";
                
                ws.Cells[row++, col++].Value = "Нарушение доступности";
                
                col = 1;
                foreach (Threat threat in data)
                {
                    ws.Cells[row, col++].Value = threat.Id;
                    ws.Cells[row, col++].Value = threat.Name;
                    ws.Cells[row, col++].Value = threat.Description;
                    ws.Cells[row, col++].Value = threat.ThreatSource;
                    ws.Cells[row, col++].Value = threat.ThreatObject;
                    ws.Cells[row, col++].Value = threat.PrivacyViolation;
                    ws.Cells[row, col++].Value = threat.IntegrityBreach;
                    ws.Cells[row++, col++].Value = threat.AccessViolation;
                    col = 1;
                    
                }
                package.SaveAs(save.FileName);
                ws.Dispose();
                
                
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
