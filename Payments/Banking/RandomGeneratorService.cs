using Payments.Banking.Interfaces;
using System;
using System.Linq;
using System.Threading;

namespace Payments.Banking
{
    public class RandomGeneratorService : IRandomGenerator
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref _seed))
        );

        private static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }

        public int GetRandomNumber(int maxValue)
        {
            return RandomWrapper.Value.Next(0, maxValue);
        }

        public int GetRandomNumber(int minValue, int maxValue)
        {
            return RandomWrapper.Value.Next(minValue, maxValue);
        }

        public double GetRandomFloatingPointNumber()
        {
            return RandomWrapper.Value.NextDouble();
        }

        public string GenerateRandomAlphanumericString(int length)
        {
            const string chars = "JKLMN0123456789OPQRSTUVWXYABCDEFGHIZ";
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[RandomWrapper.Value.Next(s.Length)]).ToArray());
        }
    }
}