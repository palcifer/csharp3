public class FizzBuzz
{
    public void CountTo(int last)
    {
        for (int i = 1; i <= last; i++)
        {
            if (ContainsChar(i, '7'))
            {
                Console.WriteLine("IzuBizu");
                continue;
            }
            if (i % 5 == 0 && i % 3 == 0)
            {
                Console.WriteLine("FizzBuzz");
                continue;
            }
            if (i % 3 == 0)
            {
                Console.WriteLine("Fizz");
                continue;
            }
            if (i % 5 == 0)
            {
                Console.WriteLine("Buzz");
                continue;
            }
            Console.WriteLine(i);
        }
    }

    private bool ContainsChar(int i, char c)
    {
        string s = i.ToString();
        return s.Contains(c);
    }
}
