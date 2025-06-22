// xercise 7: Financial Forecasting
using System;

class Program
{
    static double CalculateFutureValue(double presentValue, double rate, int years)
    {
        if (years == 0)
            return presentValue;
        return CalculateFutureValue(presentValue * (1 + rate), rate, years - 1);
    }

    static void Main()
    {
        double presentValue = 10000;  
        double annualGrowthRate = 0.08; 
        int years = 5;

        double futureValue = CalculateFutureValue(presentValue, annualGrowthRate, years);

        Console.WriteLine($"Future value after {years} years: ₹{futureValue:F2}");
    }
}
