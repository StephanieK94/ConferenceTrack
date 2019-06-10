using System;
using System.Collections.Generic;

public class Scheduler
{
    public List<Track> CreateTracksListFrom(List<Activity> conferenceTalkList)
    {
        var dayListOfTracks = new List<Track>();
        var converter = new Converter();

        var timeRemaining = converter.GetDurationOfTalksFrom(conferenceTalkList);

        while(timeRemaining > 0)
        {
            var newTrack = new Track();

            newTrack = FillTrackScheduleFrom(conferenceTalkList);

            conferenceTalkList.RemoveAll(activity => newTrack.ActivityList.Contains(activity));

            var timeOfTalksToRemove = converter.GetDurationOfTalksFrom(newTrack.ActivityList);
            timeRemaining -= (timeOfTalksToRemove - 60);

            dayListOfTracks.Add(newTrack);
        }

        return dayListOfTracks;
    }

    public Track FillTrackScheduleFrom(List<Activity> activityList)
    {
        var scheduleOfTrack = new Track();

        var StartTime = new DateTime(2019,7,6,9,0,0);    
        var EndOfTrack = StartTime.AddHours(8);   
        
        scheduleOfTrack.Lunch = new Activity()
        {
            Name = "Lunch",
            Time = StartTime.AddHours(3),
            DurationInMin = 60,
        };
        
        var sumOfTalksThusFar =0;       

        foreach(var activity in activityList)
        {
            if(IsLunchTime(StartTime, scheduleOfTrack.Lunch.Time, sumOfTalksThusFar) ==true) sumOfTalksThusFar += 60;
            
            var currentTime = StartTime.AddMinutes(sumOfTalksThusFar);
                          
            if(IsAbleToBeScheduledBefore(scheduleOfTrack.Lunch.Time, currentTime, activity))
            {
                activity.Time = currentTime;
                scheduleOfTrack.ActivityList.Add(activity);
                sumOfTalksThusFar += activity.DurationInMin;
            }
            else if(IsAbleToBeScheduledBefore(EndOfTrack, currentTime, activity))
            {
                activity.Time = currentTime;
                scheduleOfTrack.ActivityList.Add(activity);
                sumOfTalksThusFar += activity.DurationInMin;
            }
        }

        scheduleOfTrack.Networking = new Activity()
        {
            Name = "Networking Event",
            Time = StartTime.AddMinutes(sumOfTalksThusFar),
        };

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