using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralConcepts.General
{
    static public class ValueAndRefTypes
    {
        //https://msdn.microsoft.com/en-us/library/14akc2c7.aspx
        public static void ChangeByReference(ref Product itemRef)
        {
            // The following line changes the address that is stored in  
            // parameter itemRef. Because itemRef is a ref parameter, the
            // address that is stored in variable item in Main also is changed.
            //itemRef = new Product("Stapler", 99999);

            // You can change the value of one of the properties of
            // itemRef. The change happens to item in Main as well.
            itemRef.ItemID = 12345;
        }

        public static void ChangeValues(Product itemRef)
        {
            // The following line changes the address that is stored in  
            // parameter itemRef. Because itemRef is a ref parameter, the
            // address that is stored in variable item in Main also is changed.

            //itemRef = new Product("Kepler", 22121);

            // You can change the value of one of the properties of
            // itemRef. The change happens to item in Main as well.
            itemRef.ItemName = itemRef.ItemName + " 1";
            itemRef.ItemID = 11221;
        }
    }

    public class Product
    {
        public Product(string name, int newID)
        {
            ItemName = name;
            ItemID = newID;
        }

        public string ItemName { get; set; }
        public int ItemID { get; set; }
    }
}
