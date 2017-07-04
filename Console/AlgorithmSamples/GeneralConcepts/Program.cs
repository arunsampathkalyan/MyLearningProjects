using GeneralConcepts.Async;
using GeneralConcepts.Extensions;
using GeneralConcepts.General;
using GeneralConcepts.Inheritance;
using GeneralConcepts.Lazy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GeneralConcepts
{

    class Program
    {

        class Player
        {
            public string Name { get; set; }
            public string Pid { get; set; }
        }
        static void Invoke(object s)
        {
            Console.WriteLine("Object Param Invoked");
        }

        static void Invoke<T>(params T[] vals)
        {
            Console.WriteLine("Params method invoked");
        }
        static void writeCollectionToXml(List<Player> playersList)
        {
            var xdoc = new XDocument(
                                                                new XElement
                                                                        ("PlayersInfo",


                                                                                        playersList.Select(x => new XElement("Player", new XAttribute("Name", x.Name), new XAttribute("PId", x.Pid)))

                                                                      )
                                                      );
            xdoc.Save(@"C:\Users\arunkumar.sampath\Desktop\CricketPlayersListWithPID.xml");
        }
        static void WriteXml<T>(T objectToDump)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (TextWriter dataWriter = new StreamWriter(@"C:\Users\arunkumar.sampath\Desktop\CricketPlayersListWithPID.xml"))
            {
                xmlSerializer.Serialize(dataWriter, objectToDump);
                dataWriter.Close();
            }
        }

        static int WordCountInString(string s)
        {
            string[] strArr = s.Split(new char[] { '.', '!', '?' });
            int maxLength = 0;
            foreach (var sentance in strArr)
            {
                if (sentance.WordCount() > maxLength)
                    maxLength = sentance.WordCount();
            }
            return maxLength;
        }

        static int FindSummerStartPosition(int[] arr)
        {
            int maxWinterTemp = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {

                if (arr[i] > maxWinterTemp)
                    return i;
            }
            return 0;
        }

        public static int MinimumTicketCost(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)

            if (A.Count() >= 23)
            {
                return 25;
            }
            else if (A.Count() <= 3)
            {
                return 2 * A.Length;
            }
            int cost = Math.Min(CheckWeeklyUsingRecursive(A, 1, A[0] + 6) + 7, CheckWeeklyUsingRecursive(A, 1, 0) + 2);
            return Math.Min(cost, 25);

        }

        public static int CheckWeeklyUsingRecursive(int[] A, int index, int maxPos)
        {
            if (index >= A.Count())
            {
                return 0;
            }
            if (A[index] <= maxPos)
            {
                return CheckWeeklyUsingRecursive(A, index + 1, maxPos);
            }
            else
            {
                return (Math.Min(CheckWeeklyUsingRecursive(A, index + 1, A[index] + 6) + 7, CheckWeeklyUsingRecursive(A, index + 1, 0) + 2));
            }
        }

        static void Main(string[] args)
        {
            //Asynchronous Programmming

            AsynchronousPrg.DoStuff();


            AsynchronousPrg.DoStuffAsync();

            //Lazy Initialisation

            Console.WriteLine(DateTime.Now.ToLongTimeString());
            var prdList = ProductItem.GetSampleProductList();
            Console.WriteLine("Total Products in product list is : " + prdList.Count);
            Console.WriteLine(DateTime.Now.ToLongTimeString());

            Console.WriteLine(DateTime.Now.ToLongTimeString());
            var prdListLazy = ProductItem.GetSampleProductListLazy();
            Console.WriteLine("Total Products in product list is : " + prdListLazy.Count);
            Console.WriteLine(DateTime.Now.ToLongTimeString());


            int[] A = new int[7];

            A[0] = 1;
            A[1] = 2;
            A[2] = 4;
            A[3] = 5;
            A[4] = 7;
            A[5] = 29;
            A[6] = 30;

            int cost = MinimumTicketCost(A);

            int[] arr1 = { -5, -5, -5, -42, 6, 12 };
            int[] arr2 = { 5, -2, 3, 8, 6 };
            int[] arr3 = { 3, 0, 2, 3, -5, -5, -5, -42, 3, 0, 3, 6, 12 };
            int summPos = FindSummerStartPosition(arr1);
            summPos = FindSummerStartPosition(arr2);
            summPos = FindSummerStartPosition(arr3);
            string S1 = "For cvs..save time. x x";
            string S2 = "we test coders. Give us a try?";

            int wCount = WordCountInString(S1);
            wCount = WordCountInString(S2);

            //Ref type value shared
            TestClass obj1 = new TestClass();
            obj1.Number = 5;
            TestClass obj2 = obj1;
            obj1.Number = 4;//both is 4

            //Val type seperate object created
            StructOne st1 = new Inheritance.StructOne();
            st1.Number = 4;
            StructOne st2 = st1;
            st1.Number = 13;//st1 is 13 and st2 is stil 4


            string s1 = "Abc";
            string s2 = "Abc";
            String s3 = new String(new char[] { 'A', 'b', 'c' });

            if (s1 == s2)
            {
                Console.WriteLine("String comparison with == is true");
            }
            if (s1.Equals(s2))
            {
                Console.WriteLine("String comparison with equals is true");
            }
            if (s1 == s3)
            {

            }
            if (s1.Equals(s3))
            {

            }
            TestClass tc = new TestClass();
            ITestOne tc1 = new TestClass();
            ITestTwo tc2 = new TestClass();

            tc.Test();
            tc1.Test();
            tc2.Test();

            tc.Hello();

            tc1.Hello();
            tc2.Hello();

            // Retrieve 10 lines from Somefile.txt, starting from line 1
            string filePath = @"C:\Users\arunkumar.sampath\Desktop\CricketPlayersListWithPID.txt";
            int startLine = 1;
            int lineCount = File.ReadLines(filePath).Count();
            var fileLines = System.IO.File.ReadAllLines(filePath)
                            //.Skip((startLine - 1))
                            .Take(lineCount).Where(i => !string.IsNullOrWhiteSpace(i) && !i.Equals("View Details Login to see & test Player Stats API!")).ToList();

            var pIds = fileLines.Where((j, k) => k % 2 != 0).ToList();
            var pNames = fileLines.Where((j, k) => k % 2 == 0).ToList();


            if (pIds.Count == pNames.Count)
            {
                List<Player> playersList = new List<Player>();
                for (int i = 0; i < pIds.Count; i++)
                {
                    playersList.Add(new Player() { Pid = pIds[i].Replace("PID to use with CricAPI: ", ""), Name = pNames[i] });
                }

                //writeCollectionToXml(playersList);
            }

            pIds = pIds.Select(str => str.Replace("PID to use with CricAPI: ", "")).ToList();
            var regNums = new List<string> { "FEO8NTG","FE08NTG", "ABC54ABC", "AB54ABCD","AB1ABC", "A1ABCD","ABCD123A","1ABC","ABC1","1234A","A1234",
                "1234AB",
                "AB1234",
                "123ABC",
                "ABC123",
                "ABC1234" };


            var ukNumPlateRegex1 = "(^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(^[A-Z][0-9]{1,3}[A-Z]{3}$)|(^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(^[0-9]{1,4}[A-Z]{1,2}$)|(^[0-9]{1,3}[A-Z]{1,3}$)|(^[A-Z]{1,2}[0-9]{1,4}$)|(^[A-Z]{1,3}[0-9]{1,3}$)|(^[A-Z]{1,3}[0-9]{1,4}$)";

            foreach (var regNm in regNums)
            {
                Console.WriteLine("Match {0} for {1}", Regex.IsMatch(regNm, ukNumPlateRegex1), regNm);
            }




            string ss = "string";
            Invoke(ss);

            object sss = "string";
            Invoke(sss);

            Invoke(ss, sss);

            Product item = new Product("Fasteners", 54321);
            System.Console.WriteLine("Original values in Main.  Name: {0}, ID: {1}\n",
                item.ItemName, item.ItemID);

            // Send item to ChangeByReference as a ref argument.
            ValueAndRefTypes.ChangeByReference(ref item);
            System.Console.WriteLine("Back in Main.  Name: {0}, ID: {1}\n",
                item.ItemName, item.ItemID);

            Product item1 = new Product("Fasteners", 45454);

            System.Console.WriteLine("Back in Main.  Name: {0}, ID: {1}\n",
                item1.ItemName, item1.ItemID);

            ValueAndRefTypes.ChangeValues(item1);

            System.Console.WriteLine("Name: {0}, ID: {1}\n",
                item1.ItemName, item1.ItemID);



            //DateTime dt = DateTime.Now.AddDays(89);
            //Console.WriteLine(dt);
            //Delegates
            int num1 = 5, num2 = 2;
            Delegates.DelegatesAndEvents obj = new Delegates.DelegatesAndEvents();
            Delegates.DelegatesAndEvents.CalcDelegate calcDelAddObj = new Delegates.DelegatesAndEvents.CalcDelegate(obj.Add);//assign method for delegate
            Console.WriteLine("calcDelAddObj {0}", calcDelAddObj(num1, num2));

            Delegates.DelegatesAndEvents.CalcDelegate calcDelSubObj = new Delegates.DelegatesAndEvents.CalcDelegate(obj.Sub);
            Console.WriteLine("calcDelSubObj {0}", calcDelSubObj(num2: num2, num1: num1));

            Delegates.DelegatesAndEvents.CalcDelegate calcDelMultipleObj = new Delegates.DelegatesAndEvents.CalcDelegate(obj.Add);
            calcDelMultipleObj += obj.Sub;//adding another method to call(multicastdelegate)
            Console.WriteLine("calcDelMultipleObj {0}", calcDelMultipleObj(num1, num2));

            Delegates.DelegatesAndEvents.CalcDelegate calcDelCompObj = calcDelAddObj + calcDelSubObj + obj.Mul;//adding two delegate objects(multicast) and adding another method to it
            Console.WriteLine("calcDelCompObj {0}", calcDelCompObj(num1, num2));

            calcDelMultipleObj -= obj.Add;//removing method from delegate
            Console.WriteLine("calcDelMultipleObj {0}", calcDelMultipleObj(num1, num2));

            calcDelCompObj -= obj.Mul;
            Console.WriteLine("calcDelCompObj {0}", calcDelCompObj(num1, num2));

            GeneralConcepts.Delegates.GenericDelegates genDel = new Delegates.GenericDelegates();
            genDel.CallActionDelegate();

            genDel.CallFuncDelegate();

            genDel.CallPredicateDelegate();

            Delegates.DelegatesWithCovariants objDelWithCovcls = new Delegates.DelegatesWithCovariants();
            //Delegates.DelegatesWithCovariants.ObjectTypeParamDelegate objTypeParam = new Delegates.DelegatesWithCovariants.ObjectTypeParamDelegate(objDelWithCovcls.ConCatToUpperCaseObjectParam);

            Delegates.DelegatesWithCovariants.DelegateReturnObjType delObjReturnType = new Delegates.DelegatesWithCovariants.DelegateReturnObjType(objDelWithCovcls.GetCurrentDateTimeString);
            object s = delObjReturnType();

            Delegates.DelegatesWithCovariants.DelegateStrParamType delStrPraType = new Delegates.DelegatesWithCovariants.DelegateStrParamType(objDelWithCovcls.PrintObject);
            delStrPraType("Arun");
            //Inheritance

            Inheritance.BaseOne b1 = new Inheritance.BaseOne();
            Inheritance.ChildOne c1 = new Inheritance.ChildOne();
            Inheritance.BaseOne bc1 = new Inheritance.ChildOne();

            Inheritance.GChildOne gc1 = new Inheritance.GChildOne();
            Inheritance.BaseOne bgc1 = new Inheritance.GChildOne();


            b1.PrintMethodName();
            c1.PrintMethodName();

            b1.PrintName("SK");
            c1.PrintName("Arun");

            bc1.PrintName("SK ARUN");//hides the method in childOne

            bc1.PrintMethodName();

            gc1.PrintName("grand child");
            bgc1.PrintName("base object - grant child");


            //Partial Classes and Methods

            Partial.PartialClassA parClsObj = new Partial.PartialClassA();

            parClsObj.PrintValues();

            Partial.PartialClassA parClsObj2 = new Partial.PartialClassA(5, 2);
            parClsObj2.PrintValues();


            //Extension Methods with Interfaces

            // Declare an instance of class A, class B, and class C.

            A a = new A();
            B b = new B();
            C c = new C();

            // For a, b, and c, call the following methods:
            //      -- Print with an int argument
            //      -- Print with a string argument
            //      -- Describe with no argument.

            // A contains no Print, so each call to Print resolves to 
            // the extension method that has a matching signature.
            a.Print(1);           // Extension.Print(object, int)
            a.Print("hello");     // Extension.Print(object, string)

            // A has a method that matches the signature of the following call
            // to Describe.
            a.Describe();            // A.Describe()

            // B has methods that match the signatures of the following
            // method calls.
            b.Print(1);           // B.Print(int)
            b.Describe();            // B.Describe()

            // B has no matching method for the following call, but 
            // class Extension does.
            b.Print("hello");     // Extension.Print(object, string)

            // C contains an instance method that matches each of the following
            // method calls.
            c.Print(1);           // C.Print(object)
            c.Print("hello");     // C.Print(object)
            c.Describe();            // C.Describe()

            //call string Extension Methods
            string str1 = "arun.sampathkalyan-Dev";
            string strEmail = "test1@test.com, test2@test.com; sample@test.com; test3@test.com, test4@test.com";

            Console.WriteLine("{0} word count : {1}", str1, str1.WordCount());

            Console.WriteLine("{0} email count : {1}", strEmail, strEmail.GetEmailList(new char[] { ',', ';' }).Count);

            SwitchCaseFallThrough(2);

            //Covarients and Controvarients using delegates
            SortedSet<Delegates.Circle> circlesByArea = new SortedSet<Delegates.Circle>(new Delegates.ShapeAreaComparer()) { new Delegates.Circle(7.5), new Delegates.Circle(100), null, new Delegates.Circle(0.01) };

            foreach (Delegates.Circle cir in circlesByArea)
            {
                Console.WriteLine(cir == null ? "null" : "Circle with area " + cir.Area);
            }

            Console.Read();
        }

        static void SwitchCaseFallThrough(int num)
        {
            //Falling through switch-cases can be achieved by having no code in a case (see case 0), 
            //or using the special goto case (see case 1) or goto default (see case 2) forms:
            //

            switch (num)
            {
                case 3:
                case 0:
                    goto case 1;
                case 1:
                    Console.WriteLine("case1");
                    break;
                case 2:
                    goto case 3;
                default:
                    break;
            }
        }
    }
}
