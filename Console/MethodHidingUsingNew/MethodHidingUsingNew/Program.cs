using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodHidingUsingNew
{
    class Program
    {
        static void Main(string[] args)
        {
            C obj1 = new D();

            obj1.Display();

            B obj2 = new D();

            obj2.Display();

            A obj3 = new D();

            obj3.Display();

            

            //Output

            //ClassD.
            //ClassB.
            //ClassB.

            int[] numbers = { 30, 27, 15, 90, 99, 42, 75 };

            bool isMultiple = MultipleTester(numbers, 3);

            Console.Read();
        }



        private static bool MultipleTester(int[] numbers, int divisor)
        {
            bool isMultiple =
                numbers.All(number => number % divisor == 0);

            var isMultipleLinq = from num in numbers
                                 where num % 3 == 0
                                 select num;


            Console.WriteLine(numbers.Count(num => num % 2 == 0));


            return isMultiple;
        }

    }

    public class A
    {
        public virtual void Display()
        {
            Console.WriteLine("ClassA.");
        }
    }

    public class B : A
    {
        public override void Display()
        {
            Console.WriteLine("ClassB.");
        }
    }

    public class C : B
    {
        public new virtual void Display()
        {
            Console.WriteLine("ClassC.");
        }
    }

    public class D : C
    {
        public override void Display()
        {
            Console.WriteLine("ClassD.");
        }
    }
}
