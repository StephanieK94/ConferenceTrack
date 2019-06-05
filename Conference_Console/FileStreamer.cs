using System;
using System.IO;
using System.Collections.Generic;

class FileStreamer 
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

    public void FileWriter(List<Activity> conferenceList, string pathOutput)
    {
        using(StreamWriter sw = new StreamWriter(pathOutput))
        {
            foreach(var activity in conferenceList)
            {
                sw.WriteLine($"{activity.Name}  {activity.DurationInMin}");
            }
        }
    }
}