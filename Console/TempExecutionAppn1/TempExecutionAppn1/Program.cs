using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempExecutionAppn1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Number-StringFormats
            //     https://msdn.microsoft.com/en-us/library/dwhawy9k.aspx

            double number = .2468913;
            Console.WriteLine("one digit after decimal point " + number.ToString("P1", CultureInfo.InvariantCulture));
            // Displays 24.7 %

            Console.WriteLine("two digit after decimal point " + number.ToString("P2", CultureInfo.InvariantCulture));
            // Displays 24.69 %
            Console.WriteLine(number.ToString("R",
                                               CultureInfo.InvariantCulture));
            Console.WriteLine(number.ToString("#.##"));
            // Displays .25

            Console.WriteLine(number.ToString("#.00%", CultureInfo.CreateSpecificCulture("en-US")));

            //Console.WriteLine(string.Format("{0.00}%", (((double)number) * 100)));

            Console.WriteLine(string.Format("{0:#,0.###%}", (((double)number))));

            Console.WriteLine(string.Format("{0:#,0.###%}", .25));

            Console.WriteLine(string.Format("{0:#,0.##%}", 2.51));

            Console.WriteLine(string.Format("{0:#,0.##%}", .025678));
            Console.WriteLine(string.Format("%{0:#,0.##}", .025678));

            Console.WriteLine(string.Format("{0:#,#.##%}", .002567));

            Console.WriteLine(string.Format("{0:#,0.##}%", 02.5678));

            //            output

            //                one digit after decimal point 24.7 %
            //two digit after decimal point 24.69 %
            //0.2468913
            //.25
            //24.69%
            //24.689%
            //25%
            //251%
            //2.5%

            #endregion

            Console.WriteLine((new StringAppendConverter()).Convert(.0256784654, null, "Percentage", null).ToString());
            Console.WriteLine((new StringAppendConverter()).Convert(.0256784654, null, "ActualPercentage", null).ToString());

            int num1 = 50;

            bool abc = num1 == 50;

            string destinationConnString = "";
            string sourceConnString = "data source=ASPIRE1461\\SQLEXPRESS;initial catalog=SchoolContext;Integrated Security=SSPI;";
            string targetCustomer = "";
            string sourceCustomer = "";
            string locale = "";
            string lineOfBusiness = "";

            try
            {
                SqlConnection conn = new SqlConnection(sourceConnString);
                SqlCommand cmd = new SqlCommand(@"select * FROM [SSP].[EmailTemplates]", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                   //
                }
            }
            catch (Exception ex)
            {

                
            }
            




            using (var connection = new SqlConnection(sourceConnString))
            {
                connection.Open();

                var selStatement = string.Format(@"select * FROM [SSP].[EmailTemplates]"
                                                      );
                using (var deleteCommand = new SqlCommand(selStatement, connection))
                    deleteCommand.ExecuteNonQuery();


            }


            using (var connection = new SqlConnection(destinationConnString))
            {
                connection.Open();

                var deleteStatement = string.Format(@"DELETE FROM [SSP].[EmailTemplates]
                                                      WHERE Customer = '{0}'
                                                      AND   Locale = '{1}'
                                                      AND   LineOfBusiness = '{2}'", targetCustomer, locale, lineOfBusiness);
                using (var deleteCommand = new SqlCommand(deleteStatement, connection))
                    deleteCommand.ExecuteNonQuery();
            }

            using (var connection = new SqlConnection(sourceConnString))
            {

                var insertStatement = string.Format(@"INSERT INTO [SSP].[EmailTemplates](Name, Customer, Template, Locale, LineOfBusiness)
                                                      SELECT Name, '{3}', Template, Locale, LineOfBusiness
                                                      FROM [SSP].[EmailTemplates]
                                                      WHERE Customer = '{0}'
                                                      AND   Locale = '{1}'
                                                      AND   LineOfBusiness = '{2}'", sourceCustomer, locale, lineOfBusiness, targetCustomer);
                using (var insertCommand = new SqlCommand(insertStatement, connection))
                    insertCommand.ExecuteNonQuery();
            }




            Console.Read();
        }
    }


    public class StringAppendConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (parameter != null)
            {
                string stringFormat = "{0:#,0.##}%";
                switch (parameter.ToString())
                {
                    case "Percentage":
                        {
                            double tempValue = (((double)value) < 1) ? (((double)value) * 100) : ((double)value);


                            //if (Utility.GetLang() == "ar")
                            //{
                            //    if (((double)value) < 1)
                            //        return string.Format("%{0:#,0.##}", (((double)value) * 100));
                            //    return string.Format("%{0:#,0.##}", ((double)value));
                            //}
                            //else
                            //{
                            //    if (((double)value) < 1)
                            //        return string.Format("{0:#,0.##}%", (((double)value) * 100));
                            //    return string.Format("{0:#,0.##}%", ((double)value));
                            //}

                            //string.Format("{0:#,0.##%}" - Internally multiplies to 100

                            return string.Format(stringFormat, tempValue);
                        }

                    case "ActualPercentage":
                        {
                            //if (Utility.GetLang() == "ar")
                            //    return string.Format("%{0}", ((double)value));
                            //else
                            //    return string.Format("{0}%", ((double)value));

                            return string.Format(stringFormat, ((double)value));
                        }

                }
                return value.ToString() + parameter.ToString();
            }

            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
