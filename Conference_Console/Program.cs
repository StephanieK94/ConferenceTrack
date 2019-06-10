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

            var fs = new FileStreamer();
            var conferenceTalkList = fs.FileReader(pathInput);

            var converter = new Converter();
            var conferenceActivityList = new List<Activity>();
            conferenceActivityList = converter.ConvertToActivityListFrom(conferenceTalkList);

            var orderList = new List<Activity>();
            orderList = converter.GetOrderedActivityListFrom(conferenceActivityList);

            var scheduler = new Scheduler();
            var tracksList = new List<Track>();
            tracksList = scheduler.CreateTracksFrom(orderList);

            foreach(var track in tracksList)
            {
                track.ActivityList = converter.GetOrderedDateTimeListFrom(track.ActivityList);
            }

            var pathOutput = $"{path}\\Output.txt";
            fs.FileWriter(tracksList, pathOutput);
        }
    }
}
