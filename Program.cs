using ManySorts.Sorts;

namespace HighPing
{
    class ManySorts
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ManySorts");
            Console.WriteLine("A program with a lot of sorting algorithms. Using C#. (Most of sorting only support INT!!!)");
            Console.WriteLine("It has been open-sourced on Github under the MIT license.\n");

            Console.WriteLine("1. Bubble Sort");
            Console.WriteLine("2. Selection Sort");
            Console.WriteLine("3. Insertion Sort");
            Console.WriteLine("4. Shell Sort");
            Console.WriteLine("5. Merge Sort");
            Console.WriteLine("6. Quick Sort");
            Console.WriteLine("7. Heap Sort");
            Console.WriteLine("8. Counting Sort");
            Console.WriteLine("9. Bucket Sort");
            Console.WriteLine("10. Radix Sort");
            Console.WriteLine("11. Sleep Sort");
            Console.WriteLine("12. Spaghetti Sort");
            Console.WriteLine("13. Bogo Sort");
            Console.WriteLine("14. Bogobogo Sort");
            Console.WriteLine("15. Idiot Sort");
            Console.WriteLine("16. Miracle Sort");
            Console.Write("\n Enter the number to use the corresponding function: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BubbleSort.Run();
                    return;
                case "2":
                    SelectionSort.Run();
                    return;
                case "3":
                    InsertionSort.Run();
                    return;
                case "4":
                    ShellSort.Run();
                    return;
                case "5":
                    MergeSort.Run();
                    return;
                case "6":
                    QuickSort.Run();
                    return;
                case "7":
                    HeapSort.Run();
                    return;
                case "8":
                    CountingSort.Run();
                    return;
                case "9":
                    BucketSort.Run();
                    return;
                case "10":
                    RadixSort.Run();
                    return;
                case "11":
                    SleepSort.Run();
                    return;
                case "12":
                    SpaghettiSort.Run();
                    return;
                case "13":
                    BogoSort.Run();
                    return;
                case "14":
                    BogobogoSort.Run();
                    return;
                case "15":
                    IdiotSort.Run();
                    return;
                case "16":
                    MiracleSort.Run();
                    return;
            }
        }
    }
}