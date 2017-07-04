using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.Async
{
    public class AsynchronousPrg
    {
        public static string LongRunOperation()
        {
            int counter = 0;
            for (counter = 0; counter < 50000; counter++)
            {
                Console.WriteLine(counter);
            }

            return "Counter : " + counter;

        }

        public static void DoStuff()
        {
            var counterVal = LongRunOperation();
            Console.WriteLine(counterVal);
        }

        public async static Task<string> LongRunAsysn()
        {
            int counter = 50000;
            for (counter = 50000; counter > 0; counter--)
            {
                Console.WriteLine(counter);
            }

            return "Counter : " + counter;
        }

        public async static Task DoStuffAsync()
        {
            await Task.Run(() => LongRunAsysn());
        }

    }
}
