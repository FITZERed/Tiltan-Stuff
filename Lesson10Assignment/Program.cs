class Program
{
    public static void Main(string[] args)
    {
        StringList stringList = new StringList();
        stringList.Add("item1");
        stringList.Add("item2");
        stringList.Add("item3");
        stringList.Add("item4");
        stringList.Add("item5");
        stringList.Add("item6");
        stringList.Remove("item4");
        stringList.RemoveAt(1);
        stringList.Print();
        stringList.Clear();
        stringList.Print();
    }
}