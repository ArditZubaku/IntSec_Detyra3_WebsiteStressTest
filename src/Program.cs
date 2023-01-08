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

        #region Helper functions

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: stressi [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -v | --version               Show the app version.");
            Console.WriteLine("  -h | --help                  Show the app usage and help information.");
            Console.WriteLine("  -u | --url <url>             The URL to use for each request. Required!");
            Console.WriteLine("  -m | --method <method>       The HTTP method to use for each request. Defaults to 'GET'.");
            Console.WriteLine("  -s | --users <number>        Number of concurrent users to simulate. Defaults to 10.");
            Console.WriteLine("  -r | --reps <number>         Number of repetitions pr. user. Defaults to 10.");
            Console.WriteLine("  -b | --verbose               Turn on verbose mode, which shows a lot more console output.");
            Console.WriteLine("  -a | --user-agent <string>   Set the user-agent to use.");
            Console.WriteLine("  -e | --headers <string>      Comma-list of key:value, like so: key1:value1,key2:value2");
            Console.WriteLine("  -t | --timeout <number>      Set the timeout for each request to N ms.");

            Console.WriteLine();
            ConsoleEx.WriteLineWordWrapped("If a value for one of the options has spaces in it, you can use quotation marks around the string, " +
                                           "like so: \"this will all be the same value\"");

            Console.WriteLine();
            ConsoleEx.WriteLineWordWrapped("Number of users and repetitions pr. user determines the total number of requests that will be performed. " +
                                           "They both default to 10, which means 100 total requests.");

            Console.WriteLine();
            ConsoleEx.WriteLineWordWrapped($"For both options -u and -r you can supply -1 as a value to indicate it to use the max value of a " +
                                           $"int64, which is {long.MaxValue}, which will basically run forever..");

            Console.WriteLine();
        }

        private static void ShowVersion()
        {
            Console.WriteLine($"Version {Assembly.GetExecutingAssembly().GetName().Version}");
        }

        #endregion
