/* 
 * global using
allows project wide use of a namespace instead of importing into each file.
recommended to import into a dedicated global namespaces file.
    global using System.Collections.Generic; 
instead of importing into each file:
    using System.Collections.Generic;

C# 10 also implicitly imports some namespaces:
    System
    System.Collections.Generic
    System.IO
    System.Linq
    System.Net.Http
    System.Threading
    System.Threading.Tasks
to disable implicit imports alter the .csproj file:
    <ImplicitUsings>enable/disable</ImplicitUsings>

* Module initialzer
runs before the Main() method that is the usual entry point of a program.
useful for initializing logic that needs to run one time at the start of a program,
such as initializing configuration environment variables that may be URIs to servers, session keys, 
initializing database connections, etc anything that need to be set up prior to the program running.
prepend the method with:

    [ModuleInitializer]
    void MethodSignature() {...}


 */
global using System;
using System.Runtime.CompilerServices;

namespace NewFeatures
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("This is the Main() Method.");
            //Initializers below will run before Main() method
            ReferenceNullAndForgiving();
            TargetTypedNew();
        }

        //must be static, return void, and have no parameters.
        //cannot be private or protected, must be internal or public. cannot be a generic method.
        //the method can be part of a static or non static class, which cannot be a generic class.
        //cannot be a local function inside another method.
        [ModuleInitializer]
        internal static void InitializerA()
        {
            Console.WriteLine("Initializer will run before Main Method.");
        }

        //multiple initializers are allowed, and will follow alpha-numerical order.
        //in this case Main() runs after InitializerB(), which runs after InitializerA()
        //other methods can be called from inside a single initializer too,
        //such as InitializerA() calling InitializerB(), instead of a separate [ModuleInitializer] tag on InitializerB()
        //this second way of calling methods from within a single initializer allows choosing the order or execution instead of default order.
        [ModuleInitializer]
        internal static void InitializerB()
        {
            Console.WriteLine("Initializer will run before Main Method.");
        }

        internal static void ReferenceNullAndForgiving()
        {
            Example example; //unitialized, null IDE warns with green underlone
            Example? example2 = null; //null, IDE recognizes ? modifier does not warns with green underlone

            //Console.WriteLine(example); //because example is not nullable, will not compile.
            Console.WriteLine(example2?.fieldName!); //IDE recognizes the nullable? and forgiving! so even though both are null, it compiles but prints nothing
        }

        internal static void TargetTypedNew()
        {
            //normal initializing
            Example example = new Example();

            //target-typed new statement, recognizes Example reference type
            Example example2 = new();

            //target-typed new can also be typed as parameter
            //say definiton for method ProcessEmployee(Employee obj) receives an Employee obj
            //the method could be called as:
            //ProcessEmployee(new("Employee Name", 22));    //where Employee is being directly instantiated in passed argument
        }

        internal static void PatternMatching()
        {

        }

    }

    class Example
    {
        public string fieldName { get; set; }
    }
}