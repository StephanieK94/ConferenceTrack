using System;
using System.IO;
using System.Collections.Generic;

public class FileStreamer 
{
    public List<string> FileReader(string pathInput)
    {
        List<string> input = new List<string>();

        using(StreamReader sr = new StreamReader(pathInput))
        {
            while(!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                input.Add(line);
            }
        }

        return input;
    }

    public void FileWriter(List<Track> conferenceList, string pathOutput)
    {
        using(StreamWriter sw = new StreamWriter(pathOutput))
        {
            var x =1;
            foreach(var track in conferenceList)
            {
                sw.WriteLine($"Track {x}\n");

                foreach(var activity in track.ActivityList)
                {
                    sw.WriteLine($"{activity.Time.ToString("hh:mmtt")}  {activity.Name}");
                }
                
                sw.WriteLine("\n");
                x++;
            }
        }
    }
}