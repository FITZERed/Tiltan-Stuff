/*Take list
take 1st index num
check if smaller than next
     if yes, move to next
     if not, swap
repeat when done
*/

public class BubbleSorter
{
    public int[] BubbleSort(int[] list)
    {
        for (int i = 0; i < list.Length; i++) 
        {
            for (int j = 0; j < list.Length - 1; j++)
            {
                if (list[j] <= list[j + 1])
                {
                    continue;
                }
                else
                {
                    int keep = list[j + 1];
                    list[j + 1] = list[j];
                    list[j] = keep;
                }
            } 
        }
        return list;
    }
}