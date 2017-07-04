using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.Inheritance
{
    interface InterfaceBaseOne
    {
        int Num { get; set; }

        int Method();
    }

    interface ITestTwo
    {
        void Hello();
        void Test();
    }

    interface ITestOne
    {
        void Hello();
        void Test();
    }
}
