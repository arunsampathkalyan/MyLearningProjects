using eTrading.Core.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Serialization;
using UI.Support.ContentProviders;


namespace ConsoleApplication1
{
    public class FileInformation
    {
        public string controlName { get; set; }

        public string controllabel { get; set; }

        public List<string> controlValue { get; set; }

        public int control_Id { get; set; }
    }
    class Program
    {

        public static void SaveTableOrdering(string customer, int lob)
        {
            try
            {
               
                lookupAbiService.LookupServiceClient LookupClient = new lookupAbiService.LookupServiceClient();

                var homeTypeList = LookupClient.GetList("Home", lookupAbiService.LineOfBusiness.PrivateMotorcar, "52d41cbc-5f5c-4d0a-a110-ace4994fd421", "en-GB", null, null, false);



                ListOrdering listOrdering = new ConsoleApplication1.ListOrdering();
                listOrdering.Ordering = new List<Ordering>();
                int order = 0;
                foreach (var homeItem in homeTypeList)
                {
                    order += 1;
                    listOrdering.Ordering.Add(new Ordering() { Code = homeItem.Code, Order = Convert.ToString(order) });
                }

                listOrdering.TableName = "Home";

                using (var repository = new LookupRepository())
                {
                    var lookupOrdering =
                        (from context in repository.Ordering
                         where context.Customer.Equals(customer)
                         select context.Properties).FirstOrDefault();

                    if (lookupOrdering == null)
                    {
                        // Customer has no record in table
                        var orderingConfiguration = new OrderingConfiguration.LookupOrderingConfiguration();
                        var lookup = new OrderingConfiguration.Lookup
                        {
                            LineOfBusiness = lob,
                            ListName = listOrdering.TableName,
                            Locale = "en-GB",
                            TableName = string.Empty,
                            Items = new OrderingConfiguration.Item[0]
                        };

                        var lookupList = new List<OrderingConfiguration.Lookup> { lookup };

                        orderingConfiguration.Lookups = lookupList.ToArray();
                        lookupOrdering = Xml.Serialize<OrderingConfiguration.LookupOrderingConfiguration>(orderingConfiguration);

                        repository.Ordering.Add(new Orderings
                        {
                            Customer = customer,
                            Properties = lookupOrdering
                        });
                        repository.SaveChanges();
                    }


                    var lookupOrderingConfiguration = Xml.Deserialize<OrderingConfiguration.LookupOrderingConfiguration>(lookupOrdering);

                    var items = new List<OrderingConfiguration.Item>();
                    foreach (var value in listOrdering.Ordering)
                    {
                        items.Add(new OrderingConfiguration.Item
                        {
                            Text = value.Code,
                            Order = int.Parse(value.Order)
                        });
                    }

                    var orderConfiguration =
                        lookupOrderingConfiguration.Lookups.FirstOrDefault(
                            x =>
                                x.ListName.Equals(listOrdering.TableName) && x.LineOfBusiness == lob &&
                                x.Locale.Equals("en-GB"));

                    if (orderConfiguration != null)
                    {
                        if (orderConfiguration.Items == null)
                            orderConfiguration.Items = new List<OrderingConfiguration.Item>().ToArray();

                        orderConfiguration.Items = items.ToArray();
                    }
                    else
                    {
                        orderConfiguration = new OrderingConfiguration.Lookup
                        {
                            LineOfBusiness = lob,
                            Items = items.ToArray(),
                            ListName = listOrdering.TableName,
                            TableName = string.Empty,
                            Locale = "en-GB"
                        };

                        var lookups = lookupOrderingConfiguration.Lookups.ToList();
                        lookups.Add(orderConfiguration);
                        lookupOrderingConfiguration.Lookups = lookups.ToArray();
                    }

                    var properties = Xml.Serialize<OrderingConfiguration.LookupOrderingConfiguration>(lookupOrderingConfiguration);
                    var query = (from context in repository.Ordering
                                 where context.Customer.Equals(customer)
                                 select context).FirstOrDefault();

                    if (query != null)
                    {
                        query.Properties = properties;
                        repository.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        static void Main(string[] args)
        {


            try
            {

                System.Data.DataSet dataSet = new System.Data.DataSet();
                dataSet.ReadXml(AppDomain.CurrentDomain.BaseDirectory.ToString()+ "..\\..\\XmlFileInfo.xml", XmlReadMode.InferSchema);
                IList<FileInformation> nodeFile = new List<FileInformation>();

                foreach (var obj in dataSet.Tables["control"].Rows)
                {
                    FileInformation fileInfo = new FileInformation();
                    DataRow row = obj as DataRow;
                    fileInfo.controlName = row.Field<string>("controlname");
                    fileInfo.controllabel = row.Field<string>("controllable");
                    fileInfo.control_Id = row.Field<int>("control_Id");
                    foreach (var innerobj in dataSet.Tables["value"].Rows)
                    {
                        DataRow innerrow = innerobj as DataRow;
                        if (fileInfo.control_Id == innerrow.Field<int>("controlvalue_Id"))
                        {
                            if (fileInfo.controlValue == null)
                                fileInfo.controlValue = new List<string>();

                            fileInfo.controlValue.Add(innerrow.Field<string>("value_Text"));
                        }
                    }
                    nodeFile.Add(fileInfo);
                }

                //var valss =from vvvv in ( from dVRow in dataSet.Tables["value"].AsEnumerable()
                //           group dVRow by dVRow.Field<int>("controlvalue_Id"))
                //           select new
                //           {
                //               controlValId = vvvv.Key,
                //               values= (from v in vvvv select v)
                //           }

                var controlValues = from dataVal in (from valRow in (from vrow in dataSet.Tables["value"].AsEnumerable()
                                                  select new
                                                  {
                                                      controlValId = vrow.Field<int>("controlvalue_Id"),
                                                      controlvalue = vrow.Field<string>("value_Text")
                                                  })
                                      group valRow by valRow.controlValId)
                           select new
                           {
                               controlValId = dataVal.Key,
                               values = (from item in dataVal select item.controlvalue).ToList()
                           };



                var controlDS =

                                 from dcRow in dataSet.Tables["control"].AsEnumerable()
                                 join ctlval in controlValues
                                 on dcRow.Field<int>("control_Id") equals ctlval.controlValId
                                 select new FileInformation
                                 {
                                     control_Id = ctlval.controlValId,
                                     controlName = dcRow.Field<string>("controlname"),
                                     controllabel = dcRow.Field<string>("controllable"),
                                     controlValue = ctlval.values
                                 };


                //group dVRow by dVRow.Field<int>("controlvalue_Id") into vssdfd
                //select new
                //{
                //    controlValId = vssdfd.Key,
                //    values = vssdfd.ToList()
                //};





                //var controlDS = 

                //                 from dcRow in dataSet.Tables["control"].AsEnumerable()
                //                 join valRow in valss
                //                 on dcRow.Field<int>("control_Id") equals valRow.Key
                //                 select new FileInformation
                //                 {
                //                     control_Id = dcRow.Field<int>("control_Id"),
                //                     controlName = dcRow.Field<string>("controlname"),
                //                     controllabel = dcRow.Field<string>("controllable")

                //                 };
                //var controlDS = (

                //                 from dcRow in dataSet.Tables["control"].AsEnumerable()
                //                 join valRow in (from dVRow in dataSet.Tables["value"].AsEnumerable()
                //                                 group dVRow by dVRow.Field<int>("controlvalue_Id")
                //                                 select new {
                //                                     controlValId = dVRow.Field<int>("controlvalue_Id"),
                //                                     values = dVRow.Field<int>("value_Text")
                //                                 }).to 

                //                 on  dcRow.Field<int>("control_Id")  equals valRow.
                //                 group dVRow by dVRow.Field<int>("controlvalue_Id") into cv

                //                 select new FileInformation
                //                 {
                //                     control_Id = dcRow.Field<int>("control_Id"),
                //                     controlName = dcRow.Field<string>("controlname"),
                //                     controllabel = dcRow.Field<string>("controllable")
                //                 }).ToList();
                //                 ).ToList();
                //select new FileInformation
                //{
                //    control_Id = dcRow.Field<int>("control_Id"),
                //    controlName = dcRow.Field<string>("controlname"),
                //    controllabel = dcRow.Field<string>("controllable")
                //}).ToList();





                ServiceReference1.LookupServiceClient client = new ServiceReference1.LookupServiceClient();
                var homeTypeListLocal = client.GetList("Home", ServiceReference1.LineOfBusiness.PrivateMotorcar, "52d41cbc-5f5c-4d0a-a110-ace4994fd421", "en-GB", null, null, false);



                lookupAbiService.LookupServiceClient LookupClient = new lookupAbiService.LookupServiceClient();

                var homeTypeList = LookupClient.GetList("Home", lookupAbiService.LineOfBusiness.PrivateMotorcar, "52d41cbc-5f5c-4d0a-a110-ace4994fd421", "en-GB", null, null, false);


                SaveTableOrdering("SSP", 1);
            }
            catch (Exception ex)
            {


            }



            string destinationConnString = "";
            string sourceConnString = "data source=ASPIRE1461\\SQLEXPRESS;initial catalog=SchoolContext;Integrated Security=SSPI;";
            string targetCustomer = "";
            string sourceCustomer = "";
            string locale = "";
            string lineOfBusiness = "";

            try
            {
                SqlConnection conn = new SqlConnection(sourceConnString);
                SqlCommand cmd = new SqlCommand(@"select * FROM [SSP].[EmailConfiguration]", conn);
                //SqlCommand cmd = new SqlCommand(@"select * FROM [SSP].[EmailTemplates]", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    //EmailTemplate(dr["Name"].ToString(), dr["Template"].ToString(), dr["Customer"].ToString(), dr["Locale"].ToString());
                    EmailConfiguration(dr["Properties"].ToString(), dr["Customer"].ToString());
                }
            }
            catch (Exception ex)
            {


            }







            Console.Read();


        }
        public static string EmailConfiguration(string properties, string customerName)
        {
            using (
                var connection =
                    new SqlConnection("data source=10.80.9.148;initial catalog=SSP.eTrading.eSuite.Config;persist security info=True;user id=SSP.eTrading.ConfigUser;password=!N3wN3wW3b;MultipleActiveResultSets=True"))
            {
                string sql =
                    string.Format(@"UPDATE [SSP].[EmailConfiguration] 
                    SET  Properties = '{0}'
                    WHERE Customer ='{1}'", properties, customerName);


                connection.Open();

                var command = new SqlCommand(sql, connection);
                var result = command.ExecuteNonQuery();

                if (result > 0)
                    return string.Empty;

                //sql =
                //    string.Format(@"INSERT INTO [SSP].[EmailConfiguration](Template, Customer, Name, Locale)
                //    VALUES ('{0}', '{1}', '{2}', '{3}')", template.Replace("'", "''"), customerName, fileName, locale);

                //command = new SqlCommand(sql, connection);
                //command.ExecuteNonQuery();

                return string.Empty;
            }
        }

        public static string EmailTemplate(string fileName, string template, string customerName, string locale)
        {
            using (
                var connection =
                    new SqlConnection("data source=10.80.9.148;initial catalog=SSP.eTrading.eSuite.Config;persist security info=True;user id=SSP.eTrading.ConfigUser;password=!N3wN3wW3b;MultipleActiveResultSets=True"))
            {
                string sql =
                    string.Format(@"UPDATE [SSP].[EmailTemplates] 
                    SET Template = '{0}'
                    WHERE Customer ='{2}'
                    AND Name = '{1}'
                    AND Locale = '{3}'", template.Replace("'", "''"), fileName, customerName, locale);

                connection.Open();

                var command = new SqlCommand(sql, connection);
                var result = command.ExecuteNonQuery();

                if (result > 0)
                    return string.Empty;

                sql =
                    string.Format(@"INSERT INTO [SSP].[EmailTemplates](Template, Customer, Name, Locale)
                    VALUES ('{0}', '{1}', '{2}', '{3}')", template.Replace("'", "''"), customerName, fileName, locale);

                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                return template;
            }
        }
    }

    public class OrderingConfiguration
    {
        [Serializable()]
        [XmlRoot("LookupOrdering")]
        public class LookupOrderingConfiguration
        {
            [XmlElement("Lookup")]
            public Lookup[] Lookups
            {
                set;
                get;
            }
        }

        [Serializable()]
        public class Lookup
        {
            [XmlAttribute(AttributeName = "lineOfBusiness")]
            public int LineOfBusiness { set; get; }

            [XmlAttribute(AttributeName = "name")]
            public string ListName { set; get; }

            [XmlAttribute(AttributeName = "tableName")]
            public string TableName { set; get; }

            [XmlAttribute(AttributeName = "locale")]
            public string Locale { set; get; }

            [XmlElement("Item")]
            public Item[] Items
            {
                set;
                get;
            }
        }

        [Serializable()]
        public class Item
        {
            [XmlElement]
            public string Text
            {
                set;
                get;
            }

            [XmlElement]
            public int Order
            {
                set;
                get;
            }
        }
    }

    public class ListOrdering
    {
        public string TableName { get; set; }
        public List<Ordering> Ordering { get; set; }
    }

    public class Ordering
    {
        public string Code { get; set; }
        public string Order { get; set; }
    }
}
