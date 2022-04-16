using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ChangeWindowViewModel
    {
        private List<List<Threat>> threats;
        public ChangeWindowViewModel(object threats)
        {
            this.threats = threats as List<List<Threat>>;
        }
    }
}
