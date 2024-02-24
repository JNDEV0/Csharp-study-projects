
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;

namespace ExceptionHandling
{
    class Program
    {
        static void Main()
        {
            //if an error is encountered, it will "throw" an exception which must be "catched" or it will crash the program.
            //throwing an exception is reporting back. if something that was expected to be true as part of the contract is violated,
            //which leads to an impossible impass, which cannot be remedied then and there, then an exception should be thrown,
            //an exception can be intentionally thrown using the throw keyword, but having exceptions is best avoided altogether.

            //TryCatchFinally();
            //Format();
            //IndexOutOfRange();
            //NullReference();
            //ArgumentNull();
            //Inner();
            //ArgumentOutOfRange();
            //Argument();
            //InvalidOperation();
            //CustomExceptions();
            //StackTracing();
            //Logger();
            //SystemException();
            ExceptionFilters();
        }

        static void TryCatchFinally()
        {
            int a, b;
            string input;

            //if any line of the content inside the try block throws an exception
            //the remaining content of the try block will be skipped when the exception is thrown
            try //the try block will execute procedurally, that is, after the code above it
            {
                Console.WriteLine("Division");
                Console.Write("Enter first digit: ");
                input = Console.ReadLine();
                a = int.Parse(input);
                Console.Write("Enter second digit: "); 
                input = Console.ReadLine(); //what if user enters 0? 
                b = int.Parse(input);
                int c = a / b; //this will throw a DivideByZeroException exception, since any number divided by 0 is infinity.
                Console.WriteLine($"Result of division of {a} by {b} = {c}"); //this line would be skipped if above line throws an exception
            } 
            //the catch block below will specifically catch a DivideByZeroException, multiple catch blocks can be specified for different exception types
            //if any other type of exception is thrown, it will not catch it and it will crash the program unless specifically mentined by name,
            //if the parantheses is excluded or mentions the base Exception class any type of Exception would be caught
            //if a reference name is given to the exception, the second word of the parameter, it can be used within the catch block
            catch (DivideByZeroException exception) //the catch block will execute in case it catches an exception
            {
                Console.WriteLine(exception.Message); //every Exception derived class will have a Message property, amongst others.
            }
            //the finally block is optional, tipically used to execute logic that needs to occur to wrap up whatever was taking place in the try block
            //for example, closing a database connection, or filestream. this is useful because the try block will break out and skip code
            //after any line that throws an exception, and if the connection was to be closed there, it'd be skipped
            //and the catch block is used specifically to handle exceptions, while the finally block will execute wether or not there is an exception
            finally //the finally block will execute after try block if no exception, or after catch if exception. either way, finally executes.
            {
                Console.WriteLine("Program finished...");
            }
        }
        static void Format()
        {
            //FormatException is thrown when an a reference has a set data type and an attempt is made to assign the wrong data type to it
            //for example, attempting to set a string value to an int reference, or passing a single Customer objent into a List<Customer> property
            //in the example below, correct input is being naively expected
            //if any letter or symbol is passed to ba.AccountNumber or ba.AccountBalance, the Parse() method will throw a FormatException
            //since those cannot be converted into an int or double
            try
            {
                BankAccount ba = new BankAccount();
                Console.WriteLine("Enter the account holder's name:");
                ba.AccountHolderName = Console.ReadLine();
                Console.WriteLine("Enter account number:");
                ba.AccountNumber = int.Parse(Console.ReadLine()); //will throw FormatException if input is a2342asd for example
                Console.WriteLine("Enter account balance:");
                ba.AccountBalance = double.Parse(Console.ReadLine());
                Console.WriteLine($"Name: {ba.AccountHolderName}, AccountNumber: {ba.AccountNumber}, AccountBalance: {ba.AccountBalance}");
            }
            catch (FormatException exception) //this catch block will catch the FormatException once thrown, however it will not catch any others
            {
                //any other logic can also execute within the catch block, such as routing the exception to an external error handling method.
                Console.WriteLine("The format of the input data was not adequate."); //writing a custom exception message to console
                Console.WriteLine(exception.Message); //describes the issue that caused the exception to be thrown
                Console.WriteLine(exception.StackTrace); //provides information on where the exception was thrown from
            }
        }
        static void IndexOutOfRange()
        {
            //IndexOutOfRangeException means that the queried object in a given array is beyond the limit of that array
            //the array below has 3 BankAccount instances, and so correct indexes would be 0-2
            BankAccount[] bankAccounts = {
                new BankAccount() { AccountNumber = 1, AccountHolderName = "Smith", AccountBalance = 1001.1},
                new BankAccount() { AccountNumber = 2, AccountHolderName = "Wayne", AccountBalance = 2002.2},
                new BankAccount() { AccountNumber = 3, AccountHolderName = "Patel", AccountBalance = 3003.3},
            };

            //given the i < bankAccounts.Length condition, the for loop never goes beyond the last instance in the array, thus no exception
            for (int i = 0; i < bankAccounts.Length; i++)
            {
                Console.WriteLine($"AccountNumber: {bankAccounts[i].AccountNumber}, AccountHolder: {bankAccounts[i].AccountHolderName}, AccountBalance: {bankAccounts[i].AccountBalance}");
            }

            //in this example no input checking is taking place, which is not ideal
            //unreliable input should always be checked to avoid an exception all together
            try
            {
                //the serial number is the array position of the account in question starting at 1, so serial input of 1 would be index 0.
                Console.WriteLine("Enter Account Serial Number: ");
                int serialNumber = int.Parse(Console.ReadLine()) - 1; //what if user input is 4 or more?
                BankAccount selectedBankAccount = bankAccounts[serialNumber]; //this will then throw an IndexOutOfRangeException
                Console.WriteLine($"Selected AccountNumber: {selectedBankAccount.AccountNumber}, AccountHolder: {selectedBankAccount.AccountHolderName}, AccountBalance: {selectedBankAccount.AccountBalance}");
            }
            catch (IndexOutOfRangeException exception)
            {
                Console.WriteLine($"Invalid account Number selected, there are only {bankAccounts.Length} accounts."); //here Length is 3
                Console.WriteLine(exception.Message);
            }


        }
        static void NullReference()
        {
            //when a reference points to a null object, that is, a non existant value,
            //and something attempts to access te object of that reference, a NullReferenceException is thrown
            BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Bob", AccountBalance = 11000.00 };
            BankAccount ba2 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Billy", AccountBalance = 9000.00 };
            BankAccount ba3 = null;
            double transferAmount = 1000;

            //no problem here, since both ba1 and ba2 are not null
            if (FundsTransfer.TransferExceptionHandled(ba1, ba2, transferAmount))
            {
                Console.WriteLine($"Transfer of {transferAmount} successful: ba1.AccountBalance: {ba1.AccountBalance}, ba2.AccountBalance: {ba2.AccountBalance}");
            }
            else
            {
                Console.WriteLine("Transfer failed.");
            }

            //here FundsTransfer.Transfer will throw the NullReferenceException,
            //since it is not handled inside the Transfer() method, it will come up to the line where the method is called
            //the stack trace will show the line where the exception occured inside the Transfer() method,
            //then the line below that calls the method, and finally the line that called the NullReference() method of this Program class
            try
            {
                if (FundsTransfer.TransferExceptionHandled(ba1, ba3, transferAmount))
                {
                    Console.WriteLine($"Transfer of {transferAmount} successful: ba1.AccountBalance: {ba1.AccountBalance}, ba2.AccountBalance: {ba2.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Transfer failed.");
                }
            }
            catch (NullReferenceException exception)
            {
                //NullReferenceException only points to the line where the Exception occured, it does not mention which object was null
                //often times Exceptions, unless customized, will not return detailed information about what took place,
                //which is another reason to pre-emptively detect and resolve situations that can lead to exceptions before they take place
                Console.WriteLine("One of the bank accounts was of null type, not a valid BankAccount object!");
                Console.WriteLine(exception.Message);
            }
        }
        static void ArgumentNull()
        {
            //ArgumentNullException is thrown when a parameter is passed to a method as null, when it should not be null
            //typically this is thrown back to the method caller, and the specific argument/parameter that is null can be named in the error thrown
            BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Bob", AccountBalance = 11000.00 };
            BankAccount ba2 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Billy", AccountBalance = 9000.00 };
            BankAccount ba3 = null;
            double transferAmount = 1000;

            try
            {
                //since ba3 is in fact null, TransferExceptionHandled() method will check it before executing the transfer
                //and throw ArgumentNullException as described in its method definition
                if (FundsTransfer.TransferExceptionHandled(ba1, ba3, transferAmount))
                {
                    Console.WriteLine($"Transfer of {transferAmount} successful: ba1.AccountBalance: {ba1.AccountBalance}, ba2.AccountBalance: {ba2.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Transfer failed.");
                }
            }
            catch (ArgumentNullException exception)
            {
                //ArgumentNullException is a bit more specific than NullReferenceException,
                //and its constructor can actually take the name of the arg/parameter that is having the issue
                //often times Exceptions, unless customized, will not return detailed information about what took place,
                //which is another reason to pre-emptively detect and resolve situations that can lead to exceptions before they take place
                Console.WriteLine(exception.Message);
            }
        }
        static void Inner()
        {
            //InnerException is an exception included within another exception that was intentionally thrown
            //as a way to allow the caller of the method that threw the exception to look into the issue deeper
            //typically this is done within a closed system amongst developers,
            //as additional details of whats causing an exception shouldnt be mentioned to outside entities
            BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Bob", AccountBalance = 11000.00 };
            BankAccount ba2 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Billy", AccountBalance = 9000.00 };
            BankAccount ba3 = null;
            double transferAmount = 1000;

            try
            {
                //since ba3 is in fact null, TransferInnerException() method will NOT check it before attempting to executing the transfer
                //for the sake of the example, and throw ArgumentNullException as described in its method definition
                //with NullReferenceException as the InnerException
                if (FundsTransfer.TransferInnerException(ba1, ba3, transferAmount))
                {
                    Console.WriteLine($"Transfer of {transferAmount} successful: ba1.AccountBalance: {ba1.AccountBalance}, ba2.AccountBalance: {ba2.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Transfer failed.");
                }
            }
            catch (ArgumentNullException exception)
            {
                //ArgumentNullException is a bit more specific than NullReferenceException,
                //and its constructor can actually take the name of the arg/parameter that is having the issue
                //or an InnerException that caused the exception often times unless customized,
                //exceptions will not return detailed information about what took place,
                //which is another reason to pre-emptively detect and resolve situations that can lead to exceptions before they take place
                Console.WriteLine(exception.Message);

                if (exception.InnerException is not null)
                Console.WriteLine("InnerException: " + exception.InnerException.Message);
            }
        }
        static void ArgumentOutOfRange()
        {
            //ArgumentOutOfRangeException is thrown when a parameter's value is not within the acceptable range
            //for example, the acceptable transfer amount is > 0 and <= 10000
            BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Bob", AccountBalance = 11000.00 };
            BankAccount ba2 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Billy", AccountBalance = 9000.00 };
            BankAccount ba3 = null;
            double transferAmount = 10001;

            try
            {
                //since the transferAmount is higher than the limit stipulated in TransferExceptionHandled() definition,
                //the called method will throw ArgumentOutOfRangeException which will be caught by the catch block below
                if (FundsTransfer.TransferExceptionHandled(ba1, ba2, transferAmount))
                {
                    Console.WriteLine($"Transfer of {transferAmount} successful: ba1.AccountBalance: {ba1.AccountBalance}, ba2.AccountBalance: {ba2.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Transfer failed.");
                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                //ArgumentOutOfRangeException can receive the arg name in string form that caused the exception to be thrown
                //as well as the value passed to it, and an exception message
                Console.WriteLine($"{exception.ParamName} value of {exception.ActualValue} is outside the acceptable transfer amount.");
                Console.WriteLine(exception.Message);
            }
        }
        static void Argument()
        {
            //ArgumentException is the parent of ArgumentNull and ArgumentOutOFRange exceptions
            //if the argument being null or out of range is not exactly relevant, ArgumentException is best
            //if for example, the argument passed is 8 digits instead of an expected maximum of 6,
            //or a property of the argument passed object is false instead of expected true.
            try 
            {
                //here the attempt to instantiate BankAccount with a negative balance will throw the ArgumentException
                BankAccount ba2 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Billy", AccountBalance = -9000.00 };
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message, exception.ParamName);
            }
        }
        static void InvalidOperation()
        {
            //InvaLidOperationException is thrown to signal that an attempted operation is not allowed for some reason
            //for example, a List<Class> collection has Class objects which do not implement
            //the IComparable interface which is needed to accurately Sort() them in comparison to each other
            //if the Sort() method is called, the InvalidOperationException may be thrown instead of performing a botched sorting
            
            //in this example, an transfer is attempted from ba1 to ba2 of an amount beyond ba1's AccountBalance
            BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Bob", AccountBalance = 110.00 };
            BankAccount ba2 = new BankAccount() { AccountNumber = 1, AccountHolderName = "Billy", AccountBalance = 1000.00 };
            double transferAmount = 500;

            try
            {
                //since the transferAmount is higher than the available balance of ba1 account
                //the called method will throw InvalidOperationException which will be caught by the catch block below
                if (FundsTransfer.TransferExceptionHandled(ba1, ba2, transferAmount))
                {
                    Console.WriteLine($"Transfer of {transferAmount} successful: ba1.AccountBalance: {ba1.AccountBalance}, ba2.AccountBalance: {ba2.AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Transfer failed.");
                    throw new IOException();
                }
            }
            catch (InvalidOperationException exception)
            {
                //InvalidOperationException signals that the operation of TransferExceptionHandled was invalid
                //the exception message explains the balance insufficient issue
                Console.WriteLine(exception.Message);
            }
        }
        static void CustomExceptions()
        {
            //a CustomException as the name implies, is a customized exception made to communicate an issue
            //that the default exceptions do not accurately explain
            //for example a Login() method receives credentials which do not pass validation
            //there is no default InvalidCredentialsException, and so a custom exception is made

            //in the example below, a CustomException is made to signal an invalid name format for BankAccount creation
            try
            {
                BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "$34Sm1th", AccountBalance = 100};
            }
            catch (InvalidFormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                //InvalidFormatException is derived from InvalidOperationException
                //so if an InvalidFormatException is thrown, a catch (InvalidOperationException) will also catch the derived class
                Console.WriteLine(exception.Message);
            }
        }
        static void StackTracing()
        {
            //StackTrace is a property of Exceptions that describes the method call order that resulted in the exception
            //for example, Method1() calls Method2() which calls Method3(), which then throws an exception
            //the StackTrace would then point to the line of each method in call order to trace back the origin of the method call stack
            try
            {
                //due to the incorrect format in the AccountHolderName, an InvalidFormatException will be thrown
                BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "$34Sm1th", AccountBalance = 100 };
            }
            catch (InvalidOperationException exception)
            {
                //InvalidFormatException is derived from InvalidOperationException
                //so if an InvalidFormatException is thrown, a catch (InvalidOperationException) will also catch the derived class
                Console.WriteLine(exception.Message);
                
                //will print method names and line numbers of where the calls took place
                //in order from the start of the call stack all the way to the line in the method that threw the exception
                //this is useful for debuggings with breakpoints, where the program can be executed line by line
                //while examining the runtime memory to figure out whats causing the issue
                Console.WriteLine(exception.StackTrace); 
            }
        }
        static void Logger()
        {
            //the exception log will keep a log file of exceptions occured during the program's runtime
            //for example the program is sent to a remote user, who encounters problems, instead of asking for details
            //the log file can be sent to the developer and evaluated to see any exception details written to the log file
            //however the file storage of the exception details is not automatic,
            //code must be specified to write exceptions into the file
            try
            {
                //due to the incorrect format in the AccountHolderName, an InvalidFormatException will be thrown
                BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "$34Sm1th", AccountBalance = 100 };
            }
            catch (InvalidFormatException exception)
            {
                Console.WriteLine(exception.Message);

                //this custom class ExceptionLogger has a static method that receives the Exception object
                //and then writes the Exception details along with DateTime.Now to the ErrorLog.txt file
                ExceptionLogger.WriteExceptionToFile(exception);
                Console.WriteLine("DEBUG: Exception written into the log file.");
            }

        }
        static void SystemException()
        {
            //all exceptions wether default or custom are derived from System.Expection, which is derived from object type
            //NullReferenceException, FormatException, IndexOutOfRangeException, DivideByZeroException,
            //InvalidOperationException, NotImplementedException, NotSupportedException, IOException are some of the default Exceptions
            //When an Exception type catch block is defined, it will catch any exceptions correctly derived from Exception
            //thought it should be the last catch block since it will catch them all otherwise preventing specialized exception handling
            try
            {
                //due to the incorrect format in the AccountHolderName, an InvalidFormatException will be thrown
                BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "$34Sm1th", AccountBalance = 100 };
            }
            catch (Exception exception)
            {
                //even though the exception thrown is InvaLidFormatException, the catch statement catches it
                //because it is derived from Exception class. also it internally retains its actual type,
                //GetType() will show its a InvalidFormatException
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.GetType());
            }
        }
        static void ExceptionFilters()
        {
            //the when keyword when writted with a catch block, will apply a condition to when the Exception will be caught
            try
            {
                //due to the incorrect format in the AccountHolderName, an InvalidFormatException will be thrown
                BankAccount ba1 = new BankAccount() { AccountNumber = 1, AccountHolderName = "$34Sm1th", AccountBalance = 100 };
            }
            //the when condition needs to match for the exception to be caught
            //this can be useful to catch exceptions of specific ParamName for example
            catch (Exception exception) when (typeof(InvalidFormatException) == exception.GetType())
            {
                //even though the Exception type is defined in the catch statement,
                //because of the when keyword, the exception will only be caught when the condition is also met,
                //in this case when the Exception type is in fact InvalidFormatException
                //Even thought the exception caught is of Exception class, it internally retains its actual type,
                //GetType() will show its a InvalidFormatException, thus meeting the condition and entering into this block
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.GetType());
            }
        }
    }

    class BankAccount
    {
        private string _accountHolderName; 
        public string AccountHolderName 
        { 
            get => _accountHolderName; 
            set 
            {
                Regex nameVerificiation = new Regex(@"^[a-zA-Z]+$");
                if (!nameVerificiation.IsMatch(value))
                { 
                    throw new InvalidFormatException($"Invalid AccountHolderName format {value}, must be uppercase and lowercase letters only.");
                }
                else
                {
                    _accountHolderName = value;
                }
            } 
        }
        public int AccountNumber { get; set; }
        private double _currentBalance;
        public double AccountBalance 
        { 
            get => _currentBalance; 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        $"The amount of CurrentBalance should be a positive value. Amount of {value} is not allowed.",
                        "AccountBalance");
                }
                else
                {
                    _currentBalance = value;
                }
            }   
        }
    }

    static class FundsTransfer
    {
        public static bool TransferExceptionHandled(BankAccount source, BankAccount destination, double amount)
        {
            if (source is null || destination is null)
            {
                string whichIsNull =
                    source is null && destination is null ? "Source and destination accounts" :
                    source is null ? "Source account" :
                    destination is null ? "Destination account" : "";
                string errorMsg = " is null, must be a valid object instance to make a transfer.";
                    throw new ArgumentNullException(whichIsNull, errorMsg);
            } 
            else if (amount > source.AccountBalance)
            {
                string errorMsg = $"Insufficient balance in source account. Transfer amount of {amount} is greater than available balance of {source.AccountBalance}.";
                throw new InvalidOperationException(errorMsg);
            }
            else if (amount <= 0 || amount > 10000)
            {
                string errorMsg = "Transfer amount is invalid, must be greater than 0.";
                throw new ArgumentOutOfRangeException("amount", amount, errorMsg);
            }
            else
            {
                if (source.AccountBalance > amount) 
                { 
                    source.AccountBalance -= amount;
                    destination.AccountBalance += amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool TransferInnerException(BankAccount source, BankAccount destination, double amount)
        {
            try //in this try block the exception is not handled pre-emptively, which is not the prefered method
            {
                if (source.AccountBalance > amount)
                {
                    source.AccountBalance -= amount;
                    destination.AccountBalance += amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException exception)
            {
                if (source is null || destination is null)
                {
                    string whichIsNull =
                        source is null && destination is null ? "source and destination accounts" :
                        source is null ? "source account" :
                        destination is null ? "destination account" : "";
                    string errorMsg = whichIsNull + " is null, must be a valid object instance to make a transfer.";

                    //here the NullReferenceException is included as an InnerException to the ArgumentNullException
                    //as a way of allowing the calling method to dig deeper into whats causing the exception
                    throw new ArgumentNullException(errorMsg, exception); 
                }
                else if (amount <= 0)
                {
                    string errorMsg = " Transfer amount is invalid, must be greater than 0.";
                    throw new ArgumentNullException(errorMsg);
                }
            }
            return false;
        }
    }

    public class InvalidFormatException : InvalidOperationException
    {
        public InvalidFormatException() { }
        public InvalidFormatException(string message) : base(message) { }
        public InvalidFormatException(string message, Exception? innerException) : base(message, innerException) { }
    }

    public static class ExceptionLogger
    {
        public static void WriteExceptionToFile(Exception exception)
        {
            string path = @"E:\source\repos\c#Course\Section28\ExceptionHandling\ErrorLog.txt";
            using (StreamWriter fileStream = File.AppendText(path))
            {
                //the format in which the exception details are written into the file can be easily adjusted for readability
                fileStream.WriteLine($"[{DateTime.Now}]\n{exception.GetType()}\n{exception.Message}\n{exception.StackTrace}\n---");
            }
        }
    }

}