using SWT25_Assignment2_AirTrafficMonitoring.TransponderReceiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.Airport
{
    public class Airport : ISignalForwarder
    {
        /// <summary>
        /// Airport Constructor
        /// Dependency injection
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="factory"></param>
        /// <param name="forwarder"></param>
        #region Constructors 
        public Airport(ITransponderReceiver receiver, TransponderReceiverFactory factory,
            ISignalForwarder forwarder)
        {
            Receiver = receiver;
            Factory = factory;
            Forwarder = forwarder; 
        }
        #endregion

        #region Properties
        public ITransponderReceiver Receiver { get; private set; }
        public TransponderReceiverFactory Factory { get; private set; }
        public ISignalForwarder Forwarder { get; private set; }
        #endregion
    }
}
