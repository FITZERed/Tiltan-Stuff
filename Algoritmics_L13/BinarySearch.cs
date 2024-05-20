public class BinarySearcher
{
    public int BinarySearch(int[] list, int valueToFind)
    {
        int currentMidPosition = list.Length / 2;
        int searchRangeMin = 0;
        int searchRangeMax = list.Length - 1;
        while (searchRangeMin <= searchRangeMax)
        {
                currentMidPosition = (searchRangeMin + searchRangeMax) / 2;
            if (list[currentMidPosition] == valueToFind) return currentMidPosition;
            if (list[currentMidPosition] < valueToFind)
            {
                searchRangeMin = currentMidPosition + 1;
            }
            else if (list[currentMidPosition] > valueToFind)
            {
                searchRangeMax = currentMidPosition - 1;
            }
        }
        return -1;
    }
}