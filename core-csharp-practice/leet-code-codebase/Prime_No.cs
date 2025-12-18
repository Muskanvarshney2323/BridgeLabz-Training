using System;
public class Prime_No{  

    public static void Main(string[] args){
        Console.WriteLine(IsPrime(31));
    }
    public static bool IsPrime(int number){
        if (number <= 1) 
           return false;
        for (int i = 2; i <= Math.Sqrt(number); i++){
            if (number % i == 0)
                return false;
        }
        return true;
    }
}