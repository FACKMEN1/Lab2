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
    internal class ViewModel : INotifyPropertyChanged, IPagination
    {
        private int numberOfRecPerPage = 15;
        public ObservableCollection<Threat> Threats { get; set; }
        public int ItemsPerPage { get; set; }

        private List<Threat> newThreats = new List<Threat>();
        private List<Threat> removeThreats = new List<Threat>();
        private List<Threat> changeThreats = new List<Threat>();
        public List<List<Threat>> changeList = new List<List<Threat>>();

        private List<Threat> originalThreats;
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
           
            
            //ShortThreats = ToShort(Threats);
            
            
            Threats = excelWorker.LoadFile();
            originalThreats = Threats.ToList<Threat>();
            changeList.Add(newThreats);
            changeList.Add(removeThreats);
            changeList.Add(changeThreats);
        }

        /*private List<Threat> ToShort(ObservableCollection<Threat> threats)
        {
            var shortList = new List<Threat>();
            foreach (Threat threat in threats)
            {
                shortList.Add(threat);
            }
            return shortList;
        }*/

        public void Remove()
        {
            if (changeList[1].Contains(SelectedThreat))
                changeList[1].Remove(SelectedThreat);
            if (changeList[0].Contains(SelectedThreat))
                changeList[0].Remove(SelectedThreat);
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

        public void UpdateNumberOfRec(int numberOfRecPerPage)
        {
            this.numberOfRecPerPage = numberOfRecPerPage;
        }

        public void EditThreat(Threat threat)
        {
            if (!originalThreats.Contains(threat))
            {
                if (!changeList[2].Contains(threat))
                    changeList[2].Add(threat);
                else
                {
                    var index = changeList[2].IndexOf(threat);
                    changeList[2][index] = threat;
                }
                    
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void NextPage(int page)
        {
            if (originalThreats.Count > (page * ItemsPerPage))
            {
                if (originalThreats.Skip(page * ItemsPerPage).Take(ItemsPerPage).Count() == 0)
                {
                    //Threats = originalThreats.Skip((page * ItemsPerPage) - page).Take(ItemsPerPage);
                                       
                }
            }
        }

        public void PrevPage(int page)
        {
            throw new NotImplementedException();
        }

        public void LastPage()
        {
            throw new NotImplementedException();
        }

        public void FirstPage()
        {
            throw new NotImplementedException();
        }
    }
}
