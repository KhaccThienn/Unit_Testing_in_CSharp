using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldDumbestProject;

namespace WorldDumbestProject.Test
{
    /// <summary>
    /// This class contains unit tests for the <see cref="WorldDumbestFunction"/>'s methods.
    /// </summary>
    public static class WorldDumbestFunctionTest
    {
        /// <summary>
        /// Unit test for <see cref="WorldDumbestFunction"/>.
        /// This test verifies that the function returns "Pikachu" when input is zero.
        /// </summary>
        public static void WorldDumbestFunction_ReturnPikachuIfZero_ReturnString()
        {
            try
            {
                // Arrange: Prepare input and expected output
                int num          = 0;
                var worldDumbest = new WorldDumbestFunction();

                // Act: Call the function to test
                string result = worldDumbest.ReturnPikachuIfZero(num);

                // Assert: Verify the result
                if (result == "Pikachu")
                {
                    Console.WriteLine("[PASSED]: WorldDumbestFunction.ReturnPikachuIfZero");
                }
                else
                {
                    Console.WriteLine("[FAILED]: WorldDumbestFunction.ReturnPikachuIfZero");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
