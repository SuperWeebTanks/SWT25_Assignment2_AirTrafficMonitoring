using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory
{
    public abstract class Track
    {
        /// <summary>
        /// Properties for a track - based on the information 
        /// received by the transponder.
        /// </summary>
        #region Properties
        protected string AircraftType { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }
        private string tag;
        public string Tag
        {
            get { return tag; }
            set
            {
                if (value.Length == 6)
                    tag = value;
                else
                {
                    Console.WriteLine("Invalid Tag");
                    return;
                }
            }
        }
        public int CurrentHorizontalVelocity { get; set; }        
        public int CurrentAltitude { get; set; }
        //Not sure if this needs restrictions. 
        public int CurrentCompassCourse { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion
    }

    public class CommercialTrack : Track
    {
        /// <summary>
        /// Adds nothing new to track, simple states the AircraftType
        /// and sets the properties in the constructor. 
        /// </summary>
        #region Constructors
        public CommercialTrack()
        {
            AircraftType = "Commercial";
        }

        #endregion
    }
}
