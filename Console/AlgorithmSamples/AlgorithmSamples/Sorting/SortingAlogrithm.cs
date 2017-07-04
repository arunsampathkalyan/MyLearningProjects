using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmSamples.Sorting
{



    public static class SortingAlogrithm
    {
        #region QuickSort

        /*http://quiz.geeksforgeeks.org/quick-sort/
         * http://www.codingeek.com/algorithms/quick-sort-algorithm-explanation-implementation-and-complexity/
         * QuickSort
        Like Merge Sort, QuickSort is a Divide and Conquer algorithm. It picks an element as pivot and partitions the given array around the picked pivot. There are many different versions of quickSort that pick pivot in different ways.
        Always pick first element as pivot.
        Always pick last element as pivot (implemented below)
        Pick a random element as pivot.
        Pick median as pivot.

            PSEUDOCODE OF QUICK SORT


        QUICK-SORT(A,start,end)
        if start < end
        pIndex = PARTITION(A,start,end)
        QUICK-SORT(A,start,pIndex-1)
        QUICK-SORT(A,pIndex+1,end)
        end func
        /*To sort an entire array A,
        the initial call is QUICK-SORT(A,0,A.length-1)*/
        /*
        //partitioning the array with respect to pivot.
        PARTITION(A, start, end)
            pivot = A[end]
            pIndex = start
                for i = start to end-1
                   if A[i] < pivot
                       swap A[i] with A[pIndex]
                        pIndex = pIndex + 1
            swap A[pIndex] with A[end]
           return pIndex
        end func

        */
        public static int PartitionPivotAsFirstItem(int[] array, int left, int right)
        {
            int pivot = array[left];
            while (true)
            {
                while (array[left] < pivot)
                    left++;
                while (array[right] > pivot)
                    right--;

                if (left < right)
                {
                    int temp = array[right];
                    array[right] = array[left];
                    array[left] = temp;
                }
                else
                    return right;
            }
        }

        public static void QuickSort_Recursive(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = PartitionPivotAsFirstItem(array, left, right);

                //if (pivot > 1)
                    QuickSort_Recursive(array, left, pivot - 1);
                //if (pivot + 1 < right)
                    QuickSort_Recursive(array, pivot + 1, right);
            }
        }


        public static int PartitionPivotAsLastItem(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = (low - 1); // index of smaller element
            for (int j = low; j <= high - 1; j++)
            {
                // If current element is smaller than or
                // equal to pivot
                if (arr[j] <= pivot)
                {
                    i++;

                    // swap arr[i] and arr[j]
                    int tempVar = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tempVar;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot)
            int temp = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp;

            return i + 1;
        }


        /* The main function that implements QuickSort()
          arr[] --> Array to be sorted,
          low  --> Starting index,
          high  --> Ending index */
        public static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                /* pi is partitioning index, arr[pi] is 
                  now at right place */
                int pi = PartitionPivotAsLastItem(arr, low, high);

                // Recursively sort elements before
                // partition and after partition
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        #endregion
    }
}
