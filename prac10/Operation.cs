using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata.Ecma335;

namespace prac9;

public class Operation
{
    public string Trigger;
    public Func<string, string> Fn;
    public static List<Operation> List = new();

    public Operation(string trigger, Func<string, string> fn)
    {
        Trigger = trigger;
        Fn = fn;
        List.Add(this);
    }

    public static void CreateOperations()
    {

        string ToLower(string str)
        {
            return str.ToLower();
        }

        string ToUpper(string str)
        {
            return str.ToUpper();
        }

        string Replace(string var)
        {
            Console.WriteLine("Replace > ");
            string oldValue = Console.ReadLine();
            Console.WriteLine("With > ");
            string newValue = Console.ReadLine();
            return var.Replace(oldValue, newValue);
        }

        new Operation("tolower", ToLower);
        new Operation("toupper", ToUpper);
        new Operation("replace", Replace);
    }
}