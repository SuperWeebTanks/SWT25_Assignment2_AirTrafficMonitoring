using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;

namespace SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor
{
    public class OccurrenceLogger
    {
        public OccurrenceLogger()
        {
            ClearLog();
        }

        public void LogOccurrences(Track observedtrack, Track occurenceTrack, DateTime time)
        {
            // Check if entry already exists for specified aircrafts
            using (StreamReader r = File.OpenText("log.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    if (line.Contains($"Aircrafts involved: {observedtrack.Tag} and {occurenceTrack.Tag}"))
                        return;
                }
            }

            //Add new log entry
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine("Log Entry:");
                w.WriteLine($"Potential Collision between aircrafts detected at {time}");
                w.WriteLine($"Aircrafts involved: {observedtrack.Tag} and {occurenceTrack.Tag}");
                w.WriteLine();
            }
        }

        public void ClearLog()
        {
            using (StreamWriter w = File.CreateText("log.txt"))
            {
                w.Flush();
            }
        }
    }
}
