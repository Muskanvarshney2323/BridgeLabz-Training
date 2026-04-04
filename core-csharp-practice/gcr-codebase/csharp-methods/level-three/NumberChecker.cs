using System;
using System.Collections.Generic;
static class NumberChecker
{
    public static int[] Digits(int n)
    {
        if (n == 0) return new int[] { 0 };
        List<int> ds = new List<int>();
        int x = Math.Abs(n);
        while (x > 0) { ds.Add(x % 10); x /= 10; }
        ds.Reverse();
        return ds.ToArray();
    }

    public static int DigitCount(int n) => Digits(n).Length;

    public static bool IsDuckNumber(int n)
    {
        foreach (int d in Digits(n)) if (d != 0) return true;
        return false;
    }

    public static bool IsArmstrong(int n)
    {
        int[] ds = Digits(n); int p = ds.Length; int sum = 0;
        foreach (int d in ds) sum += (int)Math.Pow(d, p);
        return sum == n;
    }

    public static (int largest, int secondLargest) LargestTwo(int n)
    {
        int[] ds = Digits(n);
        int max1 = Int32.MinValue, max2 = Int32.MinValue;
        foreach (int d in ds)
        {
            if (d > max1) { max2 = max1; max1 = d; }
            else if (d > max2) max2 = d;
        }
        return (max1, max2 == Int32.MinValue ? max1 : max2);
    }

    public static (int smallest, int secondSmallest) SmallestTwo(int n)
    {
        int[] ds = Digits(n);
        int min1 = Int32.MaxValue, min2 = Int32.MaxValue;
        foreach (int d in ds)
        {
            if (d < min1) { min2 = min1; min1 = d; }
            else if (d < min2) min2 = d;
        }
        return (min1, min2 == Int32.MaxValue ? min1 : min2);
    }

    // Task 3
    public static int SumOfDigits(int n) { int s = 0; foreach (int d in Digits(n)) s += d; return s; }
    public static double SumOfSquaresOfDigits(int n) { double s = 0; foreach (int d in Digits(n)) s += Math.Pow(d, 2); return s; }
    public static bool IsHarshad(int n) { int s = SumOfDigits(n); return s != 0 && n % s == 0; }

    public static int[,] DigitFrequencies(int n)
    {
        int[] ds = Digits(n);
        int[] freq = new int[10];
        foreach (int d in ds) freq[d]++;
        int[,] outArr = new int[10, 2];
        for (int i = 0; i < 10; i++) { outArr[i, 0] = i; outArr[i, 1] = freq[i]; }
        return outArr;
    }

    // Task 4
    public static int[] ReverseDigits(int n) { int[] ds = Digits(n); Array.Reverse(ds); return ds; }
    public static bool AreArraysEqual(int[] a, int[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;
        return true;
    }
    public static bool IsPalindrome(int n) => AreArraysEqual(Digits(n), ReverseDigits(n));

    // Task 5
    public static bool IsPrime(int n)
    {
        if (n <= 1) return false; if (n <= 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;
        for (int i = 5; i * i <= n; i += 6) if (n % i == 0 || n % (i + 2) == 0) return false;
        return true;
    }

    public static bool IsNeon(int n)
    {
        int s = 0; int sq = n * n; foreach (int d in Digits(sq)) s += d; return s == n;
    }

    public static bool IsSpy(int n)
    {
        int prod = 1; int sum = 0; foreach (int d in Digits(n)) { sum += d; prod *= d; }
        return sum == prod;
    }

    public static bool IsAutomorphic(int n)
    {
        long sq = (long)n * n; string s = sq.ToString(); string t = n.ToString();
        return s.EndsWith(t);
    }

    public static bool IsBuzz(int n) => (n % 7 == 0) || (Math.Abs(n) % 10 == 7);

    // Task 6
    public static int[] Factors(int n)
    {
        if (n == 0) return new int[0];
        int absn = Math.Abs(n);
        int count = 0; for (int i = 1; i <= absn; i++) if (absn % i == 0) count++;
        int[] f = new int[count]; int idx = 0; for (int i = 1; i <= absn; i++) if (absn % i == 0) f[idx++] = i;
        return f;
    }

    public static int GreatestFactor(int n) { int[] f = Factors(n); return f.Length > 0 ? f[f.Length - 1] : 0; }
    public static int SumFactors(int n) { int s = 0; foreach (int v in Factors(n)) s += v; return s; }
    public static long ProductFactors(int n) { long p = 1; foreach (int v in Factors(n)) p *= v; return p; }
    public static double ProductOfCubesOfFactors(int n) { double p = 1; foreach (int v in Factors(n)) p *= Math.Pow(v, 3); return p; }

    public static bool IsPerfect(int n)
    {
        if (n <= 1) return false; int sumProper = 0; foreach (int v in Factors(n)) if (v != n) sumProper += v; return sumProper == n;
    }

    public static bool IsAbundant(int n) { if (n <= 0) return false; int sumProper = 0; foreach (int v in Factors(n)) if (v != n) sumProper += v; return sumProper > n; }
    public static bool IsDeficient(int n) { if (n <= 0) return false; int sumProper = 0; foreach (int v in Factors(n)) if (v != n) sumProper += v; return sumProper < n; }

    public static long Factorial(int x) { long r = 1; for (int i = 2; i <= x; i++) r *= i; return r; }
    public static bool IsStrong(int n) { int sum = 0; foreach (int d in Digits(n)) sum += (int)Factorial(d); return sum == n; }
}