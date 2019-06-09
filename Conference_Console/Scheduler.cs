using System;
using System.Collections.Generic;

public class Scheduler
{
    public List<Activity> CreateTrackScheduleFrom(List<Activity> activityList)
    {
        var scheduleOfTrack = new List<Activity>();

        var StartTime = new DateTime(2019,7,6,9,0,0);    
        var Lunch = StartTime.AddHours(3);               
        var EndOfTrack = StartTime.AddHours(8);   
        
        var sumOfTalksThusFar =0;       

        for(var index = 0; index < activityList.Count; index++)
        {
            var currentTime = StartTime.AddMinutes(sumOfTalksThusFar);

            var result = DateTime.Compare(currentTime, Lunch);

            if (result < 0)
            {                
                if(IsAbleToBeScheduled(currentTime, activityList[index].DurationInMin, Lunch))
                {
                    activityList[index].TimeStart = currentTime;
                    scheduleOfTrack.Add(activityList[index]);
                    sumOfTalksThusFar += activityList[index].DurationInMin;
                }
            }
            else if(result == 0)
            {
                var lunch = new Activity(){
                    Name = "Lunch",
                    DurationInMin = 60,
                    TimeStart = currentTime
                };

                scheduleOfTrack.Add(lunch);
                sumOfTalksThusFar += activityList[index].DurationInMin;
            }
            else if(result > 0)
            {
                if(IsAbleToBeScheduled(currentTime, activityList[index].DurationInMin, EndOfTrack))
                {
                    activityList[index].TimeStart = currentTime;
                    scheduleOfTrack.Add(activityList[index]);
                    sumOfTalksThusFar += activityList[index].DurationInMin;
                }
                else
                {
                    var networkingEvent = new Activity();

                    networkingEvent.Name = "Networking Event";
                    networkingEvent.TimeStart = currentTime;

                    sumOfTalksThusFar += activityList[index].DurationInMin;
                }
            }
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
        else return false;
    }

    // public bool IsBeforeLunch(DateTime currentTime, DateTime lunchTimeLimit)
    // {
    //     var result = DateTime.Compare(currentTime, Lunch);

    //     if (result < 0)
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // public DateTime ChangeCurrentTime(this DateTime current, int minutes)
    // {
    //     var hours = minutes/60;
    //     minutes = minutes%60;

    //     return new DateTime(
    //         current.Year,
    //         current.Month,
    //         current.Day,
    //         hours,
    //         minutes,
    //         current.Second,
    //         current.Millisecond,
    //         current.Kind
    //     );
    // }
}