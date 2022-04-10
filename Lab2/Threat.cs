using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static OfficeOpenXml.ExcelErrorValue;


namespace Lab2
{

    internal class Threat : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThreatSource { get; set; }
        public string ThreatObject { get; set; }
        public bool PrivacyViolation { get; set; }
        public bool IntegrityBreach { get; set; }
        public bool AccessViolation { get; set; }
        public DateTime AddDate { get; set; }

        public DateTime ChangeDate { get; set; }

        public Threat(int id, string name, string description, string threatSource, string threatObject, bool privacyViolation, bool integrityBreach,
            bool accessViolation, string addDate, string changeDate)
        {
            Id = id;
            Name = name;
            Description = description;
            ThreatSource = threatSource;
            ThreatObject = threatObject;
            PrivacyViolation = privacyViolation;
            IntegrityBreach = integrityBreach;
            AccessViolation = accessViolation;
            AddDate = DateTime.Parse(addDate, new CultureInfo("ru-RU")).Date;
            ChangeDate = DateTime.Parse(changeDate, new CultureInfo("ru-RU")).Date;
        }

        public Threat(int id)
        {
            Id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
