using System;
using System.Diagnostics;
/* uwaga: jesli zdarzy sie, ze system nie bedzie chcial Panstwu wykrywac biblioteki System.Diagnostics, prosze sprobowac nastepujacego rozwiazania:
 * w solution explorerze (menu po prawej stronie) kliknąć prawym przyciskiem myszy na References -> Add Reference -> wyszukać "System" 
 * -> zaznaczyć (ważne!) ptaszkiem z lewej strony i kliknąć ok
 * */

namespace B8_B9
{
    class Program
    {
        static void Main(string[] args)
        {

            // ****************************** B8 ******************************

            SearchSortEngine testList = new SearchSortEngine(1000);
            testList.SaveList("unsorted");
            // selection sort
            testList.SelectionSort();
            testList.SaveList("selection");
            // insertion sort
            testList.Reshuffle();
            testList.InsertionSort();
            testList.SaveList("insertion");
            // bubble sort 
            testList.Reshuffle();
            testList.BubbleSort();
            testList.SaveList("bubble");
            // linear search
            testList.Reshuffle();
            Console.WriteLine("Searching for numbers... [linear search]");
            for (int i = 10; i < 20; i++)
            {
                Console.WriteLine(i + " - " + testList.LinearSearch(i));
            }
            // jump search
            testList.SelectionSort(); // we need to sort the list first
            Console.WriteLine("Searching for numbers... [jump search]");
            for (int i = 10; i < 20; i++)
            {
                Console.WriteLine(i + " - " + testList.JumpSearch(i));
            }


            // ****************************** B9 ******************************
            // binary search
            testList.SelectionSort(); // we need to sort the list first
            Console.WriteLine("Searching for numbers... [binary search]");
            for (int i = 10; i < 20; i++)
            {
                Console.WriteLine(i + " - " + testList.BinarySearch(i));
            }
            // merge sort
            testList.Reshuffle();
            testList.SaveList("unsorted");
            testList.MergeSort();
            testList.SaveList("merge");


            // ************************* performance **************************
            Console.WriteLine("\nPerformance tests");
            SearchSortEngine timeTest = new SearchSortEngine(10000); // we want slightly longer list to get more reliable results 
            Stopwatch stopwatch; // this is the time measurement device we will use
            double measurement; // here results will be saved
            // selection sort
            stopwatch = Stopwatch.StartNew(); // start the measurement
            timeTest.SelectionSort(); // do your job
            stopwatch.Stop(); // stop the measurement
            measurement = Convert.ToDouble(stopwatch.ElapsedTicks) / Stopwatch.Frequency; // read the measurements value (in seconds)
            Console.WriteLine("Execution time for selection sort: {0:0.00} miliseconds", 1000 * measurement); // for convenience, we scale by 1000 and give value in ms
            // insertion sort
            timeTest.Reshuffle(); // shuffling is not included in the measurement, since we want to compare sorting algorithms only
            stopwatch = Stopwatch.StartNew();
            timeTest.InsertionSort();
            stopwatch.Stop();
            measurement = Convert.ToDouble(stopwatch.ElapsedTicks) / Stopwatch.Frequency;
            Console.WriteLine("Execution time for insertion sort: {0:0.00} miliseconds", 1000 * measurement);
            // bubble sort
            timeTest.Reshuffle();
            stopwatch = Stopwatch.StartNew();
            timeTest.BubbleSort();
            stopwatch.Stop();
            measurement = Convert.ToDouble(stopwatch.ElapsedTicks) / Stopwatch.Frequency;
            Console.WriteLine("Execution time for bubble sort: {0:0.00} miliseconds", 1000 * measurement);
            // merge sort
            timeTest.Reshuffle();
            stopwatch = Stopwatch.StartNew();
            timeTest.MergeSort();
            stopwatch.Stop();
            measurement = Convert.ToDouble(stopwatch.ElapsedTicks) / Stopwatch.Frequency;
            Console.WriteLine("Execution time for merge sort: {0:0.00} miliseconds", 1000 * measurement);

        }
    }
}
