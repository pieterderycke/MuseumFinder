using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumFinder.ViewModels
{
    public class SearchResultViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
