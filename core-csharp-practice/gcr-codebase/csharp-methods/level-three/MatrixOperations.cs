using System;
class MatrixOperations
{
    static void Main()
    {
        int[,] a = RandomMatrix(3,3);
        int[,] b = RandomMatrix(3,3);
        Console.WriteLine("Matrix A:"); Display(a);
        Console.WriteLine("Matrix B:"); Display(b);
        Console.WriteLine("A + B:"); Display(Add(a,b));
        Console.WriteLine("A - B:"); Display(Subtract(a,b));
        Console.WriteLine("A * B:"); Display(Multiply(a,b));
        Console.WriteLine("Transpose A:"); Display(Transpose(a));
        Console.WriteLine($"Determinant A (2x2) = {Determinant2x2(a)}");
        Console.WriteLine($"Determinant A (3x3) = {Determinant3x3(a)}");
        Console.WriteLine($"Inverse A (2x2):"); DisplayInverse2x2(a);
        Console.WriteLine($"Inverse A (3x3):"); DisplayInverse3x3(a);
    }

    static int[,] RandomMatrix(int r,int c)
    {
        var rnd = new Random(); int[,] m = new int[r,c]; for (int i=0;i<r;i++) for (int j=0;j<c;j++) m[i,j] = rnd.Next(1,10); return m;
    }

    static int[,] Add(int[,] a,int[,] b)
    {
        int r=a.GetLength(0), c=a.GetLength(1); int[,] res = new int[r,c]; for (int i=0;i<r;i++) for (int j=0;j<c;j++) res[i,j]=a[i,j]+b[i,j]; return res;
    }
    static int[,] Subtract(int[,] a,int[,] b)
    {
        int r=a.GetLength(0), c=a.GetLength(1); int[,] res = new int[r,c]; for (int i=0;i<r;i++) for (int j=0;j<c;j++) res[i,j]=a[i,j]-b[i,j]; return res;
    }
    static int[,] Multiply(int[,] a,int[,] b)
    {
        int r = a.GetLength(0), c = b.GetLength(1), k = a.GetLength(1);
        int[,] res = new int[r,c];
        for (int i=0;i<r;i++) for (int j=0;j<c;j++) for (int t=0;t<k;t++) res[i,j]+=a[i,t]*b[t,j];
        return res;
    }
    static int[,] Transpose(int[,] a){int r=a.GetLength(0), c=a.GetLength(1); int[,] res=new int[c,r]; for(int i=0;i<r;i++) for(int j=0;j<c;j++) res[j,i]=a[i,j]; return res;}

    static int Determinant2x2(int[,] a){ if(a.GetLength(0)!=2||a.GetLength(1)!=2) return 0; return a[0,0]*a[1,1]-a[0,1]*a[1,0]; }

    static int Determinant3x3(int[,] a)
    {
        if (a.GetLength(0) != 3 || a.GetLength(1) != 3) return 0;
        int a11 = a[0,0], a12 = a[0,1], a13 = a[0,2];
        int a21 = a[1,0], a22 = a[1,1], a23 = a[1,2];
        int a31 = a[2,0], a32 = a[2,1], a33 = a[2,2];
        int det = a11*(a22*a33 - a23*a32) - a12*(a21*a33 - a23*a31) + a13*(a21*a32 - a22*a31);
        return det;
    }

    static double[,] Inverse2x2(int[,] a)
    {
        if (a.GetLength(0)!=2||a.GetLength(1)!=2) return null;
        double det = Determinant2x2(a);
        if (Math.Abs(det) < 1e-9) return null;
        double[,] inv = new double[2,2];
        inv[0,0] = a[1,1]/det; inv[0,1] = -a[0,1]/det; inv[1,0] = -a[1,0]/det; inv[1,1] = a[0,0]/det;
        return inv;
    }

    static double[,] Inverse3x3(int[,] a)
    {
        int det = Determinant3x3(a);
        if (Math.Abs(det) < 1e-9) return null;
        // compute cofactor matrix and transpose (adjugate)
        double[,] cof = new double[3,3];
        cof[0,0] =  (a[1,1]*a[2,2] - a[1,2]*a[2,1]);
        cof[0,1] = -(a[1,0]*a[2,2] - a[1,2]*a[2,0]);
        cof[0,2] =  (a[1,0]*a[2,1] - a[1,1]*a[2,0]);

        cof[1,0] = -(a[0,1]*a[2,2] - a[0,2]*a[2,1]);
        cof[1,1] =  (a[0,0]*a[2,2] - a[0,2]*a[2,0]);
        cof[1,2] = -(a[0,0]*a[2,1] - a[0,1]*a[2,0]);

        cof[2,0] =  (a[0,1]*a[1,2] - a[0,2]*a[1,1]);
        cof[2,1] = -(a[0,0]*a[1,2] - a[0,2]*a[1,0]);
        cof[2,2] =  (a[0,0]*a[1,1] - a[0,1]*a[1,0]);

        // adjugate = transpose of cofactor matrix
        double[,] adj = new double[3,3];
        for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++) adj[i,j] = cof[j,i];

        double[,] inv = new double[3,3];
        for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++) inv[i,j] = adj[i,j] / det;
        return inv;
    }

    static void DisplayInverse2x2(int[,] a){ var inv = Inverse2x2(a); if(inv==null){ Console.WriteLine("Singular matrix or not 2x2."); return;} for(int i=0;i<2;i++){ for(int j=0;j<2;j++) Console.Write($"{inv[i,j]:F4} "); Console.WriteLine(); }}
    static void DisplayInverse3x3(int[,] a){ var inv = Inverse3x3(a); if(inv==null){ Console.WriteLine("Singular matrix or not 3x3."); return;} for(int i=0;i<3;i++){ for(int j=0;j<3;j++) Console.Write($"{inv[i,j]:F4} "); Console.WriteLine(); }}

    static void Display(int[,] m){ for(int i=0;i<m.GetLength(0);i++){ for(int j=0;j<m.GetLength(1);j++) Console.Write(m[i,j]+" "); Console.WriteLine(); } }
}