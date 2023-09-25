namespace Decenea.Domain.Helpers;

public static class RandomStringGenerator
{
    private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{}|<>?/";
    private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));
    private static int _seed = Environment.TickCount;
    public static string RandomString(int size)
    {
        char[] result = new char[size];
        Random.Value ??= new Random(_seed);
        for (int i = 0; i < size; i++)
        {
            result[i] = Characters[Random.Value.Next(Characters.Length)];
        }

        return new string(result);
    }
}