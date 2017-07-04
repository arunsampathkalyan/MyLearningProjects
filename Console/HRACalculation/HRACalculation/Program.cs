using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRACalculation
{
    public class Program
    {
        static void Main(string[] args)
        {
            double annualBasic; bool isMetro; int rent;

            Console.WriteLine("Enter the Annual Basic Pay : ");

            if (!double.TryParse(Console.ReadLine(), out annualBasic))
            {
                Console.WriteLine("Enter Valid Annual Basic Pay");
            }

            Console.WriteLine("Is Metro : ");

            isMetro = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter the monthly rent : ");
            rent = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("HRA Exemption : " + CalcualteHRAExemption(annualBasic, annualBasic / 2, rent * 12, isMetro));

            Console.Read();
        }

        static double CalcualteHRAExemption(double annalBasic, double allowedHRA, int totalRentPaid, bool isMetro)
        {
            double hraExemptionAllowed = 0;

            double halfBasic, actualRentPaidminusTenPercent = 0;

            halfBasic = isMetro ? (annalBasic / 100) * 50 : (annalBasic / 100) * 40;
            actualRentPaidminusTenPercent = totalRentPaid - ((annalBasic / 100) * 10);

            hraExemptionAllowed = (halfBasic < allowedHRA) ? (halfBasic < actualRentPaidminusTenPercent ? halfBasic : actualRentPaidminusTenPercent) : (allowedHRA < actualRentPaidminusTenPercent) ? allowedHRA : actualRentPaidminusTenPercent;

            return hraExemptionAllowed;
        }
    }
}
