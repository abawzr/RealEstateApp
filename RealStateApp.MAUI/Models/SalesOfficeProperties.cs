using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.MAUI.Models
{
    public class SalesOfficeProperties
    {
        public string OfficeName { get; set; }
        public int OfficeID { get; set; }
        public ObservableCollection<Property> Properties { get; set; }
    }
}
