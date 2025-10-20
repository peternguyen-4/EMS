using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Repositories.Interfaces
{
    public interface IExportable
    {
        string Name { get; }
        IEnumerable<object> GetAllForExport();
    }
}
