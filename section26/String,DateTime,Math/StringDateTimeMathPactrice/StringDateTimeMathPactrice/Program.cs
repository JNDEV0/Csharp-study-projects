using System.Text;
using System.Text.RegularExpressions;

namespace StringDateTimeMathPractice
{
    class Program
    {
        static void Main()
        {
            StringAndStringBuilder();
            DateTimePractice();
            MathPractice();
        }

        private static void MathPractice()
        {
            double toThePowerOf = Math.Pow(10, 4); //1st param to power of 2nd param, ie. 10 * 10 * 10 * 10 = 10 to the power of 4
            double minOf = Math.Min(5.22, 4.29); //returns smallest of params ie. 4.29
            double maxOf = Math.Max(5.22, 4.29); //returns largest of params ie. 5.22
            double floorOf = Math.Floor(minOf); //rounds to low end whole integer ie. 4.29 => 4
            double ceilingOf = Math.Ceiling(maxOf); //rounds to high end whole integer ie 5.22 => 6
            double roundFraction = Math.Round(maxOf, 0); //rounds 1st param to 2nd param number of fraction places, rounds down or up based on closest whole int
            int positiveOrNegative = Math.Sign(ceilingOf); //returns 1, 0 or -1 depending on wether param is positive, zero, or negative.
            double absoluteOf = Math.Abs(maxOf - maxOf - maxOf); //the "absolute" of a number is its positive version ie. Abs of -5.22 == 5.22
            int remainderOf = 10 % 3; //split way of acquiring only the remainder
            double quotientOf2 = Math.Floor(Convert.ToDouble(10) / 3); //split way of returning only the quotient (number of times param 2 fits into param 1)
            int quotientOf = Math.DivRem(10, 4, out int rem); //returns the number of times param 2 fits into param 1, and sends out third param with remainder value
            (int quotient, int remainder) = Math.DivRem(10, 4); //another way of acquiring the quotient and the remainder into a value tuple
            double squareRootOf = Math.Sqrt(25); //returns square root of given number

            Console.WriteLine(toThePowerOf);
            Console.WriteLine(minOf);
            Console.WriteLine(maxOf);
            Console.WriteLine(floorOf);
            Console.WriteLine(ceilingOf);
            Console.WriteLine(roundFraction);
        }

        private static void DateTimePractice()
        {
            DateTime dt = DateTime.Parse("2021-12-31 11:59:00.999 PM"); //this is standard format to parse datetime object instance from string
            dt = DateTime.Parse("12-31-2021 11:59:00.999 PM"); //this is a 2nd standard format to parse datetime object instance from string
            dt = new DateTime(2021, 12, 31, 23, 59, 00, 999); //DateTime also has a constructor, that takes 24h format

            //ParseExact() allows parsing a DateTime object from a custom format.
            //receives string of the custom DateTime, format to read the DateTime string, IFormatProvider CultureInfo for a region/country format, and format style
            dt = DateTime.ParseExact("31-12-2020 12:22:50", "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            
            dt = Convert.ToDateTime("2021-12-31 11:59:00.999 PM"); //internally calls DateTime.Parse, more readable but its an extra function call
            
            int year = dt.Year; //returns the years of the DateTime object in int format
            int month = dt.Month; //returns the months of the DateTime object in int format
            int day = dt.Day; //returns the days of the DateTime object in int format
            int hour = dt.Hour; //returns the hours of the DateTime object in int format
            int minute = dt.Minute; //returns the minutes of the DateTime object in int format
            int second = dt.Second; //returns the seconds of the DateTime object in int format
            int millisecond = dt.Millisecond; //returns the milliseconds of the DateTime object in int format 0-999 milliseconds min-max values, 1000ms == 1sec
            DayOfWeek dayOfWeek = dt.DayOfWeek; //enum will return string name of weekday
            int dayOfWeekInt = (int)dayOfWeek; //dt.DayOfWeek can be typecast into int to return number instead of string 
            int dayOfYear = dt.DayOfYear; //this can also be 366 due to leap years
            int daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month); //returns number of days in param month of param year
            DateTime dtNow = DateTime.Now; //readonly property that returns DateTime when the property is accessed
            int compareTwoDates = dt.CompareTo(DateTime.Now); //return 1 if param DateTime is greater than, 0 if same, -1 if less than instance calling method 
            TimeSpan subtractTwoDates = DateTime.Now.Subtract(dt); //subtract param DateTime from calling DateTime, returns a TimeSpan object
            TimeSpan subtractTwoDates2 = DateTime.Now - dt; //another way of subtracting a DateTime from another
            double yearsOfExperience = Math.Floor(subtractTwoDates.TotalDays / 365); //TimeSpace object has TotalDays property, use Math to calculate years, hours etc 
            double monthsOfExperience = Math.Round((subtractTwoDates.TotalDays - (Convert.ToInt32(yearsOfExperience) * 365)) / 30, 2); //calculating months from TotalDays

            //Add methods for Datetime can "add" negative values, essentially subtracting the given value
            //important to note they return a DateTime object, so it does not modify the original DateTime object
            //can be chained like promises to effectuate a single returned object
            DateTime dt2 = dt.AddYears(1).AddMonths(-2).AddDays(-5).AddHours(-10).AddMinutes(-9).AddSeconds(59).AddMilliseconds(1);

            //by default DateTime will print the date (ie. mm-dd-yyyy or dd-mm-yyyy) format as set on date format settings in the computer running the code
            Console.WriteLine(dt); //mm-dd-yyyy hh:mm:ss tt internally calls obj.ToString()
            Console.WriteLine(dt.ToString()); //mm-dd-yyyy hh:mm:ss tt explictly calling ToString() is better, one less function call
            Console.WriteLine(dt.ToShortDateString()); //mm-dd-yyy
            Console.WriteLine(dt.ToLongDateString()); //mm-MMMM-yyy (prints name of month instead of number)
            Console.WriteLine(dt.ToShortTimeString()); //hh:mm tt
            Console.WriteLine(dt.ToLongTimeString()); //hh:mm:ss tt
            Console.WriteLine(dt.ToString("dddd, MMMM dd yyyy")); //various formats using dd, dddd, mm, etc. custom date format, intellisense provides hints.
            Console.WriteLine($"year: {year}, month: {month}, day: {day}, hour: {hour}, minute: {minute}, second: {second}, millisecond: {millisecond}");
            Console.WriteLine($"{dayOfWeek} is day number {dayOfWeekInt} of the week");
            Console.WriteLine($"{dt} is day number {dayOfYear} of the year");
            Console.WriteLine($"there are {daysInMonth} in month {dt.Month}");
            Console.WriteLine($"the DateTime today is {dtNow}");
            Console.WriteLine($"comparing these two dates: {dt} to {DateTime.Now} returns {compareTwoDates}");
            Console.WriteLine($"two ways of subtracitng past date from DateTime.Now into TimeSpan: {subtractTwoDates}, {subtractTwoDates2}");
            Console.WriteLine($"Total Experience: {yearsOfExperience} years {monthsOfExperience} months"); 
            Console.WriteLine(dt2); //adjusted date via Add methods

        }

        private static void StringAndStringBuilder()
        {
            string hello = "Hello World";
            int length = hello.Length; //readonly Length property
            char h = hello[0]; //array access to char at position 0 of hello string
            string str = "Developer"; //strings are immutable reference objects in the heap, they can be converted into a hash code, to serve as key in collections
            string str2 = "Universe"; //if strings have the exact same content, the reference variables in the stack point to the same object
            string upperStr = str.ToUpper(); //new string with all characters to uppercase
            string lowerStr = str.ToLower(); //new string with all characters to lowercase
            string subStr = str.Substring(0, 3); //new string starting at 1st parameter index, for length of 2nd parameter
            string subStr2 = str.Substring(6); //new string from characters starting at index parameter
            string replaceStr = str.Replace("veloper", "senvolvedor"); //replace 1st param subString with 2nd param string
            string replaceStr2 = str.Replace('e', 'o'); //replace all 1st param char occurences with 2nd param char
            string[] splitStrings = str.Split("v"); //splits the string into two strings, removing the character passed as splitter
            string joinStr = string.Join(" ", splitStrings); //takes a character or string to separate each part and an IEnumerable collection of strings, merges into one.
            string trimString = str.Trim(); //new string with empty spaces in start and end of original string removed
            char[] charArrayFromStr = str.ToCharArray(); //retuns an array of characters by splitting the string, can be chained with .ToList() etc
            string newStrFromCharArray = new string(charArrayFromStr); //compile a character array into a string
            bool equalsStr = str.Equals(str2); //return true or false based on wether the same string is referenced, or same string content
            bool equalsStr2 = str2 == str; //2nd way of returning true or false based on wether the same string is referenced, or same string content
            bool startsWithStr = str2.StartsWith("Uni"); //return true or false based on wether string BEGINS with param sequence or character
            bool EndsWithStr = str.EndsWith("p"); //return true or false based on wether string ends with param sequence or character
            bool ContainsStr = str.Contains("Dev"); //return true or false based on wether the string contains passed param sequence or character
            int indexOfSubStr = str.IndexOf("loping"); //returns -1, not a part of the string
            int indexOfSubStr2 = str.IndexOf("Dev"); //returns 0, the index where the substring starts
            int indexOfSubStr3 = str.IndexOf("e", 4); //will look for 'e' starting at and inclusive of position 4 searching left to right
            int lastIndexOf = str.LastIndexOf("e"); //LastIndexOf() returning index of first instance from right to left 
            int lastIndexOf2 = str.LastIndexOf("e", 4); //passing a start location will start search right to left at that location
            bool isNullOrEmptyStr = string.IsNullOrEmpty(str); //return true or false based on wether the passed string is empty or unassigned(null)
            bool isNullOrWhiteSpace = string.IsNullOrWhiteSpace(str); //return true or false based on wether passed string is unassigned(null) or empty space ie: " "
            string formattedStr = string.Format("in the {0} {1}, there is lots to learn", str, str2); //old base way of formatting a string
            string formattedStr2 = new string($"in the {str} {str2}, there is lots to learn"); //new in C# 6.0 way of formatting a string
            string insetStr = str.Insert(3, "-"); //insert a string or char at first param location of original string
            string removeStr = str.Remove(3, str.Length - 3); //remove characters starting at first param, ending at second param 
            Regex regexValidation = new Regex("[A-Za-z]");
            bool isStrOnlyLetters = regexValidation.IsMatch(str);
            char[] vowels = ['A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u']; //a char requires single quotes instead of double quotes like a string

            int vowelsCount = 0;
            for (int i = 0; i < str.Length; i++) //iterating each char in the string
            {
                if (vowels.Contains(str[i])) //if the char of current iteration is found in vowels array
                    vowelsCount++; //increment the count
            }

            string[] words = ["Bird", "is", "the", "word"];
            StringBuilder mutableString = new(20, 50); //to set a MaxCapacity, it must be set on object initialization, as the second parameter
            for (int i = 0; i < words.Length; i++)
            {

                mutableString.Append(words[i] + " "); //StringBuilder objects have various methods to access and work with the string
            }
            mutableString[0] = 'V'; //individual characters can be accessed and modified in StringBuilder objects
            mutableString.Capacity = 30; //the character capacity of the StringBuilder string object can be set, and automatically doubles when filled
            mutableString.Length = 2; //length can also be set, keeping only 1st - value given characters, not 0 indexed.
            mutableString.Remove(0, 2);
            mutableString.Insert(0, "Developer");

            //StringBuilder is not a string, so to call string methods, it first must be cast ToString() to call string methods
            mutableString.Remove(mutableString.ToString().IndexOf("eloper"), mutableString.Length - mutableString.ToString().IndexOf("eloper"));

            Console.WriteLine($"{hello} {length} {h}");
            Console.WriteLine(str);
            Console.WriteLine(upperStr);
            Console.WriteLine(lowerStr);
            Console.WriteLine(subStr);
            Console.WriteLine(subStr2);
            Console.WriteLine(replaceStr);
            Console.WriteLine(replaceStr2);
            foreach (string splitStr in splitStrings) { Console.WriteLine(splitStr); }
            Console.WriteLine($"\rJoin() string: {joinStr}");
            Console.WriteLine(trimString);
            foreach (char character in charArrayFromStr) { Console.WriteLine(character); }
            Console.WriteLine(newStrFromCharArray);
            Console.WriteLine(equalsStr);
            Console.WriteLine(equalsStr2);
            Console.WriteLine(startsWithStr);
            Console.WriteLine(EndsWithStr);
            Console.WriteLine(ContainsStr);
            Console.WriteLine(indexOfSubStr);
            Console.WriteLine(indexOfSubStr2);
            Console.WriteLine(indexOfSubStr3);
            Console.WriteLine(lastIndexOf);
            Console.WriteLine(lastIndexOf2);
            Console.WriteLine(isNullOrEmptyStr);
            Console.WriteLine(isNullOrWhiteSpace);
            Console.WriteLine(formattedStr);
            Console.WriteLine(formattedStr2);
            Console.WriteLine(insetStr);
            Console.WriteLine(removeStr);
            Console.WriteLine($"is the string only upper or lowercase letters? {isStrOnlyLetters}.");
            Console.WriteLine(vowelsCount); //forloop count of vowels in string
            Console.WriteLine(str.Count(character => Array.IndexOf(vowels, character) >= 0)); //lambda to check if index of each char in string is != -1 in vowels array 
            Console.WriteLine(str.Count(character => vowels.Contains(character))); //lambda using default Contains method of vowels array
            Console.WriteLine($"string: {mutableString}, Capacity: {mutableString.Capacity}, Length: {mutableString.Length}");
        }
    }
}