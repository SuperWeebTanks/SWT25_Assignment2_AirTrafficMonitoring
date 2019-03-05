using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring
{
    public class RawTransponderDataEventArgs : EventArgs
    {
        public RawTransponderDataEventArgs(List<string> transponderData)
        {
            TransponderData = transponderData;
        }
        public List<string> TransponderData { get; }
    }
    public interface ITransponderReceiver
    {
        event EventHandler<RawTransponderDataEventArgs> TransponderDataReady;
    }

    /*
    public class TransponderReceiverFactory
    {
        public static ITransponderReceiver CreateTransponderDataReceiver()
    }
    */

}
