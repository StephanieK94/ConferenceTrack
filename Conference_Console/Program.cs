using System;
using System.Collections.Generic;
using System.IO;

namespace Conference_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            var pathInput = $"{path}\\Input.txt";

            FileStreamer fs = new FileStreamer();

            List<string> conferenceTalkList = fs.FileReader(pathInput);

            Converter separator = new Converter();

            var pathOutput = $"{path}\\Output.txt";
            List<Activity> conferenceActivityList = new List<Activity>();
            conferenceActivityList = separator.ConvertToActivityListFrom(conferenceTalkList);

            List<Activity> orderList = new List<Activity>();
            orderList = separator.GetOrderedActivityListFrom(conferenceActivityList);

            Track track1 = new Track();
            Scheduler scheduler = new Scheduler();

            track1.ActivityList = scheduler.CreateTrackScheduleFrom(orderList);

            fs.FileWriter(track1.ActivityList, pathOutput);
        }
    }
}
