using System.Collections.Immutable;
using System.Globalization;
using System.IO.Enumeration;
using Microsoft.VisualBasic.CompilerServices;
using prac9;

public class Program
{

    private static void Main(string[] args)
    {
        Operation.CreateOperations();
        
        void RewriteLine(string caret, List<char> buffer)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(caret);
            Console.Write(buffer.ToArray());
        }

        void Func(string value = "")
        {
            Console.WriteLine("(Leave blank for default) Input file name >");
            string filename = Console.ReadLine();
            string path =
                @$"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\{(filename == String.Empty ? DateTime.Now.ToLongDateString() : filename)}.txt";
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
            }

            value = File.ReadLines(path).ToList()[0];
            List<string> text = File.ReadLines(path).ToList();

            while (true)
            {
                Console.Clear();
                foreach (var i in text)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine($"{value}\nInput command >");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "save":
                        break;
                    
                    case "edit":
                        Console.Clear();
                        string caret = "> ";
                        string defaultValue = value;

                        Console.WriteLine();

                        List<char> buffer = defaultValue.ToCharArray().Take(Console.WindowWidth - caret.Length - 1)
                            .ToList();
                        List<List<char>> textbuffer = text.Select(x => x.ToCharArray().Take(Console.WindowWidth - caret.Length - 1)
                            .ToList()).ToList();
                        Console.Write(caret);
                        Console.Write(buffer.ToArray());
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);

                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        while (keyInfo.Key != ConsoleKey.Enter)
                        {
                            switch (keyInfo.Key)
                            {
                                case ConsoleKey.LeftArrow:
                                    Console.SetCursorPosition(Math.Max(Console.CursorLeft - 1, caret.Length),
                                        Console.CursorTop);
                                    break;
                                case ConsoleKey.RightArrow:
                                    Console.SetCursorPosition(
                                        Math.Min(Console.CursorLeft + 1, caret.Length + buffer.Count),
                                        Console.CursorTop);
                                    break;
                                case ConsoleKey.UpArrow:
                                    Console.SetCursorPosition(
                                        Math.Min(Console.CursorLeft, caret.Length + buffer.Count),
                                        Console.CursorTop);
                                    buffer = textbuffer[textbuffer.IndexOf(buffer) + 1];
                                    break;
                                case ConsoleKey.Home:
                                    Console.SetCursorPosition(caret.Length, Console.CursorTop);
                                    break;
                                case ConsoleKey.End:
                                    Console.SetCursorPosition(caret.Length + buffer.Count, Console.CursorTop);
                                    break;
                                case ConsoleKey.Backspace:
                                    if (Console.CursorLeft <= caret.Length)
                                    {
                                        break;
                                    }

                                    var cursorColumnAfterBackspace = Math.Max(Console.CursorLeft - 1, caret.Length);
                                    buffer.RemoveAt(Console.CursorLeft - caret.Length - 1);
                                    RewriteLine(caret, buffer);
                                    Console.SetCursorPosition(cursorColumnAfterBackspace, Console.CursorTop);
                                    break;
                                case ConsoleKey.Delete:
                                    if (Console.CursorLeft >= caret.Length + buffer.Count)
                                    {
                                        break;
                                    }

                                    var cursorColumnAfterDelete = Console.CursorLeft;
                                    buffer.RemoveAt(Console.CursorLeft - caret.Length);
                                    RewriteLine(caret, buffer);
                                    Console.SetCursorPosition(cursorColumnAfterDelete, Console.CursorTop);
                                    break;
                                default:
                                    var character = keyInfo.KeyChar;
                                    if (character < 32)
                                        break;
                                    var cursorAfterNewChar = Console.CursorLeft + 1;
                                    if (cursorAfterNewChar > Console.WindowWidth ||
                                        caret.Length + buffer.Count >= Console.WindowWidth - 1)
                                    {
                                        break;
                                    }

                                    buffer.Insert(Console.CursorLeft - caret.Length, character);
                                    RewriteLine(caret, buffer);
                                    Console.SetCursorPosition(cursorAfterNewChar, Console.CursorTop);
                                    break;
                            }

                            keyInfo = Console.ReadKey(true);
                        }

                        Console.Write(Environment.NewLine);

                        value = new string(buffer.ToArray());
                        continue;

                    default:
                        Console.Clear();
                        value = Operation.List.Any(x => x.Trigger == input) ? Operation.List.Find(x => x.Trigger == input).Fn(value) : value;
                        continue;
                }

                break;
            }

            File.WriteAllText(path, value);
        }

        Func();
    }
}