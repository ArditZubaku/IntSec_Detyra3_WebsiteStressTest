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
