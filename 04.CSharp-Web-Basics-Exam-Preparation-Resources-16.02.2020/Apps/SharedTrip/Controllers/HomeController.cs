namespace SharedTrip.Controllers
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        //default:localhost/Home/Index
        [HttpGet("/")]

        /*NOTE: If the user is logged in and he tries to go the home page,
         * 
        the application must redirect him to the /Trips/All*/
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Trips/All");
            }

            return this.View();
        }
    }
}
