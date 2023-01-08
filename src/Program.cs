using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Stressi
{
    public class Program
    {
        #region Properties

        public static Config LoadedConfig { get; set; }

        public static Stats RunStats { get; set; }

        #endregion

        private static void Main(string[] args)
        {
           
            RunStats = new Stats
            {
                ResponseTimes = new List<long>()
            };

       
            ConsoleEx.Init(args);

            if (ConsoleEx.IsSwitchPresent("v", "version"))
            {
                ShowVersion();
                return;
            }

            if (ConsoleEx.IsSwitchPresent("h", "help") ||
                ConsoleEx.NoOptions())
            {
                ShowHelp();
                return;
            }

            Console.WriteLine("Enter URL:");
            
            string url = Console.ReadLine();

            LoadedConfig = new Config
            {
                // Url = ConsoleEx.GetArgValue("u", "url"),
                Url = url,
                HttpMethod = ConsoleEx.GetArgValue("m", "method"),
                ConcurrentUsers = ConsoleEx.GetArgValueAsInt32("s", "users"),
                Repetitions = ConsoleEx.GetArgValueAsInt32("r", "reps"),
                Verbose = ConsoleEx.IsSwitchPresent("b", "verbose"),
                UserAgent = ConsoleEx.GetArgValue("a", "user-agent"),
                Headers = ConsoleEx.GetArgValueAsDictionary("e", "headers"),
                Timeout = ConsoleEx.GetArgValueAsInt32("t", "timeout")
            };

            if (LoadedConfig.ConcurrentUsers.HasValue &&
                LoadedConfig.ConcurrentUsers.Value == -1)
            {
                LoadedConfig.ConcurrentUsers = long.MaxValue;
            }

            if (LoadedConfig.Repetitions.HasValue &&
                LoadedConfig.Repetitions.Value == -1)
            {
                LoadedConfig.Repetitions = long.MaxValue;
            }

            if (LoadedConfig.Url == null)
            {
                ConsoleEx.WriteError("URL param is required!");
                return;
            }

            RunStressi();
        }
