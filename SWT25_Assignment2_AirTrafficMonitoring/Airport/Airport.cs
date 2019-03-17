using SWT25_Assignment2_AirTrafficMonitoring.TransponderReceiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace SWT25_Assignment2_AirTrafficMonitoring.Airport
{
    public class Airport : ISignalForwarder
    {
        /// <summary>
        /// Airport Constructor
        /// Dependency injection for 
        /// <see cref="ISignalForwarder"/>
        /// <see cref="ITransponderReceiver"/>
        /// <see cref="TransponderReceiverFactory"/>
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="factory"></param>
        /// <param name="forwarder"></param>
        #region Constructors 
        public Airport(ITransponderReceiver receiver, 
            ISignalForwarder forwarder, CommercialTrackFactory decode)
        {
            Decode = decode;
            Receiver = receiver;
            Receiver.TransponderDataReady += AirportReceiverHandler;
            Forwarder = forwarder; 
        }
        #endregion
        #region Properties

        public CommercialTrackFactory Decode { get; private set; };
        public ITransponderReceiver Receiver { get; private set; }
        public ISignalForwarder Forwarder { get; private set; }
 

        #endregion

        public void AirportReceiverHandler(object sender, RawTransponderDataEventArgs e)
        {
            var list = Decode.CreateTracks(e.TransponderData);
           
            TrackDataEvent?.Invoke(this, new TrackDataEventArgs(list));
        }

       public event EventHandler<TrackDataEventArgs> TrackDataEvent;

        }
    public class TrackDataEventArgs : EventArgs
    {
        public TrackDataEventArgs(List<Track> trackData)
        {
            TrackData = trackData;
        }
        public List<Track> TrackData { get; set; }
    }
}
