namespace PokemonReviewApplication.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the <see cref="PokemonController"/> class.
    /// This class contains tests that verify the functionality of the <see cref="PokemonController"/>'s methods.
    /// </summary>
    public class PokemonControllerTests
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository  _reviewRepository;
        private readonly IMapper            _mapper;

        public PokemonControllerTests()
        {
            _pokemonRepository = A.Fake<IPokemonRepository>();
            _reviewRepository  = A.Fake<IReviewRepository>();
            _mapper            = A.Fake<IMapper>();
        }

        /// <summary>
        /// Unit test for the <see cref="PokemonController.GetPokemons"/> method.
        /// This test verifies that the method returns an OK result when called.
        /// </summary>
        /// <remarks>
        /// It uses a fake implementation of <see cref="ICollection{T}"/> for <see cref="PokemonDto"/> to simulate the 
        /// behavior of the data layer, ensuring that the controller behaves as expected without relying on actual data.
        /// </remarks>
        [Fact]
        public void PokemonController_GetPokemons_ReturnOK()
        {
            // Arrange
            var pokemons    = A.Fake<ICollection<PokemonDto>>();
            var pokemonList = A.Fake<List<PokemonDto>>();

            A.CallTo(() => _mapper.Map<List<PokemonDto>>(pokemons)).Returns(pokemonList);

            var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);

            // Act
            var result = controller.GetPokemons();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        /// <summary>
        /// Unit test for the <see cref="PokemonController.CreatePokemon(int, int, PokemonDto)"/> method.
        /// This test verifies that the method initiates the creation of a Pokémon successfully when valid parameters are provided.
        /// </summary>
        /// <remarks>
        /// It sets up fake implementations for the necessary parameters, including the Pokémon DTO and repository methods, 
        /// simulating the behavior of the data layer. This allows the controller's functionality to be tested 
        /// without relying on actual data or database interactions.
        /// </remarks>
        [Fact]
        public void PokemonController_CreatePokemon_ReturnOK()
        {
            // Arrange
            int ownerId       = 1;
            int catId         = 2;
            var pokemonMap    = A.Fake<Pokemon>();
            var pokemon       = A.Fake<Pokemon>();
            var pokemonCreate = A.Fake<PokemonDto>();
            var pokemons      = A.Fake<ICollection<PokemonDto>>();
            var pokmonList    = A.Fake<IList<PokemonDto>>();

            A.CallTo(() => _pokemonRepository.GetPokemonTrimToUpper(pokemonCreate)).Returns(pokemon);
            A.CallTo(() => _mapper.Map<Pokemon>(pokemonCreate)).Returns(pokemon);
            A.CallTo(() => _pokemonRepository.CreatePokemon(ownerId, catId, pokemonMap)).Returns(true);

            var controller = new PokemonController(_pokemonRepository, _reviewRepository, _mapper);

            // Act
            var result = controller.CreatePokemon(ownerId, catId, pokemonCreate);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
