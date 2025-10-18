using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public class SpeciesData : SampleData
    {
        public int speciesID { get; set; }
        public int populationCount { get; set; }
        public int scatCount { get; set; }
        public float reproductiveFactor { get; set; }
        public string knownHabitats { get; set; } = string.Empty;
        public string healthConcerns { get; set; } = string.Empty;
        public string additionalNotes { get; set; } = string.Empty;

        public SpeciesData() { }

        public SpeciesData(int sampleID, DateTime date, int speciesID, int populationCount, int scatCount, float reproductiveFactor, string knownHabitats, string healthConcerns, string additionalNotes) : base (sampleID, date)
        {
            this.speciesID = speciesID;
            this.populationCount = populationCount;
            this.scatCount = scatCount;
            this.reproductiveFactor = reproductiveFactor;
            this.knownHabitats = knownHabitats;
            this.healthConcerns = healthConcerns;
            this.additionalNotes = additionalNotes;
        }
    }
}
