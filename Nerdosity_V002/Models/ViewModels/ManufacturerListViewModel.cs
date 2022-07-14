using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nerdosity_V002.Models
{
    public class ManufacturerListViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
