using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab2
{
    internal class ShortViewModel
    {
        private List<ShortThreat> shortThreatList = new List<ShortThreat>();
        private List<ShortThreat> allThreats = new List<ShortThreat>();
        
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
        private int itemsPerPage = 15;
        public int ItemsPerPage { get { return itemsPerPage; } set { itemsPerPage = value; } }

        public ShortViewModel(ExcelWorker excelWorker)
        {
            var threats = excelWorker.Data;
            foreach (Threat threat in threats)
            {
                allThreats.Add(new ShortThreat("УБИ." + threat.Id, threat.Name));
            }
            Threats = allThreats.Take(ItemsPerPage).ToList();
        }

        #region Pagination
        public void NextPage(int page, out bool isLast)
        {
            isLast = false;
            if (allThreats.Count > (page * ItemsPerPage))
            {
                if (allThreats.Skip(page * ItemsPerPage).Take(ItemsPerPage).Count() == 0)
                {
                    
                    Threats = allThreats.Skip((page * ItemsPerPage) - page).Take(ItemsPerPage).ToList<ShortThreat>();
                }
                else
                {
                    Threats = allThreats.Skip(page * ItemsPerPage).Take(ItemsPerPage).ToList();
                }
            }
            else
            {
                isLast = true;
            }
            
        }

        public void PrevPage(int page, out bool isFirst)
{
            isFirst = false;
            if (page != 0)
            {
                if (allThreats.Skip(page * ItemsPerPage).Take(ItemsPerPage).Count() == 0)
                {
                    
                    Threats = allThreats.Skip((page * ItemsPerPage) - page).Take(ItemsPerPage).ToList<ShortThreat>();
                }
                else
                {
                    Threats = allThreats.Skip(page * ItemsPerPage).Take(ItemsPerPage).ToList();
                }
            }
            else
            {
                Threats = allThreats.Skip(page * ItemsPerPage).Take(ItemsPerPage).ToList();
                isFirst = true;
            }
        }

        public void LastPage()
{
            Threats = allThreats.Skip(allThreats.Count - ItemsPerPage).Take(ItemsPerPage).ToList();
        }

        public void FirstPage()
        {
            Threats = allThreats.Take(ItemsPerPage).ToList();
        }

        public void ShowAll()
        {
            Threats = allThreats.Take(allThreats.Count).ToList();
        }

        #endregion

    }
}
