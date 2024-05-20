class Program
{
    public static void Main(String[] args)
    {
        int[] sortedList = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        int[] unsortedList = new int[10] { 6, 4, 8, 2, 9, 5, 7, 96, 4, 1 };
        BinarySearcher searcher = new BinarySearcher();
        //Console.WriteLine(searcher.BinarySearch(sortedList, 10));
        BubbleSorter sorter = new BubbleSorter();
        unsortedList = sorter.BubbleSort(unsortedList);
        for (int i = 0; i < unsortedList.Length; i++)
        {
            Console.WriteLine(unsortedList[i]);
        }
    }
}