using NetworkUtilities.DNS;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace NetworkUtilities.Pings
{
    /// <summary>
    /// Provides network-related services such as sending pings, managing DNS operations, 
    /// and retrieving ping options.
    /// </summary>
    public class NetworkService
    {
        private readonly IDNS _dns;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkService"/> class 
        /// with the specified DNS service.
        /// </summary>
        /// <param name="dns">An instance of <see cref="IDNS"/> used for DNS operations.</param>
        public NetworkService(IDNS dns)
        {
            _dns = dns;
        }

        /// <summary>
        /// Sends a DNS request and returns the result as a status message.
        /// </summary>
        /// <returns>
        /// A string indicating the success or failure of the DNS operation.
        /// </returns>
        public string SendPing()
        {
            var dnsSentStatus = _dns.SendDNS();
            return dnsSentStatus ? "Success: Ping sent!" : "Failed: Ping not sent!";
        }

        /// <summary>
        /// Simulates a ping timeout operation by adding two integers.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <returns>The sum of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public int PingTimedOut(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Gets the timestamp of the last ping operation.
        /// </summary>
        /// <returns>The current UTC timestamp.</returns>
        public DateTime LastPingDate()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Retrieves the ping options for the current network operation.
        /// </summary>
        /// <returns>A <see cref="PingOptions"/> object with default values.</returns>
        public PingOptions GetPingOptions()
        {
            return new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            };
        }

        /// <summary>
        /// Retrieves a list of the most recent ping options used.
        /// </summary>
        /// <returns>A list of <see cref="PingOptions"/> objects.</returns>
        public List<PingOptions> MostRecentPings()
        {
            List<PingOptions> pingOptions = new List<PingOptions>
            {
                new PingOptions { DontFragment = true, Ttl = 1 },
                new PingOptions { DontFragment = true, Ttl = 1 },
                new PingOptions { DontFragment = true, Ttl = 1 },
                new PingOptions { DontFragment = true, Ttl = 1 }
            };

            return pingOptions;
        }
    }
}
