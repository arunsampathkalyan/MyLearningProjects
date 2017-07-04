using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.Inheritance
{
    public class InheritanceConcept
    {
    }

    public abstract class AbstractClass
    {
        public void ShowMyName()
        {
            Console.WriteLine("AbstractClass Concrete Method");
        }

        public abstract void AbstractMethod();
    }

    public class BaseOne
    {
        public BaseOne()
        {
            Console.WriteLine("BaseOne Constructor Called.");
        }

        public BaseOne(string str)
        {
            Console.WriteLine("BaseOne Constructor with string param {0}", str);
        }
        public virtual void PrintMethodName()
        {
            Console.WriteLine("PrintMethodName method from BaseOne");
        }

        public virtual void PrintName(string str)
        {
            Console.WriteLine("PrintName method from BaseOne and string is : {0}", str);
        }

        public virtual void F() { Console.WriteLine("BaseOne.F"); }
    }

    public class ChildOne : BaseOne
    {
        public ChildOne()
        {
            Console.WriteLine("ChildOne Constructor Called.");
        }

        public override void PrintMethodName()
        {
            Console.WriteLine("PrintMethodName method from ChildOne");
        }

        public new void PrintName(string str)
        {
            //base.PrintName("base str");
            Console.WriteLine("PrintName method from ChildOne and string is : {0}", str);
        }

        sealed public override void F() { Console.WriteLine("ChildOne.F"); }//method sealed so child can't inherit
    }

    public class GChildOne : ChildOne
    {
        // Attempting to override F causes compiler error CS0239.

        // Overriding F2 is allowed.
        public new void PrintName(string str)
        {
            //base.PrintName("base str");
            Console.WriteLine("PrintName method from GChildOne and string is : {0}", str);
        }

        public new void PrintName23(string str)
        {
            //base.PrintName("base str");
            Console.WriteLine("PrintName23 method from GChildOne and string is : {0}", str);
        }
    }

    public class ChildTwo : AbstractClass
    {
        public override void AbstractMethod()
        {
            Console.WriteLine("ChildTwo AbstractMethod Implementation");
        }
    }

    public class ChildTheree : AbstractClass
    {
        public override void AbstractMethod()
        {
            base.ShowMyName();
            Console.WriteLine("ChildThree AbstractMethod Implementation");
        }
    }

    public class ClassWithPvtContructor
    {
        private ClassWithPvtContructor(string a)
        {

        }

        public void NormalMethod()
        {
            Console.WriteLine("ClassWithPvtContructor NormalMethod");
        }
    }

    public class ChildFour: MyClass
    {
        
        public void SomeMethod()
        {
            MyClass mcl = new Inheritance.MyClass();
        }
    }

    public class MyClass
    {
        private MyClass(object data1, string data2) { }

        public MyClass(object data1) : this(data1, null) { }

        public MyClass(string data2) : this(null, data2) { }

        public MyClass() : this(null, null) { }
    }


    public struct StructOne
    {
        public int Number { get; set; }
    }
    public class TestClass : ITestOne, ITestTwo
    {
        public int Number { get; set; }
        public void Test()
        {
            Console.WriteLine("Test Method impl in Child");
        }

        void ITestOne.Hello()
        {
            Console.WriteLine("Hello from TestOne");
        }

        void ITestTwo.Hello()
        {
            Console.WriteLine("Hello from TestTwo");
        }

        public void Hello()
        {
            Console.WriteLine("Hello from TestClass - Public");
        }
    }

}
