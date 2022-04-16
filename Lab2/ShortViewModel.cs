using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ShortViewModel
    {
        private List<ShortThreat> shortThreatList = new List<ShortThreat>();
        private ShortThreat selectedThreat;
        public ShortThreat SelectedThreat
        {
            get { return selectedThreat; }
            set
            {
                selectedThreat = value;
            }
        }
        public List<ShortThreat> Threats
        {
            get
            {
                return shortThreatList;
            }

            set
            {
                shortThreatList = value;
            }
        }
        
        public ShortViewModel(List<Threat> threats)
        {

            foreach (Threat threat in threats)
            {
                Threats.Add(new ShortThreat("УБИ." + threat.Id, threat.Name));
            }
        }
    }
}
