
using System;
using System.Collections.Generic;

namespace Stressi
{
    public class Stats
    {
        #region Timing

        public DateTimeOffset? Started { get; set; }

        public DateTimeOffset? Ended { get; set; }

        public TimeSpan? Duration { get; set; }

        #endregion

        #region Responses

        public long TotalRequests { get; set; }

        public long SuccessfulRequests { get; set; }

        public long FurtherActionResponses { get; set; }

        public long UserErrors { get; set; }

        public long ServerErrors { get; set; }

        public long Exceptions { get; set; }

        #endregion

        #region Response Times

        public List<long> ResponseTimes { get; set; }

        #endregion

        #region Request/Response Sizes

        public long BytesSent { get; set; }

        public long BytesReceived { get; set; }

        #endregion
    }
}
