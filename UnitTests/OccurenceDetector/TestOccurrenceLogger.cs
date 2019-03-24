using NUnit.Framework;
using SWT25_Assignment2_AirTrafficMonitoring.DecodeFactory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using SWT25_Assignment2_AirTrafficMonitoring.AirTrafficMonitor;

namespace UnitTests.OccurenceDetector
{
    [TestFixture]
    public class TestOccurrenceLogger
    {
        private Track TestTrack1 { get; set; }
        private Track TestTrack2 { get; set; }
        private Track OccurenceTrack { get; set; }
        private DateTime TimeNow { get; set; }

        private OccurrenceLogger Logger { get; set; }

        [SetUp]
        public void SetUp()
        {
            TestTrack1 = new Track();
            TestTrack2 = new Track();
            OccurenceTrack = new Track();
            TimeNow = DateTime.Now;
            Logger = new OccurrenceLogger();

            TestTrack1.Tag = "BTR312";
            TestTrack2.Tag = "QLM267";
            OccurenceTrack.Tag = "ATB927";

            Logger.ClearLog();
        }

        [Test]
        public void LogOccurences_LogOccurencesForTwoTracks_LogsOccurenceinTextFile()
        {
            //Arrange
            string LogText = "Log Entry:";
            bool result = false; 
            LogText += $"Potential Collision between aircrafts detected at {TimeNow}";
            LogText += $"Aircrafts involved: {TestTrack1.Tag} and {OccurenceTrack.Tag}";

            //Act
            Logger.LogOccurrences(TestTrack1, OccurenceTrack, TimeNow);

            using (StreamReader r = File.OpenText("log.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        line += r.ReadLine(); 
                       
                    }
                    if (line.Contains(LogText))
                        result = true; 
                }
            }

            //Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void LogOccurences_LogOccurencesForSeveralTrackOccurences_LogSeveralOccurencesinTextFile()
        {
            //Arrange
            bool result1 = false;
            bool result2 = false;
            bool TotalResult = false;
            string LogText1 = "Log Entry:";
            LogText1 += $"Potential Collision between aircrafts detected at {TimeNow}";
            LogText1 += $"Aircrafts involved: {TestTrack1.Tag} and {OccurenceTrack.Tag}";

            string LogText2 = "Log Entry:";
            LogText2 += $"Potential Collision between aircrafts detected at {TimeNow}";
            LogText2 += $"Aircrafts involved: {TestTrack2.Tag} and {OccurenceTrack.Tag}";

            //Act
            Logger.LogOccurrences(TestTrack1, OccurenceTrack, TimeNow);
            Logger.LogOccurrences(TestTrack2, OccurenceTrack, TimeNow);

            using (StreamReader r = File.OpenText("log.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        line += r.ReadLine();

                    }

                    if (line.Contains(LogText1))
                        result1 = true;
                    else if (line.Contains(LogText2))
                        result2 = true;
                    line = ""; 
                }
            }

            if (result1 && result2)
                TotalResult = true; 

            //Assert
            Assert.That(TotalResult, Is.EqualTo(true));
        }

        [Test]
        public void LogOccurences_LogOccurenceThatAlreadyExists_ReturnsWithoutLoggingOccurence()
        {
            //Arrange
            string LogText = "Log Entry:";
            bool result = false;
            bool totalResult = false;
            int LogOccurences = 0; 
            LogText += $"Potential Collision between aircrafts detected at {TimeNow}";
            LogText += $"Aircrafts involved: {TestTrack1.Tag} and {OccurenceTrack.Tag}";

            //Act
            //First time, it logs
            Logger.LogOccurrences(TestTrack1, OccurenceTrack, TimeNow);
            //Second time, it does not log 
            Logger.LogOccurrences(TestTrack1, OccurenceTrack, TimeNow);

            using (StreamReader r = File.OpenText("log.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        line += r.ReadLine();

                    }

                    if (line.Contains(LogText))
                    {
                        result = true;
                        LogOccurences++; 
                    }
                }
            }

            if (result && LogOccurences == 1)
                totalResult = true;
            
            //Assert
            Assert.That(totalResult, Is.EqualTo(true));
        }

        [Test]
        public void ClearsLog_ClearsLogWithTrackLogs_LogIsEmpty()
        {
            //Arrange 
            //Log Fake Entry
            string line = ""; 
            Logger.LogOccurrences(TestTrack1, OccurenceTrack, TimeNow);

            //Act
            Logger.ClearLog();
            using (StreamReader r = File.OpenText("log.txt"))
            {
                line = r.ReadLine();
            }
            Assert.That(line, Is.EqualTo(null));
        }

        

    }
}
