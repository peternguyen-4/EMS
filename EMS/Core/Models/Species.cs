using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class Species
    {
        [Key]
        public int speciesID {  get; set; }
        public string speciesName { get; set; } = string.Empty;

        public Species() { }

        public Species(int speciesID, string speciesName)
        {
            this.speciesID = speciesID;
            this.speciesName = speciesName;
        }
    }
}
