using System;
using System.Collections.Generic;
using System.Linq;

public class Converter
{
    public List<Activity> GetOrderedActivityListFrom(List<Activity> unorderedList)
    {
        List<Activity> orderedList = new List<Activity>();

        orderedList = unorderedList.OrderByDescending(activity => activity.DurationInMin).ToList();

        return orderedList;
    }

    public List<Activity> ConvertToActivityListFrom(List<string> conferenceTalkList)
    {
        List<Activity> conferenceActivitiesList = new List<Activity>();

        foreach(var talk in conferenceTalkList)
        {
            Activity newActivity = new Activity();

            newActivity.Name = talk;
            newActivity.StartTime = new DateTime(2019,6,6,9,0,0);   // all start at 9am

            string[] talkName = talk.Split(' ');
            
            newActivity.DurationInMin = ConvertToIntFrom(talkName[talkName.Length-1]);

            conferenceActivitiesList.Add(newActivity);
        }

        return conferenceActivitiesList;
    }

    public int ConvertToIntFrom(string lastString)
    {
        char[] minRemove = {'m','i','n'};
        int duration=0;

        if(lastString == "lightning".ToLower())
        {
            duration = 5;
        }
        else
        {
            duration = Convert.ToInt32(lastString.Trim(minRemove));
        }

        return duration;
    }
}