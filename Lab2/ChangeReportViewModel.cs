using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ChangeReportViewModel
    {
        private List<Threat> newThreats = new List<Threat>();
        private List<Threat> removeThreats = new List<Threat>();
        private List<Threat> changeThreats = new List<Threat>();
        public List<Threat> NewThreats { get { return newThreats; } }
        public List<Threat> RemoveThreats { get { return removeThreats; } }
        public List<Threat> ChangeThreats { get { return changeThreats; } }
        private ExcelWorker excelWorker;
        public ChangeReportViewModel(ExcelWorker excelWorker)
        {
            this.excelWorker = excelWorker;
            
            Check();

        }

        private void Check()
        {
            var oldThreats = excelWorker.Data.ToList();
            
            string fileName = "thrlist.xlsx", myStringWebResource = null;
            string remoteURL = "https://bdu.fstec.ru/files/documents/";
            using (WebClient myWebClient = new WebClient())
            {
                myStringWebResource = remoteURL + fileName;

                myWebClient.DownloadFile(myStringWebResource, fileName);
            }
            excelWorker.Refresh();

            for (int i = 0; i < oldThreats.Count; i++)
            {
                bool exist = false;
                for (int j = i; j < excelWorker.Data.Count; j++)
                {

                    if (excelWorker.Data[j].Id == oldThreats[i].Id)
                    {
                        var threat = new Threat(oldThreats[i].Id);
                        bool diff = false;
                        if (excelWorker.Data[j].Name != oldThreats[i].Name)
                        {
                            threat.Name = excelWorker.Data[j].Name;
                            diff = true;
                        }

                        if (excelWorker.Data[j].Description != oldThreats[i].Description)
                        {
                            threat.Description = excelWorker.Data[j].Description;
                            diff = true;
                        }
                        if (excelWorker.Data[j].ThreatSource != oldThreats[i].ThreatSource)
                        {
                            threat.ThreatSource = excelWorker.Data[j].ThreatSource;
                            diff = true;
                        }
                        if (excelWorker.Data[j].ThreatObject != oldThreats[i].ThreatObject)
                        {
                            threat.ThreatObject = excelWorker.Data[j].ThreatObject;
                            diff = true;
                        }
                        if (excelWorker.Data[j].PrivacyViolation != oldThreats[i].PrivacyViolation)
                        {
                            threat.PrivacyViolation = excelWorker.Data[j].PrivacyViolation;
                            diff = true;
                        }
                        if (excelWorker.Data[j].IntegrityBreach != oldThreats[i].IntegrityBreach)
                        {
                            threat.IntegrityBreach = excelWorker.Data[j].IntegrityBreach;
                            diff = true;
                        }
                        if (excelWorker.Data[j].AccessViolation != oldThreats[i].AccessViolation)
                        {
                            threat.AccessViolation = excelWorker.Data[j].AccessViolation;
                            diff = true;
                        }

                        if (diff)
                        {
                            changeThreats.Add(threat);
                        }
                        exist = true;
                    }
                }
                if (!exist)
                    removeThreats.Add(oldThreats[i]);
            }
            for (int i = 0; i < excelWorker.Data.Count; i++)
            {
                if (!changeThreats.Contains(excelWorker.Data[i]) && !oldThreats.Contains(excelWorker.Data[i]))
                {
                    newThreats.Add(excelWorker.Data[i]);
                }
            }

        }
    }
}
