// LinqGen.Generator, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Text;

namespace Cathei.LinqGen.Generator;

public static class Base62
{
    private const string PossibleLetters =
        "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    private static readonly StringBuilder Builder = new();

    public static string Encode(uint value)
    {
        if (value == 0)
            return "0";

        Builder.Clear();

        while (value > 0)
        {
            int idx = (int)(value % (uint)PossibleLetters.Length);

            Builder.Append(PossibleLetters[idx]);

            value /= (uint)PossibleLetters.Length;
        }

        return Builder.ToString();
    }
}