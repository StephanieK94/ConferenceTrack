using System;
using System.Collections.Generic;

public class Scheduler
{
    public List<Activity> CreateTrackScheduleFrom(List<Activity> orderedListByDuration)
    {
        List<Activity> scheduleOfTrack = new List<Activity>();

        DateTime currentTime = new DateTime(2019,6,6,9,0,0);
        DateTime Lunch = currentTime.AddHours(3);
        DateTime EndOfTrack = currentTime.AddHours(8);

        // add a whil(!currentTime == 5pm) ?
        foreach(var activity in orderedListByDuration)
        {
            if ((DateTime.Compare(currentTime, Lunch)) < 0)
            {
                // see if can schedule the activity in before 12
                if(IsAbleToBeScheduled(currentTime, activity.DurationInMin, Lunch))
                {
                    // add Time to the activity from currentTime,
                    // add the activity to the list
                    // add Time to currentTime
                    // break
                }
            }
            else if((DateTime.Compare(currentTime, Lunch)) == 0)
            {
                // add a new Activity of lunch? or add as an activity for track to begin with?
                currentTime.AddHours(1);
                continue;
            }
            else if(DateTime.Compare(currentTime, Lunch) > 0)
            {
                // see if can schedule the activity in after lunch
                if(IsAbleToBeScheduled(currentTime, activity.DurationInMin, EndOfTrack))
                {
                    // add Time to the activity from currentTime,
                    // add the activity to the list
                    // add Time to currentTime
                    // break
                }
                //else // add networking event for the currentTime
            }
            //else create a default or here is where will add to track2

        }

        

        return scheduleOfTrack;
    }

    public bool IsAbleToBeScheduled(DateTime currentTime, int durationOfActivity, DateTime timeLimit)
    {
        var value = DateTime.Compare(currentTime.AddMinutes(durationOfActivity), timeLimit);

        if(value <= 0)
        {
            return true;
        }
        return false;
    }
}