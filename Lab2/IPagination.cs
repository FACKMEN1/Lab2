using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal interface IPagination
    {
        int ItemsPerPage { get; set; }
        void NextPage(int page);
        void PrevPage(int page);
        void LastPage();
        void FirstPage();
    }
}
