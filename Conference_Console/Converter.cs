using System;
using System.Collections.Generic;

class Separator
{
    public List<Activity> ConvertToActivityListFrom(List<string> conferenceTalkList)
    {
        List<Activity> conferenceActivitiesList = new List<Activity>();

        foreach(var talk in conferenceTalkList)
        {
            Activity newActivity = new Activity();

            newActivity.Name = talk;
            newActivity.StartTime = new DateTime(2019,6,6,9,0,0);   // all start at 9am

            string[] talkName = talk.Split(' ');
            char[] minRemove = {'m','i','n'};

            if(talkName[talkName.Length-1] == "lightning".ToLower())
            {
                newActivity.DurationInMin = 5;
            }
            else
            {
                newActivity.DurationInMin = Convert.ToInt32(talkName[talkName.Length-1].Trim(minRemove));
            }

            conferenceActivitiesList.Add(newActivity);
        }

        return conferenceActivitiesList;
    }
}