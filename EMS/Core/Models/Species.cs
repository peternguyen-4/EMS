using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class Species
    {
        public int speciesID {  get; set; }
        public string speciesName { get; set; }

        public Species(int speciesID, string speciesName)
        {
            this.speciesID = speciesID;
            this.speciesName = speciesName;
        }
    }
}
