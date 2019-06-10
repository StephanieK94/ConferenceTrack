using System;
using System.Collections.Generic;

public class Scheduler
{
    public List<Track> CreateTracksFrom(List<Activity> activityList)
    {
        var dayListOfTracks = new List<Track>();
        var converter = new Converter();

        var timeRemainging = converter.GetDurationOfTalksFrom(activityList);

        while(timeRemainging > 0)
        {
            var newTrack = new Track();

            newTrack.ActivityList = FillTrackScheduleFrom(activityList);

            activityList.RemoveAll(activity => newTrack.ActivityList.Contains(activity));

            var timeToRemove = converter.GetDurationOfTalksFrom(newTrack.ActivityList);
            timeRemainging -= (timeToRemove - 60);      // minus 60 for lunch, refactor to change so each track has a lunch and a networking?

            dayListOfTracks.Add(newTrack);
        }

        return dayListOfTracks;
    }

    public List<Activity> FillTrackScheduleFrom(List<Activity> activityList)
    {
        var scheduleOfTrack = new List<Activity>();

        var StartTime = new DateTime(2019,7,6,9,0,0);    
        var EndOfTrack = StartTime.AddHours(8);   
        
        var lunch = new Activity()
        {
            Name = "Lunch",
            Time = StartTime.AddHours(3),
            DurationInMin = 60,
        };
        
        var sumOfTalksThusFar =0;       

        foreach(var activity in activityList)
        {
            if(IsLunchTime(StartTime, lunch.Time, sumOfTalksThusFar) ==true) sumOfTalksThusFar += 60;
            
            var currentTime = StartTime.AddMinutes(sumOfTalksThusFar);
                          
            if(IsAbleToBeScheduledBefore(lunch.Time, currentTime, activity))
            {
                activity.Time = currentTime;
                scheduleOfTrack.Add(activity);
                sumOfTalksThusFar += activity.DurationInMin;
            }
            else if(IsAbleToBeScheduledBefore(EndOfTrack, currentTime, activity))
            {
                activity.Time = currentTime;
                scheduleOfTrack.Add(activity);
                sumOfTalksThusFar += activity.DurationInMin;
            }
        }

        scheduleOfTrack.Add(lunch);

        var networking = new Activity()
        {
            Name = "Networking Event",
            Time = StartTime.AddMinutes(sumOfTalksThusFar),
        };
        scheduleOfTrack.Add(networking);

        return scheduleOfTrack;
    }

    public bool IsAbleToBeScheduledBefore(DateTime timeLimit, DateTime currentTime, Activity activity)
    {
        var value = DateTime.Compare(currentTime.AddMinutes(activity.DurationInMin), timeLimit);

        if(value <= 0)
        {
            return true;
        }
        return false;
    }

    public bool IsLunchTime(DateTime startTime, DateTime lunchTime, int talksThusFar)
    {
        if(DateTime.Compare(startTime.AddMinutes(talksThusFar),lunchTime) == 0) return true;
        return false;
    }
}