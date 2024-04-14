class Proccessor
{
    public void ListCreator()
    {

    }
    public int AverageFinder(List<int> nums)
    {
        int sum = 0;
        for (int i = 0; i < nums.Count; i++)
        {
            sum+= nums[i];
        }
        int avg = sum / nums.Count;
        return avg;
    }

    public void MedianFinder(List<int> nums)
    {
        nums.Sort();//...
    }

    public int SumFinder(List<int> nums)
    {
        int sum = 0;
        for (int i = 0; i < nums.Count; i++)
        {
            sum += nums[i];
        }
        return sum;
    }

    public enum Function
    {
        Average,
        Median,
        Sum
    }
}