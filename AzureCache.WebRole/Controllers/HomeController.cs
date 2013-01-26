using AzureCache.WebRole.Models.HomeModels;
using Microsoft.ApplicationServer.Caching;
using System.Web.Mvc;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AzureCache.WebRole.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.RoleInstanceId = RoleEnvironment.CurrentRoleInstance.Id;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new IndexModel());
        }

        [HttpPost]
        public ActionResult Index(
            IndexModel indexModel,
            string button)
        {
            var dataCache = new DataCache("default");

            if (button.Equals("set"))
            { 
                dataCache.Put(indexModel.Name, indexModel.Value);
            }
            else
            {
                var value = dataCache.Get(indexModel.Name);
                if (value != null)
                {
                    ModelState.Remove("Value");
                    indexModel.Value = (string)value;
                }
            }

            return View(indexModel);
        }
    }
}
