using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmSamples.Scenarios
{
    public class Scenarios
    {
        /*
        //Check if a large number is divisible by 11 or not
        http://www.geeksforgeeks.org/check-large-number-divisible-11-not/

            Input : n = 76945
            Output : Yes

            Input  : n = 1234567589333892
            Output : Yes

            Input  : n = 363588395960667043875487
            Output : No

        A number is divisible by 11 if difference of following two is divisible by 11.

        Sum of digits at odd places.
        Sum of digits at even places.

        For example, let us consider 76945 
        Sum of digits at odd places  : 7 + 9 + 5
        Sum of digits at even places : 6 + 4 
        Difference of two sums = 21 - 10 = 11
        Since difference is divisible by 11, the
        number 7945 is divisible by 11.

        */

        public static bool NumDivsibleByParam(string num, int divisor)
        {
            switch (divisor)
            {
                case 11:
                    {
                        int n = num.Length;
                        // Compute sum of even and odd digit
                        // sums
                        int oddDigitSum = 0, evenDigitSum = 0, sum = 0;
                        for (int i = 0; i < n; i++)
                        {
                            // When i is even, position of digit is odd
                            //if (i % 2 == 0)
                            //    oddDigitSum += num[i]-'0';
                            //else
                            //    evenDigitSum += num[i] - '0';


                            //Form the alternating sum of the digits.The result must be divisible by 11
                            //918,082: 9 − 1 + 8 − 0 + 8 − 2 = 22 = 2 × 11.
                            if (i % 2 == 0)
                                sum += num[i] - '0';
                            else
                                sum -= num[i] - '0';
                        }

                        return ((sum) % 11 == 0);
                        // Check its difference is divisble by 11 or not
                        //return ((oddDigitSum - evenDigitSum) % 11 == 0);

                    }
                case 7:
                    {

                        return false;
                    }
                default:
                    return false;
            }
        }

        public static int LinearSearchItemInArray<T>(T[] arr, T item)
        {
            /*
             * Algorithm
             * sequential search is made over all items one by one. 
             * Every item is checked and if a match is found then that particular item is returned, otherwise the search continues till the end of the data collection.
             * 
             *  A ←  array   
             *  n ← size of array   
             *  x ← value to be searched

             * Linear Search ( Array A, Value x)
             * 
             * 1. set i to 0
             * 2. if i > n goto step 7
             * 3. check A[i] = x then goto step 6
             * 4. set i=i+1;
             * 5. goto step 3
             * 6. Return i 
             * 7. retrun -1
             */
            var pos = SearchUsingRecursive<T>(arr, arr.Length - 1, 89);
            var posForloop = SearchUsingForloop<T>(arr, 89);

            int i = 0;
            while (i < arr.Length)
            {
                if (arr[i].ToString() == item.ToString())
                    return i;
                i += 1;
            }
            return -1;
        }

        static int SearchUsingRecursive<T>(T[] arr, int size, object item)
        {
            int location;
            if (arr[size].ToString() == item.ToString())
                return size;
            else if (size == -1)
                return -1;
            else
                return (location = SearchUsingRecursive<T>(arr, size - 1, item));
        }

        static int SearchUsingForloop<T>(T[] arr, object item)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToString() == item.ToString())
                    return i;
            }
            return -1;
        }

        public static int BinarySearchItemInArray<T>(T[] arr, T Item)
        {
            /*
             * Algorithm
             * Binary search is a fast search algorithm with run-time complexity of Ο(log n). 
             * This search algorithm works on the principle of divide and conquer. 
             * For this algorithm to work properly, the data collection should be in the sorted form.
             * 
             * Binary search looks for a particular item by comparing the middle most item of the collection. 
             * If a match occurs, then the index of item is returned. 
             * If the middle item is greater than the item, then the item is searched in the sub-array to the left of the middle item. 
             * Otherwise, the item is searched for in the sub-array to the right of the middle item. 
             * This process continues on the sub-array as well until the size of the subarray reduces to zero.
             * 
             *  A ← sorted array   
             *  n ← size of array   
             *  x ← value to be searched
             *  
             *  
             *  Set lowerBound=1
             *  Set upperBound=n
             *  
             * 
             * if upperbound < lowerfound return not found
             * midPoint = (lowerBound + upperBound) / 2;
             * 
             * when x is not found - (lowerBound < upperBound)
             * 
             * if(A[midpoint] < x)
             * set lowerbound=midpoint+1;
             * f(A[midpoint]=x) then return midpoint;
             * else
             * set upperbound=midpoint-1;
             * midPoint = (lowerBound + upperBound) / 2;
             * i
             */

            int midPoint, lowerBound = 1, upperBound = arr.Length;

            if (upperBound < lowerBound) return -1;

            midPoint = (lowerBound + upperBound) / 2;

            while (lowerBound < upperBound)
            {

                if (Convert.ToInt32(arr[midPoint]) < Convert.ToInt32(Item.ToString()))
                    lowerBound = midPoint + 1;
                else if (Convert.ToInt32(arr[midPoint]) == Convert.ToInt32(Item.ToString()))
                    return midPoint;
                else
                    upperBound = midPoint - 1;

                midPoint = (lowerBound + upperBound) / 2;
            }
            return -1;
        }
    }
}
