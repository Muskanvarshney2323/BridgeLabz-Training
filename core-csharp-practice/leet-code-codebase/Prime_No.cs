public class Primr_No{  

    public static void Main(string[] args){
        cosnole.WriteLine(IsPrime(29));
    }
    public bool IsPrime(int number){
        if (number <= 1) 
           return false;
        for (int i = 2; i <= Math.Sqrt(number); i++){
            if (number % i == 0)
                return false;
        }
        return true;
    }
}