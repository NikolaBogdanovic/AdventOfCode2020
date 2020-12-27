using System.IO;
using System.Text;

namespace Advent_of_Code_2020.Answers
{
    public static class Day25
    {
        public static long PartOne()
        {
            var lines = File.ReadAllLines("Inputs\\Puzzle25.txt", Encoding.UTF8);

            int cardPublicKey = 0;
            int doorPublicKey = 0;

            foreach (var data in lines)
            {
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                if (cardPublicKey == 0)
                    cardPublicKey = int.Parse(data);
                else
                    doorPublicKey = int.Parse(data);
            }

            var cardSubjectNumber = 7;
            var remainder = 20201227;

            var cardLoopSize = 0;

            var transform = 1;
            while (transform != cardPublicKey)
            {
                transform *= cardSubjectNumber;
                transform %= remainder;

                ++cardLoopSize;
            }

            var doorLoopSize = 0;

            transform = 1;
            while (transform != doorPublicKey)
            {
                transform *= cardSubjectNumber;
                transform %= remainder;

                ++doorLoopSize;
            }

            long cardSecretKey = 1;
            for (var i = 0; i < cardLoopSize; i++)
            {
                cardSecretKey *= doorPublicKey;
                cardSecretKey %= remainder;
            }

            long doorSecretKey = 1;
            for (var i = 0; i < doorLoopSize; i++)
            {
                doorSecretKey *= cardPublicKey;
                doorSecretKey %= remainder;
            }

            if (cardSecretKey == doorSecretKey)
                return cardSecretKey;

            return -1;
        }
    }
}
