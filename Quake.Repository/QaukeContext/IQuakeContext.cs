using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quake.Repository.QaukeContext
{
    public interface IQuakeContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
