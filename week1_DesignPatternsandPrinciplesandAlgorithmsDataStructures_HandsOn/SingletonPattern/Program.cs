//Exercise 1: Implementing the Singleton Pattern
using System;

class Program
{
    static void Main()
    {
    
        Logger logger1 = Logger.GetInstance();
        Logger logger2 = Logger.GetInstance();

        logger1.Log("This is the first log message.");
        logger2.Log("This is the second log message.");

        if (ReferenceEquals(logger1, logger2))
        {
            Console.WriteLine("logger1 and logger2 refer to the same instance.");
        }
        else
        {
            Console.WriteLine("logger1 and logger2 refer to different instances.");
        }
    }
}
