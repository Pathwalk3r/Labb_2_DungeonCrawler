using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_2_DungeonCrawler
{
    public class Dice
    {
        private int _numbersOfDice;

        private int _sidesPerDice;

        private int _modifier;
        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {
            _numbersOfDice = numberOfDice;
            _sidesPerDice = sidesPerDice;
            _modifier = modifier;
        }

        public int Throw()
        {
            int result = 0;
            for (int i = 0; i < _numbersOfDice; i++)
            {
                Random r = new Random();
                result += r.Next(1, _sidesPerDice + 1);
            }
            return result + _modifier;
        }
        public override string ToString()
        {
            return $"{_numbersOfDice}D{_sidesPerDice}+{_modifier}";
        }
    }
}
