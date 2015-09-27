using System.Web.Mvc;
using SVTrade.Abstract;

namespace SVTrade.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/
        private IRepository repository;
        

        

        public AdminController(IRepository repo)
        {
            repository = repo;
        }
    
        public ViewResult UserGroupsList()
        {
            return View(repository.UserGroups);
        }
	}
}