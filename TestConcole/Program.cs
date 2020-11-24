using System;

namespace TestConcole
{
    class Program
    {
        static void Main(string[] args)
        {
            DerivedClass der = new DerivedClass();
            der.Method();

            BaseClass bas = der;
            bas.Method();

            Console.ReadKey();
        }
    }
}
