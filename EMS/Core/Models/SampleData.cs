using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public abstract class SampleData
    {
        int sampleID { get; set; }
        DateTime date { get; set; }

        protected SampleData(int sampleID, DateTime date)
        {
            this.sampleID = sampleID;
            this.date = date;
        }
    }
}
