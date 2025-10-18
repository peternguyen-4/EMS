using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    internal class SoilData : SampleData
    {
        public int firmness { get; set; }
        public float pH { get; set; }
        public float density {  get; set; }
        public float moisture { get; set; }
        public float nitrogen { get; set; }
        public float organicMatter { get; set; }
        public string microbiology { get; set; }
        public string contaminants { get; set; }

        public SoilData(int sampleID, int firmness, DateTime date, float pH, float density, float moisture, float nitrogen, float organicMatter, string microbiology, string contaminants) : base (sampleID, date)
        {
            this.firmness = firmness;
            this.pH = pH;
            this.density = density;
            this.moisture = moisture;
            this.nitrogen = nitrogen;
            this.organicMatter = organicMatter;
            this.microbiology = microbiology;
            this.contaminants = contaminants;
        }
    }
}
