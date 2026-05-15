using System;

class Program
{
    static void Main()
    {
        // Ask user for their grade percentage
        Console.Write("What is your grade percentage? ");
        int gradePercentage = int.Parse(Console.ReadLine());
        
        string letter = "";
        string sign = "";
        
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        
        if (letter != "F")
        {
            int lastDigit = gradePercentage % 10;
            
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            else
            {
                sign = "";
            }
            
            if (letter == "A" && sign == "+")
            {
                sign = "";  
            }
        }
        else
        {
            sign = ""; 
        }

        Console.WriteLine($"Your letter grade is: {letter}{sign}");
        
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course!");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep working hard and you'll get it next time.");
        }
    }
}
