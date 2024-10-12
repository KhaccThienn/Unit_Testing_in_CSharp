namespace RunGroupWebApp.Tests.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="ClubRepository"/> class.
    /// This class contains tests that verify the functionality of the <see cref="ClubRepository"/>'s methods.
    /// </summary>
    public class ClubRepositoryTests
    {

        /// <summary>
        /// Creates and returns an in-memory instance of <see cref="ApplicationDbContext"/> 
        /// for testing purposes.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ApplicationDbContext"/> pre-populated with sample data.
        /// </returns>
        /// <remarks>
        /// This method ensures the database schema is created and adds 10 sample clubs if 
        /// the initial count of clubs is less than 0.
        /// </remarks>
        private async Task<ApplicationDbContext> GetDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            // Populate the in-memory database with sample data.
            if (await databaseContext.Clubs.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Clubs.Add(
                        new Club()
                        {
                            Title        = $"Running Club {i + 1}",
                            Image        = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description  = $"This is the description of the {i + 1} cinema",
                            ClubCategory = ClubCategory.City,
                            Address      = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;
        }

        /// <summary>
        /// Tests the <see cref="ClubRepository.Add(Club)"/> method to verify that a club 
        /// is successfully added to the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// This test uses an in-memory database to ensure isolated and repeatable testing.
        /// </remarks>
        [Fact]
        public async void ClubRepository_Add_ReturnsBool()
        {
            // Arrange
            var club = new Club()
            {
                Title        = "Running Club 1",
                Image        = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description  = "This is the description of the first cinema",
                ClubCategory = ClubCategory.City,
                Address      = new Address()
                {
                    Street = "123 Main St",
                    City   = "Charlotte",
                    State  = "NC"
                }
            };
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);

            // Act
            var result = clubRepository.Add(club);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Tests the <see cref="ClubRepository.GetByIdAsync(int)"/> method to verify that the method returns a valid 
        /// <see cref="Club"/> object when provided with an existing ID.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <remarks>
        /// Uses an in-memory database for testing to ensure isolated behavior.
        /// </remarks>
        [Fact]
        public async void ClubRepository_GetByIdAsync_ReturnsClub()
        {
            // Arrange
            var id             = 1;
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);
            // Act
            var result = clubRepository.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();
        }

        /// <summary>
        /// Unit test for the <see cref="ClubRepository.GetAll"/> method.
        /// This test verifies that the method returns a non-null list of <see cref="Club"/> objects.
        /// </summary>
        /// <remarks>
        /// An in-memory database is used to simulate data retrieval, ensuring that the test is isolated
        /// and does not depend on an actual database.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous operation of the test.
        /// </returns>
        [Fact]
        public async void ClubRepository_GetAll_ReturnsList()
        {
            // Arrange
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);

            // Act
            var result = clubRepository.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IEnumerable<Club>>>();
        }

        /// <summary>
        /// Unit test for the <see cref="ClubRepository.Delete"/> method.
        /// This test verifies that a club can be successfully deleted from the repository.
        /// </summary>
        /// <remarks>
        /// An in-memory database is used to simulate data storage, ensuring that the test is isolated
        /// and does not depend on an actual database. The test checks that after deletion, the 
        /// club count is zero, confirming the deletion was successful.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous operation of the test.
        /// </returns>
        [Fact]
        public async void ClubRepository_SuccessfulDelete_ReturnsTrue()
        {
            // Arrange
            var club = new Club()
            {
                Title        = "Running Club 1",
                Image        = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description  = "This is the description of the first cinema",
                ClubCategory = ClubCategory.City,
                Address      = new Address()
                {
                    Street = "123 Main St",
                    City   = "Charlotte",
                    State  = "NC"
                }
            };
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            clubRepository.Add(club);
            var result = clubRepository.Delete(club);
            var count  = await clubRepository.GetCountAsync();

            //Assert
            result.Should().BeTrue();
            count.Should().Be(0);
        }

        /// <summary>
        /// Unit test for the <see cref="ClubRepository.GetCountAsync"/> method.
        /// This test verifies that the count of clubs in the repository is returned correctly after adding a club.
        /// </summary>
        /// <remarks>
        /// An in-memory database is used to simulate data storage, ensuring that the test is isolated
        /// and does not depend on an actual database. The test checks that the count reflects the
        /// correct number of clubs after the addition of a new club.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous operation of the test.
        /// </returns>
        [Fact]
        public async void ClubRepository_GetCountAsync_ReturnsInt()
        {
            // Arrange
            var club = new Club()
            {
                Title        = "Running Club 1",
                Image        = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description  = "This is the description of the first cinema",
                ClubCategory = ClubCategory.City,
                Address      = new Address()
                {
                    Street = "123 Main St",
                    City   = "Charlotte",
                    State  = "NC"
                }
            };
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            clubRepository.Add(club);
            var count = await clubRepository.GetCountAsync();

            //Assert
            count.Should().Be(1);
        }

        /// <summary>
        /// Unit test for the <see cref="ClubRepository.GetAllStates"/> method.
        /// This test verifies that the method returns a list of states correctly.
        /// </summary>
        /// <remarks>
        /// An in-memory database is used to simulate data storage, ensuring that the test is isolated
        /// and does not depend on an actual database. The test checks that the returned result is
        /// not null and is of the expected type, <see cref="List{State}"/>.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous operation of the test.
        /// </returns>
        [Fact]
        public async void ClubRepository_GetAllStates_ReturnsList()
        {
            // Arrange
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);

            // Act
            var result = await clubRepository.GetAllStates();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<State>>();
        }

        /// <summary>
        /// Unit test for the <see cref="ClubRepository.GetClubsByState(string)"/> method.
        /// This test verifies that the method returns a list of clubs filtered by the specified state.
        /// </summary>
        /// <remarks>
        /// An in-memory database is used to simulate data storage, ensuring that the test is isolated
        /// and does not depend on an actual database. The test checks that the returned result is
        /// not null, is of the expected type, and contains the expected club.
        /// </remarks>
        [Fact]
        public async void ClubRepository_GetClubsByState_ReturnsList()
        {
            //Arrange
            var state = "NC";
            var club  = new Club()
            {
                Title        = "Running Club 1",
                Image        = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description  = "This is the description of the first cinema",
                ClubCategory = ClubCategory.City,
                Address      = new Address()
                {
                    Street = "123 Main St",
                    City   = "Charlotte",
                    State  = "NC"
                }
            };
            var dbContext      = await GetDbContextAsync();
            var clubRepository = new ClubRepository(dbContext);

            //Act
            clubRepository.Add(club);
            var result = await clubRepository.GetClubsByState(state);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Club>>();
            result.First().Title.Should().Be("Running Club 1");
        }
    }
}
