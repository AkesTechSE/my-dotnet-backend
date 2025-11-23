using System;

class Program
{
    static void Main()
    {
        int i = 400;

        Console.WriteLine($"Original int: i = {i}\n");

        // 1️⃣ Explicit cast to byte (wraps around)
        byte b = (byte)i;
        Console.WriteLine($"Explicit cast to byte: b = (byte)i => {b} (wraps around using modulo 256)");

        // 2️⃣ Explicit cast to short (no data loss)
        short s = (short)i;
        Console.WriteLine($"Explicit cast to short: s = (short)i => {s} (fits in 16-bit)");

        // 3️⃣ Explicit cast to long (no data loss)
        long l = (long)i;
        Console.WriteLine($"Explicit cast to long: l = (long)i => {l} (fits in 64-bit)");

        // 4️⃣ Safe conversion using Convert.ToByte (throws exception if out of range)
        try
        {
            byte safeB = Convert.ToByte(i);
            Console.WriteLine($"Convert.ToByte(i) => {safeB}");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Convert.ToByte(i) => OverflowException: value out of byte range (0-255)");
        }

        // 5️⃣ Show more examples with negative values
        int j = -50;
        byte b2 = (byte)j; // wrap around
        Console.WriteLine($"\nint j = {j} cast to byte: b2 = {b2} (wraps around, underflow)");
    }
}
