﻿using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public class Calculator
    {
        /// <summary>
        ///  Calculates Distances between two track positions
        /// </summary>
        /// <param name="track1"></param>
        /// <param name="track2"></param>
        /// <returns>Distance in meters </returns>
        public static double CalculateHorizontalDistance(Track track1, Track track2)
        {
            return Math.Sqrt(Math.Pow((track2.CurrentPositionX - track1.CurrentPositionX), 2) +
                             Math.Pow((track2.CurrentPositionY - track1.CurrentPositionY), 2));
        }

        /// <summary>
        /// Calculates Horizontal Velocity based on two track positions
        /// </summary>
        /// <param name="updatedTrack"></param>
        /// <param name="oldTrack"></param>
        /// <returns>Horizontal Velocity in meters per second </returns>
        public static double CalculateHorizontalVelocity(Track updatedTrack, Track oldTrack)
        {
            return (CalculateHorizontalDistance(oldTrack, updatedTrack) /
                  (updatedTrack.TimeStamp - oldTrack.TimeStamp).TotalSeconds);
        }
        
        /// <summary>
        /// Calculates Compass Course based on two track positions
        /// </summary>
        /// <param name="updatedTrack"></param>
        /// <param name="oldTrack"></param>
        /// <returns></returns>
        public static double CalculateCompassCourse(Track updatedTrack, Track oldTrack)
        {
            double x = updatedTrack.CurrentPositionX - oldTrack.CurrentPositionX;
            double y = updatedTrack.CurrentPositionY - oldTrack.CurrentPositionY;

            double angle = Math.Tanh(y / x);

            if (y / x > 0)
            {
                if (x > 0)
                {

                }
                else
                {
                    angle += 180;
                }

            }
            else if (y/x<0)
            {
                if (x > 0)
                {
                    angle += 360;
                }
                else
                {
                    angle -=180;
                }
                angle += 180; 
            }

            return 0; 
        }
    }
}