using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumFinder.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public SearchViewModel()
        {
            SearchResults = new string[] { "test 1", "test 2", };
        }

        public IEnumerable<string> SearchResults { get; set; }
    }
}
