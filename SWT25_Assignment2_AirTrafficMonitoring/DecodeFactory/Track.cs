using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
namespace SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory
{
    public class Track
    {
        /// <summary>
        /// Properties for a track - based on the information 
        /// received by the transponder.
        /// </summary>

        #region Properties

        public virtual int CurrentPositionX { get; set; } = 0;
        public virtual int CurrentPositionY { get; set; } = 0;

        private string tag = "";

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

        public double CurrentHorizontalVelocity { get; set; } = 0;

        private int currentAltitude = 0;

        public int CurrentAltitude
        {
            get { return currentAltitude; }
            set
            {
                if (value >= 0)
                {
                    currentAltitude = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Altitude");
                }
            }
        }

        private int compassCourse = 0;

        public int CurrentCompassCourse
        {
            get { return compassCourse; }
            set
            {
                if (value >= 0 && value <= 360)
                {
                    compassCourse = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Compass Course");
                }
            }
        }

        public DateTime TimeStamp { get; set; }
        #endregion
        
        #region Overloads == & != 
        public static bool operator ==(Track obj1, Track obj2)
        {
            return (obj1.Tag == obj2.Tag && obj1.CurrentPositionX == obj2.CurrentPositionX
                                         && obj1.CurrentAltitude == obj2.CurrentAltitude &&
                                         obj1.CurrentPositionY == obj2.CurrentPositionY
                                         && obj1.TimeStamp == obj2.TimeStamp &&
                                         obj1.CurrentCompassCourse == obj2.CurrentCompassCourse &&
                                         obj1.CurrentHorizontalVelocity == obj2.CurrentHorizontalVelocity);
        }

        public static bool operator !=(Track obj1, Track obj2)
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

