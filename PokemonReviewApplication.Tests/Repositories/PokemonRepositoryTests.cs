namespace PokemonReviewApplication.Tests.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="PokemonRepository"/> class.
    /// This class contains tests that verify the functionality of the <see cref="PokemonRepository"/>'s methods.
    /// </summary>
    public class PokemonRepositoryTests
    {
        /// <summary>
        /// Asynchronously creates and configures an in-memory database context for testing purposes.
        /// This method initializes the database with sample Pokémon data if it is empty.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a <see cref="DataContext"/> 
        /// instance configured with the in-memory database and pre-populated with sample Pokémon data.
        /// </returns>
        /// <remarks>
        /// This method uses a unique database name generated from a GUID to ensure isolation between tests.
        /// If the database is empty, it populates it with a set of Pokémon, categories, and reviews to facilitate testing.
        /// </remarks>
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new DataContext(options);

            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Pokemon.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Pokemon.Add(
                    new Pokemon()
                    {
                        Name              = "Pikachu",
                        BirthDate         = new DateTime(1903, 1, 1),
                        PokemonCategories = new List<PokemonCategory>()
                        {
                            new PokemonCategory()
                            {
                                Category = new Category
                                {
                                    Name = "Electric"
                                }
                            }
                        },
                        Reviews          = new List<Review>()
                        {
                            new Review
                            {
                                Title    = "Pikachu",
                                Text     = "Pickahu is the best pokemon, because it is electric", 
                                Rating   = 5,
                                Reviewer = new Reviewer
                                {
                                    FirstName = "Teddy", 
                                    LastName  = "Smith"
                                }
                            },
                            new Review
                            {
                                Title    = "Pikachu",
                                Text     = "Pickachu is the best a killing rocks",
                                Rating   = 5,
                                Reviewer = new Reviewer
                                {
                                    FirstName = "Taylor",
                                    LastName  = "Jones"
                                }
                            },
                            new Review
                            {
                                Title    = "Pikachu",
                                Text     = "Pickachu, pikachu, pikachu",
                                Rating   = 1,
                                Reviewer = new Reviewer
                                {
                                    FirstName = "Jessica",
                                    LastName  = "McGregor"
                                }
                            },
                        }
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        /// <summary>
        /// Unit test for the <see cref="PokemonRepository.GetPokemon(string)"/> method.
        /// This test verifies that the method returns a <see cref="Pokemon"/> object
        /// when called with a valid Pokémon name.
        /// </summary>
        /// <remarks>
        /// The test sets up an in-memory database context, populates it with sample Pokémon data,
        /// and then calls the <see cref="GetPokemon(string)"/> method of the <see cref="PokemonRepository"/> 
        /// to retrieve a Pokémon by name. It asserts that the result is not null and is of type <see cref="Pokemon"/>.
        /// </remarks>
        [Fact]
        public async void PokemonRepository_GetPokemon_ReturnsPokemon()
        {
            // Arrange
            var name              = "Pikachu";
            var dbContext         = await GetDatabaseContext();
            var pokemonRepository = new PokemonRepository(dbContext);

            // Act
            var result = pokemonRepository.GetPokemon(name);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Pokemon>();
        }

        /// <summary>
        /// Unit test for the <see cref="PokemonRepository.GetPokemonRating(int)"/> method.
        /// This test verifies that the method returns a decimal value representing the Pokémon's rating,
        /// which is expected to be between 1 and 10, inclusive.
        /// </summary>
        /// <remarks>
        /// The test sets up an in-memory database context, retrieves a Pokémon by its ID,
        /// and then calls the <see cref="PokemonRepository.GetPokemonRating(int)"/> method of the <see cref="PokemonRepository"/> 
        /// to obtain the rating. It asserts that the result is not zero and falls within the expected range.
        /// </remarks>
        [Fact]
        public async void PokemonRepository_GetPokemonRating_ReturnDecimalBetweenOneAndTen()
        {
            // Arrange
            var pokeId            = 1;
            var dbContext         = await GetDatabaseContext();
            var pokemonRepository = new PokemonRepository(dbContext);

            // Act
            var result = pokemonRepository.GetPokemonRating(pokeId);

            // Assert
            result.Should().NotBe(0);
            result.Should().BeInRange(1, 10);
        }
    }
}
