/*
Write a C# program to print the number of years, days, hours, minutes, seconds - based on given number of seconds.
Eg:
seconds = 288970
The given number of seconds is equivalent to "3 days, 8 hours, 16 minutes, 10 seconds"
Output: 3 days, 8 hours, 16 minutes, 10 seconds
*/
class Program
{
    static void Main()
    {
        int seconds = 288970; //input

        int remaining_seconds = seconds; //copying the actual input; so that, the original input will be persisted
        int seconds_per_minute = 60; //1 minute = 60 seconds
        int seconds_per_hour = 60 * 60; //1 hour = 60 minute (each minute is 60 seconds)
        int seconds_per_day = 24 * 60 * 60; //1 day = 24 hours (each hour is 60 minutes; and each minute is 60 seconds)

        int days = remaining_seconds / seconds_per_day; //divide the "remaining_seconds" with "number of seconds per day"
        remaining_seconds -= days * seconds_per_day; //subtract the number of seconds which equivalent to the number of days calculated above

        int hours = remaining_seconds / seconds_per_hour; //divide the "remaining_seconds" with "number of seconds per hour"
        remaining_seconds -= hours * seconds_per_hour; //subtract the number of seconds which equivalent to the number of hours calculated above
        int minutes = remaining_seconds / seconds_per_minute; //divide the "remaining_seconds" with "number of seconds per minute"
        remaining_seconds -= minutes * seconds_per_minute; //subtract the number of seconds which equivalent to the minutes of days calculated above

        System.Console.WriteLine(days + " days, " + hours + " hours, " + minutes + " minutes, " + remaining_seconds + " seconds");
        System.Console.ReadKey();
    }
}

