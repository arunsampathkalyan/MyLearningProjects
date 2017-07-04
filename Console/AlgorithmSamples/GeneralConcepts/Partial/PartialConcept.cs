using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.Partial
{
    public partial class PartialClassA
    {
        private int x, y;
        public PartialClassA(int X, int Y)
        {
            this.x = X;
            this.y = Y;
        }

        //Partial Methods

        /*
         A partial method declaration consists of two parts: the definition, and the implementation.
         These may be in separate parts of a partial class, or in the same part. 
         If there is no implementation declaration, then the compiler optimizes away both the defining declaration and all calls to the method.

        Partial method declarations must begin with the contextual keyword partial and the method must return void.
        Partial methods can have ref but not out parameters.
        Partial methods are implicitly private, and therefore they cannot be virtual.
        Partial methods cannot be extern, because the presence of the body determines whether they are defining or implementing.
        Partial methods can have static and unsafe modifiers.
        Partial methods can be generic. Constraints are put on the defining partial method declaration, and may optionally be repeated on the implementing one. Parameter and type parameter names do not have to be the same in the implementing declaration as in the defining one.
        You can make a delegate to a partial method that has been defined and implemented, but not to a partial method that has only been defined.

         */

        // Method declared here, Compiler omits this if no implementation found.
        partial void PartialMethod();
    }

    public partial class PartialClassA
    {
        public PartialClassA()
        {
            this.x = 10; this.y = 20;
        }


        public void PrintValues()
        {
            Console.WriteLine("Values : {0}, {1}", x, y);
            PartialMethod();//calling partial method.
        }

        partial void PartialMethod()
        {
            Console.WriteLine("Partial Method implementation in another part.");//partial methods are private.
        }
    }

    //Partial Interfaces

    public partial interface IInterFaceOne
    {
        void InterFace_Test();
    }

    public partial interface IInterFaceOne
    {
        void InterFace_Test2();
    }

    public partial struct StructOne
    {
        void Struct_Test() { }
    }
    public partial struct StructOne
    {
        void Struct_Test2() { }
    }


}
