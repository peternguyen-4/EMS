using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Models
{
    public abstract class SampleData
    {
        [Key]
        public int sampleID { get; set; }
        public DateTime date { get; set; }

        protected SampleData() { }

        protected SampleData(int sampleID, DateTime date)
        {
            this.sampleID = sampleID;
            this.date = date;
        }
    }
}
