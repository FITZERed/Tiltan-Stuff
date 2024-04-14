public class StringList
{

    private int Capacity;
    private int Count;
    private string[] list;

    public StringList()
    {
        Capacity = 4;
        Count = 0;
        list = new string[Capacity];
    }
    public StringList(int capacity)
    {
        Capacity = capacity;
        Count = 0;
        list = new string[Capacity];
    }
    public void Add(string str)
    {
        if (Count >= Capacity)
        {
            Capacity *= 2;
            var oldList = list;
            list = new string[Capacity];
            for (int i = 0; i < oldList.Length; i++)
            {
                list[i] = oldList[i];
            }
        }

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == null)
            {
                list[i] = str;
                Count++;
                break;
            }
        }
    }

    public void Remove(string str)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == str)
            {
                list[i] = null;
                Count--;
                return;
            }
        }
    }

    public void RemoveAt(int index)
    {
        list[index] = null;
        Count--;
    }

    public void Clear()
    {
        Capacity = 4;
        Count = 0;
        list = new string[Capacity];
    }

    public void Print()
    {
        foreach (var item in list)
        {
            if (item == null) continue;
            Console.WriteLine(item);
        }
    }


}