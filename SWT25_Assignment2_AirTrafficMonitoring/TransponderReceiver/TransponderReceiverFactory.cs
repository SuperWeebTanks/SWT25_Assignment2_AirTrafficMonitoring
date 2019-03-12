using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.TransponderReceiver
{
    public abstract class TransponderReceiverFactory
    {
        public abstract ITransponderReceiver createReceiver(string type); 
    }

    public class AirportTransponderFactory : TransponderReceiverFactory
    {
        public override ITransponderReceiver createReceiver(string type)
        {
            throw new NotImplementedException();
        }
    }
}
