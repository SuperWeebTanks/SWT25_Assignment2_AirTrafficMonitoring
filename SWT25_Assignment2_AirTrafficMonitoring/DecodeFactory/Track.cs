using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
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
                    throw new ArgumentException("Invalid Tag"); 
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
            CurrentHorizontalVelocity = 0;
            CurrentCompassCourse = 0; 
        }
        #endregion

        #region Overloads
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(CommercialTrack obj1, CommercialTrack obj2)
        {
            return (obj1.Tag == obj2.Tag && obj1.CurrentPositionX == obj2.CurrentPositionX
                                         && obj1.CurrentAltitude == obj2.CurrentAltitude &&
                                         obj1.CurrentPositionY == obj2.CurrentPositionY
                                         && obj1.TimeStamp == obj2.TimeStamp &&
                                         obj1.CurrentCompassCourse == obj2.CurrentCompassCourse &&
                                         obj1.CurrentHorizontalVelocity == obj2.CurrentHorizontalVelocity); 
        }

        public static bool operator !=(CommercialTrack obj1, CommercialTrack obj2)
        {
            return (obj1.Tag != obj2.Tag || obj1.CurrentPositionX != obj2.CurrentPositionX
                                         || obj1.CurrentAltitude != obj2.CurrentAltitude ||
                                         obj1.CurrentPositionY != obj2.CurrentPositionY
                                         || obj1.TimeStamp != obj2.TimeStamp ||
                                         obj1.CurrentCompassCourse != obj2.CurrentCompassCourse ||
                                         obj1.CurrentHorizontalVelocity != obj2.CurrentHorizontalVelocity);
        }

        #endregion

    }
}

