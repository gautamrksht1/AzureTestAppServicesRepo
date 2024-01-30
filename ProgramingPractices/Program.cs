// See https://aka.ms/new-console-template for more information
using ProgramingPractices;

public class MainApp
{
    public static void Main(string[] args)
    {
        int[] A = {  2, 8 , -22 };
        int result = ArrayPrograms.FindMissingSmallestPositiveInteger(A);

        Console.WriteLine("FindMissingSmallestPositiveInteger - {0} ", result);

        A = [100, 10, 20, 15, 2, 23, 90, 67, 200];
        int[] peakElements = ArrayPrograms.FindPeakElements(A);
        Console.WriteLine("FindPeakElements - {0}", string.Join(",", peakElements));

        A = [1, 2, 9, 4, 5, 6, 1, 10];
        int largestPairSum = ArrayPrograms.LargestPairSum(A);
        Console.WriteLine("Find Largest Pair Sum - {0}", largestPairSum);

        A =  [ 5, 1, 4, 3, 6, 8, 10, 7, 9];
        int index = ArrayPrograms.FindRightOrderedElement(A);
        Console.WriteLine("Find the element before which all the elements are smaller than it, and after which all are greater - {0}", index);

        A = [1, 2, 3, 1, 4, 2, 1, 2, 2];
        int maxOccurring = ArrayPrograms.HighestOccurringNumber(A);
        Console.WriteLine("Highest occurring number- {0}", maxOccurring);

        A = [1, 5, 7, -1, 3,3];
        int pairCount = ArrayPrograms.CountPairsWithGivenSum(A, 6);
        Console.WriteLine("Count pairs with given sum- {0}", pairCount);


        A = [1, 2, 3, 4, 5, 6];
        int[] B = { 1, 2, -1, 6};
        int[] commonElements = ArrayPrograms.FindCommonElementsInTwoArrays(A,B);
        Console.WriteLine("FindCommonElementsInTwoArrays - {0}", string.Join(",", commonElements));

        A = [4, 5, 0, 1, 6];
        int[] subArray = ArrayPrograms.FindFirstSubArrayWhichSumsToGivenInteger(A, 6);
        Console.WriteLine("FindFirstSubArrayWhichSumsToGivenInteger - {0}", string.Join(",", subArray));


        A = [12, 3, 4, 1, 6, 9];
        int[] triplet = ArrayPrograms.TripletSumInArray(A, 24);
        Console.WriteLine("TripletSumInArray - {0}", string.Join(",", triplet));
    }
}
