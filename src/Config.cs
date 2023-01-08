using System.Collections.Generic;

namespace Stressi
{
    public class Config
    {
    
        public string Url { get; set; }

        public string HttpMethod { get; set; }

        public long? ConcurrentUsers { get; set; }

        public long? Repetitions { get; set; }

        public bool Verbose { get; set; }

        public string UserAgent { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public int? Timeout { get; set; }
    }

    public class DefaultConfig
    {

        public const string HttpMethod = "GET";

        public const int ConcurrentUsers = 10;

        public const int Repetitions = 10;
    }
}
