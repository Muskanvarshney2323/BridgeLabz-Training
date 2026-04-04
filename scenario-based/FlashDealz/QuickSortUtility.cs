using System;
class QuickSortUtility : ISorter
{
    public void Sort(Product[] products)
    {
        QuickSort(products, 0, products.Length - 1);
    }
    private void QuickSort(Product[] products, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(products, low, high);
            QuickSort(products, low, pi - 1);
            QuickSort(products, pi + 1, high);
        }
    }
    private int Partition(Product[] products, int low, int high)
    {
        double pivot = products[high].Discount;
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (products[j].Discount > pivot)
            {
                i++;
                Swap(products, i, j);
            }
        }
        Swap(products, i + 1, high);
        return i + 1;
    }
    private void Swap(Product[] products, int i, int j)
    {
        Product temp = products[i];
        products[i] = products[j];
        products[j] = temp;
    }
}