using System;
using System.Collections.Generic;
using System.Linq;

namespace Stressi
{
    public class ConsoleEx
    {
        #region Properties

        public static string[] CmdArgs { get; set; }

        public static string ShortHandOptionPrefix { get; set; }

        public static string LongHandOptionPrefix { get; set; }

        #endregion

        #region Initiation

        public static void Init(string[] args, string shortHandOptionPrefix = "-", string longHandOptionPrefix = "--")
        {
            CmdArgs = args;
            ShortHandOptionPrefix = shortHandOptionPrefix;
            LongHandOptionPrefix = longHandOptionPrefix;
        }

        #endregion

        #region Get functions

        public static string GetArgValue(params string[] keys)
        {
            if (CmdArgs == null ||
                CmdArgs.Length < 2)
            {
                return null;
            }

            for (var i = 0; i < CmdArgs.Length; i++)
            {
                if (keys.Any(key => CmdArgs[i] == $"{ShortHandOptionPrefix}{key}" ||
                                    CmdArgs[i] == $"{LongHandOptionPrefix}{key}"))
                {
                    return CmdArgs[i + 1];
                }
            }

            return null;
        }

        public static Dictionary<string, string> GetArgValueAsDictionary(params string[] keys)
        {
            var value = GetArgValue(keys);
            var items = value?.Split(',');

            return items?.Select(item => item.Split(':'))
                .Where(kv => kv.Length == 2)
                .ToDictionary(kv => kv[0], kv => kv[1]);
        }

        public static int? GetArgValueAsInt32(params string[] keys)
        {
            var value = GetArgValue(keys);

            if (value != null &&
                int.TryParse(value, out var temp))
            {
                return temp;
            }

            return null;
        }

        public static bool IsSwitchPresent(params string[] keys)
        {
            if (CmdArgs == null)
            {
                return false;
            }

            return keys.Any(key => CmdArgs.Any(n => n == $"{ShortHandOptionPrefix}{key}") ||
                                   CmdArgs.Any(n => n == $"{LongHandOptionPrefix}{key}"));
        }

        public static bool NoOptions()
        {
            return CmdArgs?.Length == 0;
        }

        #endregion

        #region Write functions

        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("[ERROR] ");

            Console.ResetColor();
            Console.WriteLine(message);
        }

        public static void WriteLineWordWrapped(string message)
        {
            var words = message.Split(' ');
            var charsLeft = Console.WindowWidth;

            foreach (var word in words)
            {
                if (word.Length > charsLeft)
                {
                    charsLeft = Console.WindowWidth;
                    Console.WriteLine();
                }

                charsLeft -= word.Length + 1;

                Console.Write($"{word} ");
            }

            Console.WriteLine();
        }

        #endregion
    }
}
