using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab2
{

    internal class Data
    {
        public int Id { get; set; }
        public string ThreatName { get; set; }
        public string ThreatDescription { get; set; }
        public string ThreatSource { get; set; }
        public string ThreatObject { get; set; }
        public bool PrivacyViolation { get; set; }
        public bool IntegrityBreach { get; set; }
        public bool AccessViolation { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ChangeDate { get; set; }

        public Data(int id, string threatName, string threatDescription, string threatSource, string threatObject, bool privacyViolation, bool integrityBreach,
            bool accessViolation, DateTime addDate, DateTime changeDate)
        {
            Id = id;
            ThreatName = threatName;
            ThreatDescription = threatDescription;
            ThreatSource = threatSource;
            ThreatObject = threatObject;
            PrivacyViolation = privacyViolation;
            IntegrityBreach = integrityBreach;
            AccessViolation = accessViolation;
            AddDate = addDate;
            ChangeDate = changeDate;
        }

        public Data(int id)
        {
            Id = id;
        }

    }
}
