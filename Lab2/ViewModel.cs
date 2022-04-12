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
    internal class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Threat> Threats { get; set; }
        private Threat[] originalThreats;
        public event PropertyChangedEventHandler PropertyChanged;
        private Threat selectedThreat;
        private ExcelWorker excelWorker;
        public Threat SelectedThreat
        {
            get { return selectedThreat; }
            set
            {
                selectedThreat = value;
                OnPropertyChanged("SelectedThreat");
            }
        }


        public ViewModel()
        {
            excelWorker = new ExcelWorker();
           
            Threats = excelWorker.LoadFile();
            
            
            originalThreats = Threats.ToArray();
        }

        public void Remove()
        {
            if (SelectedThreat != null)
                Threats.Remove(SelectedThreat);
        }

        public void Save()
        {
            /*var diffThreats = new List<object>();
            if (originalThreats.Length > Threats.Count)
            {
                for (int i = 0; i < originalThreats.Length; i++)
                {
                    if (!Threats.Contains(originalThreats[i]))
                    {
                        if (i < Threats.Count)
                        {
                            if (Threats[i].Id == originalThreats[i].Id)
                            {
                                string original = "";
                                string diff = "";
                                if (Threats[i].Name != originalThreats[i].Name)
                                {
                                    diff += Threats[i].Name;
                                    original += originalThreats[i].Name;
                                }
                                if (Threats[i].Description != originalThreats[i].Description)
                                {
                                    diff += Threats[i].Description;
                                    original += originalThreats[i].Description;
                                }
                                if ()
                            }
                            else
                            {
                                diffThreats.Add(null);
                                diffThreats.Add(originalThreats[i]);
                            }
                        }
                    }
                        
                    
                }
            }*/
            excelWorker.Save(Threats.ToList<Threat>());

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
