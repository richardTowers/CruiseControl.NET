using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThoughtWorks.CruiseControl.WebDashboard.IO;

namespace ThoughtWorks.CruiseControl.WebDashboard.MVC
{
    /// <summary>
    /// A response to be used with HTML5 Server Sent Events.
    /// This will keep the HTTP connection alive and stream 
    /// events to the browser.
    /// </summary>
    public class EventStreamResponse : IResponse
    {
        private readonly Func<string> _getCurrentResponse;

        private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(1);
        private readonly TimeSpan _maxRequestTime = TimeSpan.FromHours(1);

        /// <param name="getCurrentResponse">
        /// A delegate to get the current response. 
        /// This will be called repeatedly and the responses streamed to the client.
        /// </param>
        /// <param name="pollInterval">
        /// For event-stream responses the interval between partial responses.
        /// </param>
        /// <param name="maxRequestTime">
        /// For event-stream responses the maximum time to allow a connection
        /// to live before dropping it.
        /// </param>
        public EventStreamResponse(
            Func<string> getCurrentResponse,
            TimeSpan? pollInterval = null,
            TimeSpan? maxRequestTime = null)
        {
            _getCurrentResponse = getCurrentResponse;
            _pollInterval = pollInterval ?? _pollInterval;
            _maxRequestTime = maxRequestTime ?? _maxRequestTime;
        }

        public void Process(HttpResponse response)
        {
            response.ContentType = MimeType.EventStream.ContentType;
            var startTime = DateTime.Now;
            while (DateTime.Now < startTime + _maxRequestTime)
            {
                try
                {
                    response.Write("data: " + _getCurrentResponse() + "\n\n");
                    response.Flush();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                catch(HttpException exception)
                {
                    if (exception.ErrorCode == -2147023667)
                    {
                        // "The remote host closed the connection"
                        // This is fine, it just means the user has left the page.
                        break;
                    }
                    throw;
                }
            }
            response.Close();
        }

        public ConditionalGetFingerprint ServerFingerprint { get; set; }
    }
}