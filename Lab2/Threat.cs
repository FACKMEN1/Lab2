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

    internal class Threat 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThreatSource { get; set; }
        public string ThreatObject { get; set; }
        public string PrivacyViolation { get; set; }
        public string IntegrityBreach { get; set; }
        public string AccessViolation { get; set; }


        public Threat(string id, string name, string description, string threatSource, string threatObject, bool privacyViolation, bool integrityBreach,
            bool accessViolation)
        {
            Id = id;
            Name = name;
            Description = description;
            ThreatSource = threatSource;
            ThreatObject = threatObject;
            PrivacyViolation = privacyViolation ? "Да" : "Нет";
            IntegrityBreach = integrityBreach ? "Да" : "Нет";
            AccessViolation = accessViolation ? "Да" : "Нет";

        }

        public Threat(string id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            Threat threat = obj as Threat;
            if (threat == null)
                return false;
            if (threat.Id == Id && threat.Name == Name && threat.Description == Description && threat.ThreatSource == ThreatSource && 
                threat.ThreatObject == ThreatObject && threat.PrivacyViolation == PrivacyViolation && threat.IntegrityBreach == IntegrityBreach && 
                threat.AccessViolation == AccessViolation)
                return true;
            return false;
        }

    }
}
