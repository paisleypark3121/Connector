using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public interface IConnector
    {
        object Connect(object parameters=null);
        object Disconnect(object parameters=null);
        object GetData(object parameters);
        object GetValue(object parameters);
    }
}
