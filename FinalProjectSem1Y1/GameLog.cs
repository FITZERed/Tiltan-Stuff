public class GameLog
{
    public void PrintLog()
    {
        string[] logArray = EventLog.ToArray();
        Console.SetCursorPosition(23, 0);
        Console.Write(logArray[0]);
        Console.SetCursorPosition(23, 1);
        Console.Write(logArray[1]);
        Console.SetCursorPosition(23, 2);
        Console.Write(logArray[2]);
        Console.SetCursorPosition(23, 3);
        Console.Write(logArray[3]);
        Console.SetCursorPosition(23, 4);
        Console.Write(logArray[4]);
    }
    private Queue<string> EventLog = new Queue<string>();

    private int EventLogSize = 5;

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