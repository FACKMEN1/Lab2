using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab2
{
    internal class FullInfoViewModel
    {
        public List<Threat> Threats { get; set; }
        private ExcelWorker excelWorker;
        public FullInfoViewModel(string id, ExcelWorker excelWorker)
        {
            this.excelWorker = excelWorker;
            string reallId = id.Split('.')[1];
            foreach (var threat in excelWorker.Data)
            {
                if (threat.Id == reallId)
                    Threats = new List<Threat> { threat };
            }            
        }
    }
}
