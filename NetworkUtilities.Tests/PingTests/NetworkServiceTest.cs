namespace NetworkUtilities.Tests.PingTests
{
    /// <summary>
    /// Contains unit tests for <see cref="NetworkService"/>.
    /// </summary>
    public class NetworkServiceTest
    {
        private readonly NetworkService _networkService;
        private readonly IDNS _dns;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkServiceTest"/> class.
        /// </summary>
        public NetworkServiceTest()
        {
            _dns            = A.Fake<IDNS>();
            _networkService = new NetworkService(_dns);
        }

        /// <summary>
        /// Tests the <see cref="NetworkService.SendPing"/> method to ensure it returns the expected success message.
        /// </summary>
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            // Arrange
            A.CallTo(() => _dns.SendDNS()).Returns(true);

            // Act
            var result = _networkService.SendPing();

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        /// <summary>
        /// Tests the <see cref="NetworkService.PingTimedOut"/> method to verify it returns the correct sum of two integers.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <param name="expected">The expected sum.</param>
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimedOut_ReturnInt(int a, int b, int expected)
        {
            // Act
            var result = _networkService.PingTimedOut(a, b);

            // Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-1000000, 0);
        }

        /// <summary>
        /// Tests the <see cref="NetworkService.LastPingDate"/> method to ensure the returned date is within a valid range.
        /// </summary>
        [Fact]
        public void NetworkService_LastPingDate_ReturnString()
        {
            // Act
            var result = _networkService.LastPingDate();

            // Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        /// <summary>
        /// Tests the <see cref="NetworkService.GetPingOptions"/> method to ensure it returns a valid <see cref="PingOptions"/> object.
        /// </summary>
        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            // Arrange
            var expected = new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            };

            // Act
            var result = _networkService.GetPingOptions();

            // Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(expected.Ttl);
        }

        /// <summary>
        /// Tests the <see cref="NetworkService.MostRecentPings"/> method to ensure it returns a list of valid <see cref="PingOptions"/> objects.
        /// </summary>
        [Fact]
        public void NetworkService_MostRecentPings_ReturnObject()
        {
            // Arrange
            var expected = new PingOptions
            {
                DontFragment = true,
                Ttl = 1
            };

            // Act
            var result = _networkService.MostRecentPings();

            // Assert
            result.Should().BeOfType<List<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}