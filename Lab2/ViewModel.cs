using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Threat> Threats { get; set; } 
        public event PropertyChangedEventHandler PropertyChanged;
        private Threat selectedThreat;
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
            Threats = ExcelWorker.LoadFile();
            
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
