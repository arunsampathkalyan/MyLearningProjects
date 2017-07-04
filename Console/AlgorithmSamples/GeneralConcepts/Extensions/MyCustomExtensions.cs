using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.Extensions
{
    //https://msdn.microsoft.com/en-IN/library/bb383977.aspx
    public interface IMyInterfaceEx
    {
        // Any class that implements IMyInterfaceEx must define a method
        // that matches the following signature.
        void Describe();
    }
    public static class MyCustomExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?', '-', '/' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static List<string> GetEmailList(this string str, char[] splitChars)
        {
            return str.Split(splitChars).ToList();
        }

        // The following extension methods can be accessed by instances of any 
        // class that implements IMyInterface.        

        public static void Print(this IMyInterfaceEx myInterfaceEx, int num)
        {
            Console.WriteLine("ExtensionMethod - MyCustomExtensions.Describe(this IMyInterfaceEx myInterfaceEx, int num)");
        }

        public static void Print(this IMyInterfaceEx myInterfaceEx, string str)
        {
            Console.WriteLine("ExtensionMethod - MyCustomExtensions.Describe(this IMyInterfaceEx myInterfaceEx, string str)");
        }

        // This method is never called in program, because each 
        // of the three classes A, B, and C implements a method named MethodB
        // that has a matching signature.
        public static void Describe(this IMyInterfaceEx myInterfaceEx)
        {
            Console.WriteLine("ExtensionMethod - MyCustomExtensions.Describe(this IMyInterfaceEx myInterfaceEx)");
        }
    }

    // Define three classes that implement IMyInterface, and then use them to test
    // the extension methods.
    public class A : IMyInterfaceEx
    {
        public void Describe()
        {
            Console.WriteLine("A.Describe()");
        }

    }

    public class B : IMyInterfaceEx
    {
        public void Describe()
        {
            Console.WriteLine("B.Describe()");
        }

        public void Print(int num)
        {
            Console.WriteLine("B.Print(int num)");
        }
    }

    public class C : IMyInterfaceEx
    {
        public void Describe()
        {
            Console.WriteLine("C.Describe()");
        }

        public void Print(object obj)
        {
            Console.WriteLine("C.Print(object obj)");
        }
    }

}
