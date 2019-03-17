using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.Airport;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;


namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public abstract class Air_Traffic_Monitor
    {
        private List<Track> tracks;
        private IOccurenceDetector occurenceDetecter;
        private IDisplay display;
        protected Air_Traffic_Monitor(Airport.Airport airport, IOccurenceDetector detector,IDisplay display)
        {
            tracks=new List<Track>();
            this.display = display;
            occurenceDetecter = detector;
            airport.TrackDataEvent += Update;
        }


        public void Update(Object sender, TrackDataEventArgs e)
        {
            var list = e.TrackData;
            foreach (var track in list)
            {
                var listOfTracksInOccurence=//kald til IOccurence funktion 


            }
        }

        
        
    }

    public class Commercial_ATM : Air_Traffic_Monitor
    {

    }
}
