using System;

namespace CodexBit.Services;

public static class TimeHelperService
{
    public static string TimeAgo (DateTime CreatedTime)
    {
        var timeDiff = DateTime.UtcNow - CreatedTime;
        var hoursAgo = (int)timeDiff.TotalHours;
        var daysAgo = (int)timeDiff.TotalDays;
        var minutesAgo = (int)timeDiff.TotalMinutes;

        string timeString;
        if (daysAgo > 0)
        {
            timeString = $"Há {daysAgo} dia{(daysAgo > 1 ? "s" : "")}";
        }
        else if (hoursAgo > 0)
        {
            timeString = $"Há {hoursAgo} hora{(hoursAgo > 1 ? "s" : "")}";
        }
        else if (minutesAgo > 1)
        {
            timeString = $"Há {minutesAgo} minutos";
        }
        else
        {
            timeString = "Agora mesmo";
        }

        return timeString;
    }
}