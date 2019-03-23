using SWT25_Assignment2_AirTrafficMonitoring.TransponderReceiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace SWT25_Assignment2_AirTrafficMonitoring
{
    public struct AirSpace
    {
        public int Height_to { get; set; }
        public int Height_from{ get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class Airport
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
        public Airport(ITransponderReceiver receiver,DecodeFactory.DecodeFactory decode, AirSpace airspace)
        {
            Decode = decode;
            Receiver = receiver;
            Receiver.TransponderDataReady += AirportReceiverHandler;
            this.airspace = airspace;

        }
        #endregion
        #region Properties

        public DecodeFactory.DecodeFactory Decode { get; private set; }
        public ITransponderReceiver Receiver { get; private set; }
        public AirSpace airspace { get; private set; }
 

        #endregion

        public void AirportReceiverHandler(object sender, RawTransponderDataEventArgs e)
        {
            var list = Decode.CreateTracks(e.TransponderData);
            var filteredList = FilterTracks(list);
            
            TrackDataEvent?.Invoke(this, new TrackDataEventArgs(filteredList));
        }

       public event EventHandler<TrackDataEventArgs> TrackDataEvent;

       public List<Track> FilterTracks(List<Track> tracks)
       {
           List<Track> sendTracks=new List<Track>();
           foreach (var track in tracks)
           {
               if (airspace.Height_from <= track.CurrentAltitude && airspace.Height_to >= track.CurrentAltitude
                                                                 && airspace.X >= track.CurrentPositionX 
                                                                 && airspace.Y >= track.CurrentPositionY
                                                                 && track.CurrentPositionX>=0
                                                                 && track.CurrentPositionY>=0)
               {
                    sendTracks.Add(track);
               }
           }

           return sendTracks;
       }
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
