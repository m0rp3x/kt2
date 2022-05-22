using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata.Ecma335;

namespace prac9;

public class Operation
{
    public string Trigger;
    public Func<List<string>, List<string>> Fn;
    public static List<Operation> List = new();

    public Operation(string trigger, Func<List<string>, List<string>> fn)
    {
        Trigger = trigger;
        Fn = fn;
        List.Add(this);
    }

    public static void CreateOperations()
    {

        List<string> ToLower(List<string> var)
        {
            for (int i = 0; i < var.Count; i++)
            {
                var[i] = var[i].ToLower();
            }
            return var;
        }
        
        List<string> ToUpper(List<string> var)
        {
            for (int i = 0; i < var.Count; i++)
            {
                var[i] = var[i].ToUpper();
            }
            return var;
        }

        List<string> Replace(List<string> var)
        {
            Console.WriteLine("Replace > ");
            string oldValue = Console.ReadLine();
            Console.WriteLine("With > ");
            string newValue = Console.ReadLine();
            for (int i = 0; i < var.Count; i++)
            {
                var[i] = var[i].Replace(oldValue, newValue);
            }

            return var;
        }

        new Operation("tolower", ToLower);
        new Operation("toupper", ToUpper);
        new Operation("replace", Replace);
    }
}