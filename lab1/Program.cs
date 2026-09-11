using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Вхідні дані Варіанту №3
        int N = 91;
        double F = 0.7;
        sbyte A = -50;
        sbyte B = -20;
        float C = -6.5f;

        Console.WriteLine("=== ЗАВДАННЯ 2 (Переведення чисел) ===");
        Console.WriteLine($"N = {N} -> Двійкова (згори вниз): {IntToBaseTopDown(N, 2)}, Вісімкова: {IntToBaseTopDown(N, 8)}, Шістнадцяткова: {IntToBaseTopDown(N, 16)}");
        Console.WriteLine($"F = {F} -> Двійковий дріб: {FracToBin(F, 6)}");

        Console.WriteLine("\n=== ЗАВДАННЯ 3 (Додавання в додатковому коді A=-50, B=-20) ===");
        Add8BitTwosComplement(A, B);

        Console.WriteLine("\n=== ЗАВДАННЯ 4 (Розбір IEEE 754 для C=-6.5) ===");
        ParseIEEE754(C);

        Console.WriteLine("\n=== ЗАВДАННЯ 5 (Похибка представлення дробів) ===");
        DemonstrateFloatError();
    }

    // Завдання 2: Переведення згори вниз
    static string IntToBaseTopDown(int n, int targetBase)
    {
        if (n == 0) return "0";
        string digits = "0123456789ABCDEF";
        StringBuilder result = new StringBuilder();
        int val = Math.Abs(n);

        while (val > 0)
        {
            result.Append(digits[val % targetBase]);
            val /= targetBase;
        }

        return result.ToString();
    }

    // Завдання 2: Дріб
    static string FracToBin(double f, int precision = 6)
    {
        StringBuilder result = new StringBuilder("0.");
        double val = Math.Abs(f) - Math.Truncate(Math.Abs(f));

        for (int i = 0; i < precision; i++)
        {
            val *= 2;
            int bit = (int)val;
            result.Append(bit);
            val -= bit;
        }

        return result.ToString();
    }

    // Завдання 3: 8-бітне додавання та Overflow Flag
    static void Add8BitTwosComplement(sbyte a, sbyte b)
    {
        byte aRaw = (byte)a;
        byte bRaw = (byte)b;
        byte resRaw = (byte)(aRaw + bRaw);
        sbyte resDec = (sbyte)resRaw;

        bool signA = (aRaw & 0x80) != 0;
        bool signB = (bRaw & 0x80) != 0;
        bool signRes = (resRaw & 0x80) != 0;
        bool overflow = (signA == signB) && (signA != signRes);

        Console.WriteLine($"A (-50) у бітах: {Convert.ToString(aRaw, 2).PadLeft(8, '0')}");
        Console.WriteLine($"B (-20) у бітах: {Convert.ToString(bRaw, 2).PadLeft(8, '0')}");
        Console.WriteLine($"Сума у бітах:  {Convert.ToString(resRaw, 2).PadLeft(8, '0')}");
        Console.WriteLine($"Сума десяткова: {resDec}");
        Console.WriteLine($"Переповнення (Overflow): {overflow}");
    }

    // Завдання 4: IEEE 754
    static void ParseIEEE754(float value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

        StringBuilder sb = new StringBuilder();
        foreach (byte b in bytes) sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

        string bits = sb.ToString();
        Console.WriteLine($"32 біти: {bits}");
        Console.WriteLine($"Знак: {bits[0]}, Порядок: {bits.Substring(1, 8)}, Мантиса: {bits.Substring(9)}");
    }

    // Завдання 5: Похибка
    static void DemonstrateFloatError()
    {
        double sum = 0.1 + 0.2;
        Console.WriteLine($"0.1 + 0.2 == 0.3: {sum == 0.3}");
        Console.WriteLine($"Фактичне значення 0.1 + 0.2 = {sum:G17}");
    }
}