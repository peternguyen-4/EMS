using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class WaterData : SampleData
    {
        public float pH {  get; set; }
        public float dissolvedOxygen { get; set; }
        public float salinity { get; set; }
        public float turbidity { get; set; }
        public float hardness { get; set; }
        public float eutrophicPotential { get; set; }
        public string microbiology { get; set; }
        public string contaminants { get; set; }

        public WaterData(int sampleID, DateTime date, float pH, float dissolvedOxygen, float salinity, float turbidity, float hardness, float eutrophicPotential, string microbiology, string contaminants) : base (sampleID, date)
        {
            this.pH = pH;
            this.dissolvedOxygen = dissolvedOxygen;
            this.salinity = salinity;
            this.turbidity = turbidity;
            this.hardness = hardness;
            this.eutrophicPotential = eutrophicPotential;
            this.microbiology = microbiology;
            this.contaminants = contaminants;
        }
    }
}
