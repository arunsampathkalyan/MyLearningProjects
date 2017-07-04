using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmSamples.Sorting;
using AlgorithmSamples.CustomClass;
using System.IO;

namespace AlgorithmSamples
{
    class Program
    {
        static void Main(string[] args)
        {


           

            //Linear Search
            int[] numbers1 = { 45, 65, 2, 58, 1, 24, 13, 89, 54, 72, 56, 51, 85, 88, 12, 3, 99, 154, 114, 120, 28 };

            Console.WriteLine("3 at the index {0} ", Scenarios.Scenarios.LinearSearchItemInArray<int>(numbers1, 3));

            //Binary Search in sorted array
            var num1arr = numbers1.ToList();
            num1arr.Sort();

            Console.WriteLine("72 at the index in Binary Search {0} ", Scenarios.Scenarios.BinarySearchItemInArray<int>(num1arr.ToArray(), 72));

            //Custom Collection

            Voters votersList = new Voters();
            votersList.Add(new Voter { Name = "Arun", Age = 28 });
            votersList.Add(new Voter { Name = "KUmar", Age = 29 });
            votersList.Add(new Voter { Name = "SK", Age = 28 });
            votersList.Add(new Voter { Name = "Kesavan", Age = 32 });
            votersList.Add(new Voter { Name = "Raj", Age = 35 });

            votersList.RemoveAt(1);

            votersList.Remove(new Voter() { Name = "Raj" });


            //Number Divisable by 11
            Console.WriteLine("{0} is divisible by {1} : {2}", "76945", 11, Scenarios.Scenarios.NumDivsibleByParam("76945", 11));

            Console.WriteLine("{0} is divisible by {1} : {2}", "1234567589333892", 11, Scenarios.Scenarios.NumDivsibleByParam("1234567589333892", 11));

            Console.WriteLine("{0} is divisible by {1} : {2}", "363588395960667043875487", 11, Scenarios.Scenarios.NumDivsibleByParam("363588395960667043875487", 11));

            int[] numbers = { 45, 65, 2, 58, 1, 24, 13, 89, 54, 72, 56, 51, 85, 88, 12, 3, 99, 154, 114, 120, 28 };
            PerformQuickSort(numbers);
            //Linked list implementaion
            LList lList = new CustomClass.LList();
            lList.Add("A");
            Console.WriteLine("Count {0}", lList.Count);
            Console.WriteLine("Delete at 1", lList.Delete(1));
            Console.WriteLine("Delete at 1", lList.Delete(1));
            Console.WriteLine("Count {0}", lList.Count);


            lList.Add("B");


            lList.Add("C");
            Console.WriteLine("Delete at 1", lList.Delete(1));
            lList.Add("D");
            lList.Add("E");
            lList.Add("F");
            lList.Add("G");
            lList.Add("H");
            lList.Add("I");
            lList.Add("J");

            lList.ListNodes();
            Console.WriteLine("Count {0}", lList.Count);

            Console.WriteLine("Node at 5 : {0}", lList.RetrieveNode(5).content);

            Console.WriteLine("Node at 15 : {0}", lList.RetrieveNode(15));

            Console.WriteLine("Delete at 5", lList.Delete(5));

            Console.Read();
        }

        static void PerformQuickSort(int[] array)
        {
            //this method not working for duplicate elements
            SortingAlogrithm.QuickSort_Recursive(array, 0, array.Length - 1);
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            //int[] array1 = { 45, 65, 2, 58, 1, 24, 13, 89, 54, 3, 1, 72, 56, 51, 4, 4, 3, 85, 88, 12, 3, 99, 154, 114, 120, 28 };
            //SortingAlogrithm.QuickSort_Recursive(array1, 0, array1.Length - 1);

            int[] numbers = { 45, 65, 2, 58, 1, 24, 13, 89, 54, 3, 1, 72, 56, 51, 4, 4, 3, 85, 88, 12, 3, 99, 154, 114, 120, 28 };

            //this method is working for duplicate elements
            SortingAlogrithm.QuickSort(numbers, 0, numbers.Length - 1);

            int[] numbers2 = { 45, 65, 2, 58, 1, 24, 13, 89, 54, 3, 72, 56, 51, 4, 85, 88, 12, 99, 154, 114, 120, 28 };
            SortingAlogrithm.QuickSort(numbers2, 0, numbers2.Length - 1);
            foreach (var item in numbers2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
