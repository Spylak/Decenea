using Decenea.Domain.Constants;

namespace Decenea.Domain.Helpers;

public static class RandomStringGenerator
{
    private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));
    private static int _seed = Environment.TickCount;
    public static string RandomString(int size)
    {
        char[] result = new char[size];
        Random.Value ??= new Random(_seed);
        for (int i = 0; i < size; i++)
        {
            result[i] = StringConstants.Characters[Random.Value.Next(StringConstants.Characters.Length)];
        }

        return new string(result);
    }
}