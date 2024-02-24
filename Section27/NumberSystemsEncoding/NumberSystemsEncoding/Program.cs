
using System.Text;

namespace NumberSystemEncoding
{
    class Program
    {
        static void Main()
        {
            //numbers are by default decimal system
            int decimalNumber = 100;

            //different number systems are encoded by identifying the remainder of the decimal number divided by the base number.
            //ie. 100 % 2 = 0, 50 % 2 = 0, 25 % 2 = 1, 12 % 2 = 0, 6 % 2 = 0, 3 % 2 = 1, 1 % 2 = 1. note that if the divide operation results in 0, the remainder is 1
            //the binary number in the example above is determined by sequencing the remainders backwards in the calculation
            //which is then prefixed by 0b in the case of binary, so 0b100100 is binary for 100
            BinaryNumbers();

            //octal system divides/remainder using the same logic as the binary calculation, except that the numbers are 0-7
            //so the numbers are divided by 8, with remainders 0-7, and the result is prefixed by 0o
            OctalNumbers();

            //hexadecimal numbers are also calculated same way as the binary example above, dividing/remainder by 16
            //hexadecimal numbers are 0-15, where 10-15 are the first letters of the alphabet
            //ie. 0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f - so 15 is f for example.
            //this also applies when calculating the hexadecimal representation of a decimal, 742 would be 2"e"6 instead of 2"14"6, minus the quotes.
            HexadecimalNumbers();

            //encoding is the process of converting characters, numbers and symbols into a a code or number that represents the original
            //this is important for writing, reading or transffering file data.
            AsciiEncoding();
            UnicodeEncoding();
        }
        
        static void BinaryNumbers()
        {
            int decimalNumber = 100;
            string binaryString = Convert.ToString(decimalNumber, 2); //ToString has overloads to convert to encoded formats, where the second param is the base number, here 2 is for binary.
            Console.WriteLine(binaryString); //this prints 1100100 instead of 100, because it is converted into a binary string
            int binaryToDecimal = Convert.ToInt32(binaryString, 2); //base number passed as second param
            Console.WriteLine(binaryToDecimal); //here the binary string is converted back into a decimal
            decimalNumber = 0b1100100; //string literal can be passed to an int, which will automatically convert to decimal
        }

        static void OctalNumbers()
        {
            int decimalNumber = 100;
            string decimalToOctalString = Convert.ToString(decimalNumber, 8); //note octal is base 8
            int octalToDecimal = Convert.ToInt32(decimalToOctalString, 8);
            //octal system does not accept string literal
        }

        static void HexadecimalNumbers()
        {
            int decimalNumber = 100;
            string HexadecimalString = Convert.ToString(decimalNumber, 16); //hexadecimal base 16
            decimalNumber = 0x2e6; //0x prefix for hexadecimal, accepts string literal
            Console.WriteLine(decimalNumber);
            int HexToDecimal = Convert.ToInt32(HexadecimalString, 16); //converting back to decimal
        }

        static void AsciiEncoding()
        {
            //ASCII characters have a decimal and hexadecimal representation for each of the 127 characters.
            //a byte value type can hold values 0-256, so for optimization byte is best when working with ASCII codes.
            //however the value type need not be a byte. int, long, double etc will work as well, but consume more memory to do the same.
            //when a supported ASCII symbol or character is cast into a byte or sbyte, it converts into the ASCII representation of that symbol or letter.
            char letter = 'A'; //this is A
            byte AsciiCodeOfLetter = (byte)letter; //this is 65, the ASCII representation of A.
            char AsciiToChar = (char)AsciiCodeOfLetter; //this is A again, converted from the ASCII representation 65

            byte[] codes = new byte[128];
            for (byte i = 0; i < 128; i++) //adding numbers 0-127 into an array
            {
                codes[i] = i;
            }

            for (byte i = 0; i < codes.Length; i++) //printing out all ASCII symbols represented by the values in the codes array.
            {
                //OS may play exception sound due to missing character package, and output would be blank for missing symbol.
                Console.Write($"{i}: {(char)codes[i]} ");
            }

            //informs the Console of the format being output
            //even then, some symbols dont make sense, just show rectangular boxes.
            //this is because some ASCII symbols represent special keystrokes, like Enter, Esc, etc, not regular symbols.
            Console.OutputEncoding = System.Text.Encoding.ASCII;

            //instead of calling each character individually, there is a method to get the full string from the array of ASCII codes
            string stringFromAscii = System.Text.Encoding.ASCII.GetString(codes);
            Console.WriteLine(stringFromAscii);

            //instead of manually iterating each char there is a method for that: System.Text.Encoding.ASCII.GetBytes()
            //this can be useful for acquiring a series of bytes from the characters of a string, which can be streamed into a filesystem for example
            string testInput = "123 downtown street, Bigcity, Capital 12345";
            byte[] testInputBytes = System.Text.Encoding.ASCII.GetBytes(testInput); //will generate a ASCII numerical value 0-127 for each character in the input string
            string backToStringFromAsciiBytes = System.Text.Encoding.ASCII.GetString(testInputBytes);
            Console.WriteLine(backToStringFromAsciiBytes); //prints same string as testInput
            Console.ReadKey();
        }

        static void UnicodeEncoding()
        {
            char unicodeLiteral = '¢'; //unicode can be directly pasted into C#
            char unicode = '\u00A2'; //by using the escape sequence \u what would be read as a string, is instead read as unicode
            string testInput = "123 downtown street, Bigcity, Capital 12345";
            byte[] unicodeBytesFromString = System.Text.Encoding.Unicode.GetBytes(testInput); //returns a byte array with multiple bytes for each character/unicode in the param string
            string backToStringFromUnicodeBytes = System.Text.Encoding.Unicode.GetString(unicodeBytesFromString); //reads the byte array to convert the unicode back to characters and string
            Console.WriteLine(unicodeLiteral);
            Console.WriteLine(unicode);
            Console.WriteLine(backToStringFromUnicodeBytes);
        }
    }
}