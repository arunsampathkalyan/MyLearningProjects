using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.Delegates
{
    public class DelegatesAndEvents
    {
        public delegate string CalcDelegate(int num1, int num2);

        public string Add(int num1, int num2)
        {
            return "Add Result " + (num1 + num2);
        }

        public string Sub(int num1, int num2)
        {
            return "Sub Result " + (num1 - num2);
        }

        public string Mul(int num1, int num2)
        {
            return "Mul Result " + (num1 * num2);
        }
    }

    public class DelegatesWithCovariants
    {
        //public delegate string ObjectTypeParamDelegate(object obj1, object obj2);

        //public delegate string StringTypeParamDelegate(string str1, string str2);

        public delegate object DelegateReturnObjType();

        public delegate string DelegateReturnStrType();

        public delegate void DelegateStrParamType(string str1);

        public string GetCurrentDateTimeString()
        {
            return DateTime.Now.ToString();
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;

        }

        public void PrintObject(object obj1)
        {
            Console.WriteLine(Convert.ToString(obj1));
            //Console.WriteLine(Convert.ToDateTime(obj1));
        }
        public string ConCatToUpperCase(string str1, string str2)
        {
            return "Cocatenated " + (str1 + str2).ToUpper();
        }

        public string ConCatToUpperCaseObjectParam(object str1, object str2)
        {
            return "Cocatenated Object " + (str1.ToString() + str2.ToString()).ToUpper();
        }
    }

    public class GenericDelegates
    {
        /*
         There are three types of generic delegates: don't require to define the delegate instance in order to invoke the methods.
Action
Func
Predicate

         * */

        #region
        //Encapsulates a method that has a single parameter and does not return a value.In other words,
        //  Action generic delegate, points to a method that takes up to 16 Parameters and returns void.


        public void CallActionDelegate()
        {
            Action<string> actionDel = new Action<string>(PrintStringWithOneParam);
            actionDel += PrintHelloWithOneParam;
            actionDel("Buddy");

            Action<string, string> actionDel2 = new Action<string, string>(PrintStringWithTwoParam);
            actionDel2("Hi", "Buddy");


            //Action without Params
            Action actWithOutParam = PrintTime;
        }

        #endregion

        #region 
        /*
         *Encapsulates a method that has no parameters and returns a value of the type. 
         * In other words, The generic Func delegate is used when we want to point to a method that returns a value. 
         * Always remember that the final parameter of Func<> is always the return value of the method. 
         * For examle Func <int, int, string>
         */

        public void CallFuncDelegate()
        {
            Func<int, int, int, string> FuncDel = new Func<int, int, int, string>(GetRegistrationAmount);
            Console.WriteLine("Registration Amount : {0}", FuncDel(1500, 140, 8));

            //Func with object return type
            Func<object> funcDelObjType = RetrunAndPrintHello;
        }
        #endregion


        #region
        //The Predicate delegate defines a method that can be called on arguments and always returns Boolean type result.
        public void CallPredicateDelegate()
        {
            Predicate<string> predDel = new Predicate<string>(IsNullEmpty);
            Console.WriteLine(" '' empty or not {0}", predDel(""));
            Console.WriteLine(" 'Arun' empty or not {0}", predDel("Arun"));
            predDel += AtleastHavingFourChar;//multicast
            Console.WriteLine(" 'Arun' having atleast 4 char or not {0}", predDel("Arun"));

            Console.WriteLine(" 'SK' having atleast 4 char or not {0}", predDel("SK"));
        }
        #endregion

        public object RetrunAndPrintHello()
        {
            Console.WriteLine("RetrunAndPrintHello string is Hello: ");
            object obj = DateTime.Now;
            return obj;
        }

        public void PrintTime()
        {
            Console.WriteLine("PrintTime " + DateTime.Now);
        }
        void PrintHelloWithOneParam(string str1)
        {
            Console.WriteLine("PrintHelloWithOneParam string is Hello: {0}", str1);
        }
        void PrintStringWithOneParam(string str1)
        {
            Console.WriteLine("PrintStringWithOneParam string is : {0}", str1);
        }

        void PrintStringWithTwoParam(string str1, string str2)
        {
            Console.WriteLine("PrintStringWithTwoParam string 1 is : {0} string 2 is {1}", str1, str2);
        }

        public string GetRegistrationAmount(int size, int sqftPrice, int stampDutyPercent)
        {
            return "Stamp Amount " + (size * sqftPrice) * (stampDutyPercent / 100);
        }


        public bool IsNullEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public bool AtleastHavingFourChar(string str1)
        {
            return str1.Length >= 4;
        }

    }

    //Covarients and Controvarients using delegates
    public abstract class Shape
    {
        public virtual double Area { get { return 0; } }
    }

    public class Circle : Shape
    {
        private double r;
        public Circle(double radius)
        {
            r = radius;
        }

        public double Radius { get { return r; } }

        public override double Area
        {
            get
            {
                return Math.PI * r * r;
            }
        }
    }

    public class ShapeAreaComparer : IComparer<Shape>
    {
        int IComparer<Shape>.Compare(Shape x, Shape y)
        {
            if (x == null) return y == null ? 0 : -1;
            return y == null ? 1 : x.Area.CompareTo(y.Area);
        }
    }
}
