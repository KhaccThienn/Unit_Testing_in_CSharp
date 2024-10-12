﻿namespace RunGroupWebApp.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the <see cref="ClubController"/> class.
    /// This class contains tests that verify the functionality of the <see cref="ClubController"/>'s methods.
    /// </summary>
    public class ClubControllerTests
    {
        private ClubController       _clubController;
        private IClubRepository      _clubRepository;
        private IPhotoService        _photoService;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClubControllerTests"/> class.
        /// </summary>
        public ClubControllerTests()
        {
            //Dependencies
            _clubRepository      = A.Fake<IClubRepository>();
            _photoService        = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT
            _clubController = new ClubController(_clubRepository, _photoService);
        }

        /// <summary>
        /// Tests if the <see cref="ClubController.Index"/> method returns a successful result.
        /// </summary>
        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            //Arrange - What do i need to bring in?
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);

            //Act
            var result = _clubController.Index();

            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        /// <summary>
        /// Tests if the <see cref="ClubController.DetailClub(int, string)"/> method 
        /// returns a successful result for a valid club ID.
        /// </summary>
        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            //Arrange
            var id      = 1;
            var club    = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(club);

            //Act
            var result = _clubController.DetailClub(id, "RunningClub");

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
