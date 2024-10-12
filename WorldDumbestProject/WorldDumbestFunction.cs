using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDumbestProject
{
    /// <summary>
    /// A class containing the world's dumbest function for demonstration purposes.
    /// </summary>
    public class WorldDumbestFunction
    {
        /// <summary>
        /// Returns "Pikachu" if the input number is zero, otherwise returns "WTF".
        /// </summary>
        /// <param name="num">An integer to evaluate.</param>
        /// <returns>
        /// A string "Pikachu" if <paramref name="num"/> is 0; otherwise, "WTF".
        /// </returns>
        public string ReturnPikachuIfZero(int num)
        {
            if (num == 0)
            {
                return "Pikachu";
            }
            return "WTF";
        }
    }
}
