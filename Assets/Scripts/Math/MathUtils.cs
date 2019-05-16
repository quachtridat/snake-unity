using System.Linq;

public static class MathUtils {
    /// <summary>
    /// Greatest common divisor of <paramref name="a"/> and <paramref name="b"/>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Gcd(int a, int b) {
        return b == 0 ? a : Gcd(b, a % b);
    }

    /// <summary>
    /// Greatest common divisor of all numbers in <paramref name="args"/>.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static int Gcd(params int[] args) {
        return args.Aggregate(Gcd);
    }

    /// <summary>
    /// Least common multiple of <paramref name="a"/> and <paramref name="b"/>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Lcm(int a, int b) {
        return a / Gcd(a, b) * b;
    }

    /// <summary>
    /// Least common multiple of all numbers in <paramref name="args"/>.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static int Lcm(params int[] args) {
        return args.Aggregate(Lcm);
    }
}