using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryData.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsSchengen { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public virtual List<Region> Region { get; set; }
    }
}
