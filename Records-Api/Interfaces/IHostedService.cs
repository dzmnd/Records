using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Records_Api.Interfaces
{
    public interface IHostedService
    {
        void UpdateRecords(object o);
    }
}
