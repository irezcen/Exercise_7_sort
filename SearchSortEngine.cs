using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace B8_B9
{
    class SearchSortEngine
    {
        private List<int> numbers;
        private int size;

        public SearchSortEngine(int n)
        {
            //constructor
            if (n > 0) size = n;
            else
            {
                Console.WriteLine("List size cannot be negative - defaulting to 10 instead");
                size = 10;
            }
            numbers = new List<int>(size);
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                numbers.Add(rnd.Next(10000));
            }
        }

        // utility methods
        public void Reshuffle()
        {
            // shuffle list contents 
            // Fisher-Yates shuffle algorithm
            Random rnd = new Random();
            int j = size;
            while (j > 1)
            {
                int i = rnd.Next(j);
                j--;
                int tmp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = tmp;
            }
        }
        public void SaveList(string filename)
        {
            // save list to "filename.txt"
            // uwaga dla Panstwa - pliki domyslnie zapisuja sie w folderze aplikacji
            // typowo jest to katalog roboczy -> bin -> Debug (-> netcoreappX.Y jesli taki katalog wystepuje)
            try
            {
                List<String> listStr = numbers.ConvertAll<string>(i => i.ToString()); // convert list<int> to list<string>
                File.WriteAllLines(filename + ".txt", listStr.ToArray()); // write the list 
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be opened or a writing error has occurred:");
                Console.WriteLine(e.Message);
            }
        }
        // insert your sorting and searching methods here
        public void SelectionSort()
        {
            List<int> sorted = new List<int>();
            int a = numbers[0];
            while(numbers.Count > 0)
            {
                a = numbers[0];
                for (int i = 1; i < numbers.Count; i++)
                {
                    if (a > numbers[i])
                    {
                        a = numbers[i];
                    }
                }
                numbers.Remove(a);
                sorted.Add(a);
            }
            numbers = sorted;
        }
        public void InsertionSort()
        {
            List<int> sorted = new List<int>();
            sorted.Add(numbers[0]);
            numbers.RemoveAt(0);
            while(numbers.Count > 0)
            {
                for (int i = 0; i < sorted.Count; i++)
                {
                    if (numbers[0] <= sorted[i])
                    {
                        sorted.Insert(i, numbers[0]);
                        numbers.RemoveAt(0);
                        break;
                    }
                }
                if (numbers.Count > 0 && numbers[0] > sorted[sorted.Count - 1])
                {
                    sorted.Add(numbers[0]);
                    numbers.RemoveAt(0);
                }
            }
            numbers = sorted;
        }
        public void BubbleSort()
        {
            int a;
            bool b = true;
            while (b)
            {
                b = false;
                for (int i = 0; i < numbers.Count - 1; i++)
                {
                    a = numbers[i];
                    if (numbers[i] > numbers[i + 1])
                    {
                        a = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = a;
                        b = true;
                    }
                }
            }           
        }
        public bool LinearSearch(int a)
        {
            for(int i = 0; i < numbers.Count; i++)
            {
                if(numbers[i] == a)
                {
                    return true;
                }
            }
            return false;
        }
        public bool JumpSearch(int a)
        {
            int m = Convert.ToInt32(Math.Round(Math.Sqrt(numbers.Count)));
            for(int i = 0; i <= numbers.Count/m; i++)
            {
                if(a > numbers[i * m] && a < numbers[(i+1)*m])
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (numbers[i*m + j] == a)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if(a >= numbers[i * m])
                    {
                        for (int j = 0; j < numbers.Count - i*m; j++)
                        {
                            if (numbers[i * m + j] == a)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if(a <= numbers[m])
                        {
                            for (int j = 0; j <= m; j++)
                            {
                                if (numbers[j] == a)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool BinarySearch(int a)
        {
            double floor = 0;
            double roof = numbers.Count;
            int marker = numbers.Count/2;
            do
            {
                if (a == numbers[marker])
                {
                    return true;
                }
                else
                {
                    if (a < numbers[marker])
                    {
                        roof = marker - 1;
                    }
                    else
                    {
                        floor = marker + 1;
                    }
                }
                marker = Convert.ToInt32(Math.Round((floor + roof) / 2));
            }while(roof - floor > -1);
            return false;
        }

        private List<int> DevideMergeSort(List<int> unsorted)
        {
            if(unsorted.Count == 1)
            {
                return unsorted;
            }
            List<int> down = new List<int>();
            List<int> up = new List<int>();
            List<int> sorted = new List<int>();
            int marker = unsorted.Count/ 2;
            for(int i = 0; i < marker; i++)
            {
                down.Add(unsorted[i]);
            }
            for(int i = marker; i < unsorted.Count; i++)
            {
                up.Add(unsorted[i]);
            }
            down = DevideMergeSort(down);
            up = DevideMergeSort(up);
            while (down.Count > 0 || up.Count > 0)
            {
                if (down.Count > 0 && up.Count > 0)
                {
                    if (down[0] > up[0])
                    {
                        sorted.Add(up[0]);
                        up.RemoveAt(0);
                    }
                    else
                    {
                        sorted.Add(down[0]);
                        down.RemoveAt(0);
                    }
                }
                else
                {
                    if (down.Count > 0)
                    {
                        for (int i = 0; i < down.Count; i++)
                        {
                            sorted.Add(down[i]);
                        }
                        down.Clear();
                    }
                    if (up.Count > 0)
                    {
                        for (int i = 0; i < up.Count; i++)
                        {
                            sorted.Add(up[i]);
                        }
                        up.Clear();
                    }
                }
            }
            return sorted;
        }
        public void MergeSort()
        {
            numbers = DevideMergeSort(numbers);
        }
    }
}
