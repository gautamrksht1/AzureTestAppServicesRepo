
namespace ProgramingPractices
{
    public class ArrayPrograms
    {
        public static int LargestPairSum(int[] A)
        {
            int firstMax = 0;
            int secondMax = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] > firstMax)
                {
                    secondMax = firstMax;
                    firstMax = A[i];
                }
                else if (A[i] > secondMax)
                {
                    secondMax = A[i];
                }
            }

            return (firstMax + secondMax);
        }

        // Find the Smallest positive number which is not present in the given array
        /// <summary>
        /// e.g A = [1,2, 7] , Output = 3
        /// A = [-1,-22, 10]; Output = 1
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static int FindMissingSmallestPositiveInteger(int[] A)
        {
            Array.Sort<int>(A);

            int lowest = A[0];
            int highest = A[A.Length - 1];
            if (lowest < 0 && highest < 0)
            {
                return 1;
            }

            for (int i = 1; i <= highest + 1; i++)
            {
                bool isExists = false;
                for (int j = 0; j < A.Length; j++)
                {
                    if (A[j] == i)
                    {
                        isExists = true;
                        break;
                    }
                }
                if (isExists == false)
                {
                    return i;
                }
            }

            return 0;
        }

        // Peak elements are those elements which greater than there neighbouring elements
        internal static int[] FindPeakElements(int[] A)
        {
            List<int> result = new List<int>();

            if (A[0] > A[1])
            {
                result.Add(A[0]);
            }
            if (A[A.Length - 1] > A[A.Length - 2])
            {
                result.Add(A[A.Length - 1]);
            }

            for (int i = 2; i < A.Length - 2; i++)
            {
                if (A[i] > A[i - 1] && A[i] > A[i + 1])
                {
                    result.Add(A[i]);
                }
            }

            return result.ToArray();
        }

        //Find the element before which all the elements are smaller than it, and after which all are greater
        //Given an array, find an element before which all elements are smaller than it, and after which all are greater than it. Return the index of the element if there is such an element, otherwise, return -1.
        //        Examples:

        //Input: arr[] = {5, 1, 4, 3, 6, 8, 10, 7, 9}; 
        //Output: 4 
        //Explanation: All elements on left of arr[4] are smaller than it
        //and all elements on right are greater.

        //Input: arr[] = {5, 1, 4, 4}; 
        //Output: -1 
        //Explanation : No such index exits.
        public static int FindRightOrderedElement(int[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                bool greaterThanAllPreviousAndSmallerThanAllPostItems = true;
                for (int prev = 0; prev < i; prev++)
                {
                    if (A[prev] > A[i])
                    {
                        greaterThanAllPreviousAndSmallerThanAllPostItems = false;
                        break;
                    }
                }
                if (!greaterThanAllPreviousAndSmallerThanAllPostItems)
                {
                    continue;
                }
                for (int post = i + 1; post < A.Length - 1; post++)
                {
                    if (A[i] > A[post])
                    {
                        greaterThanAllPreviousAndSmallerThanAllPostItems = false;
                        break;
                    }
                }
                if (!greaterThanAllPreviousAndSmallerThanAllPostItems)
                {
                    continue;
                }

                return A[i];
            }

            return -1;
        }

        // Find the highest occurring element in a given Array of integers
        // A = [1,2,3,1,4,5,1,2] should return 1
        public static int HighestOccurringNumber(int[] A)
        {
            Dictionary<int, int> keyValuePair = new Dictionary<int, int>();

            for (int i = 0; i < A.Length; i++)
            {
                if (!keyValuePair.ContainsKey(A[i]))
                {
                    keyValuePair.Add(A[i], 1);

                }
                else
                {
                    int value = keyValuePair[A[i]]; //.TryGetValue(A[i], out value);
                    keyValuePair[A[i]] = value + 1;
                }
            }

            int maxCount = 0;
            int key = 0;
            foreach (var item in keyValuePair)
            {
                if (item.Value > maxCount)
                {
                    maxCount = item.Value;
                    key = item.Key;
                }
            }

            return key;

        }

        // Given an array of N integers, and an integer K, the task is to find the number of pairs of integers in the array whose sum is equal to K.

        //Examples:  

        //Input: arr[] = {1, 5, 7, -1}, K = 6
        //Output:  2
        //Explanation: Pairs with sum 6 are(1, 5) and(7, -1).

        //Input: arr[] = {1, 5, 7, -1, 5}, K = 6
        //Output:  3
        //Explanation: Pairs with sum 6 are(1, 5), (7, -1) & (1, 5).    

        public static int CountPairsWithGivenSum(int[] A, int K)
        {
            int pairCount = 0;
            for (int i = 0; i < A.Length - 1; i++)
            {
                for (int j = i + 1; j < A.Length; j++)
                {
                    if (A[i] + A[j] == K)
                    {
                        pairCount = pairCount + 1;
                    }

                }
            }

            return pairCount;
        }

        // Find Common elements in two arrays

        public static int[] FindCommonElementsInTwoArrays(int[] A, int[] B)
        {
            int[] tempA;
            int[] tempB;
            HashSet<int> result = new HashSet<int>();
            if (A.Length < B.Length)
            {
                tempA = A;
                tempB = B;
            }
            else
            {

                tempA = B;
                tempB = A;
            }

            for (int i = 0; i < tempA.Length; i++)
            {
                for (int j = 0; j < tempB.Length; j++)
                {
                    if (tempA[i] == tempB[j])
                    {
                        result.Add(tempA[i]);
                    }
                }

            }

            return result.ToArray();
        }

        // Find the sub array which sums to a given number

        // Given an array of positive and negative numbers, the task is to find if there is a subarray(of size at least one) with K sum.

        //Examples: 

        //Input: {4, 2, -3, 1, 6} , K =0
        //Output: true 
        //Explanation:
        //There is a subarray with zero sum from index 1 to 3.

        //Input: {4, 2, 0, 1, 6}, k = 6
        //Output: true
        //Explanation: [4,2] or [6].

        public static int[] FindFirstSubArrayWhichSumsToGivenInteger(int[] A, int K)
        {
            System.Collections.Generic.List<int> subArray = new List<int>();
            for (int i = 0; i < A.Length; i++)
            {
                int sum = 0;
                subArray.Add(A[i]);
                sum += A[i];
                if (sum == K)
                {
                    return subArray.ToArray();
                }
                for (int j = i + 1; j < A.Length; j++)
                {
                    subArray.Add(A[j]);
                    sum += A[j];
                    if (sum == K)
                    {
                        return subArray.ToArray();
                    }
                }
                subArray.Clear();
            }

            return subArray.ToArray();
        }

        // Given an array arr[] of size n and an integer X.Find if there’s a triplet in the array which sums up to the given integer X.

        //Examples: 

        //Input: array = { 12, 3, 4, 1, 6, 9}, sum = 24; 
        //Output: 12, 3, 9 
        //Explanation: There is a triplet(12, 3 and 9) present 
        //in the array whose sum is 24. 

        //Input: array = {1, 2, 3, 4, 5}, sum = 9 
        //Output: 5, 3, 1 
        //Explanation: There is a triplet(5, 3 and 1) present 
        //in the array whose sum is 9.

        public static int[] TripletSumInArray(int[] A, int X)
        { 
            List<int> result = new List<int>();
            for(int i = 0;i < A.Length-2;i++)
            {
                for (int j = i + 1; j < A.Length-1; j++)
                {
                    for (int k = j + 1; k < A.Length; k++)
                    {
                        if (A[i] + A[j] + A[k] == X)
                        {
                            Console.WriteLine("Triplet is " + A[i] + ", " + A[j] + ", " + A[k]);
                            result.AddRange([A[i], A[j], A[k]]);
                            return result.ToArray();
                        }
                    } 
                }
            }

            return result.ToArray();

        }

        public static int FindNthSmallest(int[] A, int n)
        {
            Array.Sort(A);

            return A[n - 1];
        }

        public static int FindNthLargest(int[] A, int n)
        {
            Array.Reverse(A);

            return A[n - 1];
        }
    }
}
