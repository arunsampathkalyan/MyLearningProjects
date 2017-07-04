using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel VM = new Controllers.HomeViewModel();
            VM.DOB = DateTime.Now.Date;
            try
            {


                lookupAbiService.LookupServiceClient LookupClient = new lookupAbiService.LookupServiceClient();

                var homeTypeList = LookupClient.GetList("TypeOfDwelling", lookupAbiService.LineOfBusiness.PrivateMotorcar, "52d41cbc-5f5c-4d0a-a110-ace4994fd421", "en-GB", null, null, false);


                
            }
            catch (Exception ex)
            {


            }

            return View(VM);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class HomeViewModel
    {
        public DateTime DOB { get; set; }
    }
}