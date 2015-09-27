using System.Linq;
using System.Web.Mvc;
using SVTrade.Abstract;
using SVTrade.Models;

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

        public ViewResult EditUserGroup(int id)
        {
            var userGroup = repository.UserGroups.FirstOrDefault(p => p.userGroupID == id);
            return View(userGroup);
        }

        [HttpPost]
        public ActionResult EditUserGroup(UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                repository.SaveUserGroup(userGroup);
                TempData["message"] = string.Format("Должность {0} была сохранена!", userGroup.name);
                return RedirectToAction("UserGroupsList");
            }
            else
            {
                return View(userGroup);
            }
        }

        public ViewResult CreateUserGroup()
        {
            return View("EditUserGroup", new UserGroup());
        }

        [HttpPost]
        public ActionResult DeleteUserGroup(int id)
        {
            var deleted = repository.DeleteUserGroup(id);
            if (deleted != null)
            {
                TempData["message"] = string.Format("Должность {0} была удалена!", deleted.name);
            }
            return RedirectToAction("UserGroupsList");
        }
    }
}