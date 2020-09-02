using libStreamSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector
{
    public class ThinkgearConnector : IConnector
    {
        private int connectionID = -1;
        private string COM_port = null;

        public ThinkgearConnector(string[] args)
        {
            NativeThinkgear thinkgear = new NativeThinkgear();
            connectionID = NativeThinkgear.TG_GetNewConnectionId();
            if (connectionID < 0)
                throw new Exception("ERROR: TG_GetNewConnectionId() returned: " + connectionID);
            COM_port = "COM5";
            if (args != null)
                COM_port = args[0];
        }

        public object Connect(object parameters=null)
        {
            try
            {
                string comPortName = "\\\\.\\" + COM_port;
                int errCode = NativeThinkgear.TG_Connect(connectionID,
                              comPortName,
                              NativeThinkgear.Baudrate.TG_BAUD_115200,
                              NativeThinkgear.SerialDataFormat.TG_STREAM_PACKETS);
                if (errCode < 0)
                    throw new Exception("ERROR: TG_Connect() returned: " + errCode);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                return false;
            }
        }

        public object Disconnect(object parameters=null)
        {
            try
            {
                NativeThinkgear.TG_Disconnect(connectionID);
                NativeThinkgear.TG_FreeConnection(connectionID);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                return false;
            }
        }

        public object GetData(object parameters)
        {
            try
            {
                int numPackets = 1;
                if (parameters != null)
                    numPackets = (int)parameters;

                int errCode = NativeThinkgear.TG_ReadPackets(connectionID, numPackets);
                if (errCode < 0)
                    throw new Exception("Error in reading packets: " + errCode);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                return false;
            }
        }

        public object GetValue(object parameters)
        {
            if (parameters == null)
                return null;

            try
            { 
                NativeThinkgear.DataType dataType = (NativeThinkgear.DataType)parameters;
                return NativeThinkgear.TG_GetValue(connectionID, dataType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                return null;
            }
        }
    }
}
