// ---- C# I (Dor Ben Dor) ----
//        Daniel Fitzer
// ----------------------------


//Exercise A

namespace myNamespace
{

public enum OverflowBehavior
    {
        Clip,
        Overflow,
        Ellipsis,
    }

    public class Printer
    {
        public string text;
        public int maxLength;
        public Printer(string text, int maxLength)
        {
            this.text = text;
            this.maxLength = maxLength;
        }

        public void Print()
        {
            for (int i = 0; i < maxLength && i < text.Length; i++)
            {
                Console.Write(text[i]);
            }
        }
        public void Print(OverflowBehavior x)
        {
            switch (x)
            {
                case OverflowBehavior.Clip:

                    for (int i = 0; i < maxLength && i < text.Length; i++)
                    {
                        Console.Write(text[i]);
                    }
                    Console.WriteLine();
                    break;
                case OverflowBehavior.Ellipsis:

                    for (int i = 0; i < maxLength - 3; i++)
                    {
                        Console.Write(text[i]);
                    }
                    Console.Write(".");
                    Console.Write(".");
                    Console.Write(".");
                    Console.WriteLine();
                    break;
                case OverflowBehavior.Overflow:

                    Console.WriteLine(text);
                    break;
            }
        }
    }
}