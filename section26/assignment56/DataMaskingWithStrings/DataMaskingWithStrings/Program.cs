using System.Text;
using System.Text.RegularExpressions;

namespace DataMaskingWithStrings
{
    class Program
    {
        public static void Main()
        {
            //last two inputs are intentionally formatted incorrectly. 3rd is too short, last is missing a dash
            string[] testInputs = { "323-23-6785", "9809654387547698", "0560659", "55985523-2536-4557" };
            
            for (int i = 0; i < testInputs.Length; i++)
            {
                string tryMask = Masker.MaskData(testInputs[i]); //returns the masked data or error message
                Console.WriteLine($" test[{i + 1}]: Raw Data: {testInputs[i]} Masked Data: {tryMask} \n");
            }
        }
    }

    static class Masker
    {
        //route string to appropriate mask function
        public static string MaskData(string data)
        {
            string tempString = "";
            string[] splits = data.Split("-"); //remove any dashes from input data
            tempString = string.Join("",splits).Trim(); //join separated segments into one string, trim empty spaces
            (bool InputValid, string InputType) = Validate(tempString); //validate correct length and only numbers, identify if it's a SSN or CC

            switch (InputValid)
            {
                case true:
                    //route string input to masking method which returns a StringBuilder object
                    if (InputType == "SSN") 
                    { 
                        tempString = Mask(tempString).Insert(3, '-').Insert(6, '-').ToString();
                    }
                    else if (InputType == "CC") 
                    { 
                        tempString = Mask(tempString).Insert(4, '-').Insert(9, '-').Insert(14, '-').ToString(); 
                    }
                    break;
                default: 
                    //else defaults to invalid error message
                    tempString = $"Input length of {data.Length} is {InputType}. SSN has 9 digits and CC has 16."; 
                    break;
            }

            //returns the masked string with proper dashes and last 4 digits unmasked or error message if input failed validation
            return tempString;
        }

        private static (bool, string) Validate(string input)
        {
            Regex regexValidation = new Regex("^[0-9]+$");
            string InputType;

            //detect SSN as 9 digits or CC as 16 digits, new cases could be added to expand on other data types to validate
            switch (input.Length)
            {
                case 9:
                    InputType = regexValidation.IsMatch(input) ? "SSN" : "Invalid";
                    if (InputType == "SSN")
                    {
                        Console.WriteLine($"DEBUG: {input} passed validation as {InputType}...");
                        return (true, InputType);
                    }
                    Console.WriteLine($"DEBUG: {input} passed length validation but failed regex validation...");
                    break;
                case 16:
                    InputType = regexValidation.IsMatch(input) ? "CC" : "Invalid";
                    if (InputType == "CC")
                    {
                        Console.WriteLine($"DEBUG: {input} passed validation as {InputType}...");
                        return (true, InputType);
                    }
                    Console.WriteLine($"DEBUG: {input} passed length validation but failed regex validation...");
                    break;
                default:
                    Console.WriteLine($"DEBUG: {input} failed string length validation...");
                    break;
            }

            //if input.Length is not in a case accounted for, return "Invalid" InputType
            return (false, "Invalid");
        }

        //replace the sensitive data with a mask character ('X' character)
        private static StringBuilder Mask(string data)
        {
            StringBuilder maskedString = new StringBuilder();

            //mask numbers, do not mask last 4 digits
            for (int i = 0; i < data.Length; i++)
            {
                if (i <= data.Length - 5)
                {
                    maskedString.Append("X");
                }
                else 
                {
                    maskedString.Append(data[i]);
                }
            }

            //return masked StringBuilder object
            return maskedString;
        }
    }
}