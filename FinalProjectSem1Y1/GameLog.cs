public class GameLog
{
    public void PrintLog()
    {
        ClearLog();

        int line = 0;
        foreach (string log in EventLog)
        {
            Console.SetCursorPosition(25, line);
            Console.Write(log);
            line++;
        }
    }
    public void ClearLog()
    {
        int line = 0;
        foreach (var item in EventLog)
        {
            Console.SetCursorPosition(25, line);
            Console.Write("                                                                          ");
            line++;
        }
    }

    private Queue<string> EventLog = new Queue<string>();

    private int EventLogSize = 10;
    public void LogEvent(string eventToLog)
    {
        AddLog(eventToLog);
        PrintLog();
    }
    
    public void InitEventLog()
    {
        EventLog = new Queue<string>();
        for(int i = 0; i < EventLogSize; i++) { EventLog.Enqueue(""); }
    }

    public void AddLog(string eventToLog)
    {
        EventLog.Enqueue(eventToLog);
        if (EventLog.Count > EventLogSize) EventLog.Dequeue();
    }
}