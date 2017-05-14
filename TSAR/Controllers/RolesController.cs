using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TSAR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSAR.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> UserManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        RolesLogic obj = new RolesLogic();

        [Authorize(Roles = "Admin")]
        // GET: Roles
        public ActionResult Index()
        {
            //  var roles = context.Roles.ToList();
            return View(roleManager.Roles.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, RolesView role)
        {
            if (roleManager.RoleExists(role.RoleName))
            {
                ViewBag.Message = "Role Already Exists";
            }
            else
            {

                try
                {
                    db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                    {
                        Name = collection["RoleName"]
                    });
                    db.SaveChanges();
                    ViewBag.ResultMessage = "Role created successfully !";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();

        }
        public ActionResult Delete(string RoleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            db.Roles.Remove(thisRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]

        //Add User To A Role
        public ActionResult AnotherUserToRole(string Hey = null, string MyUser = null)
        {
            //Get All Roles Into A List
            IEnumerable<SelectListItem> items = db.Roles.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name

            });

            //Get All Users Into List
            IEnumerable<SelectListItem> items2 = db.UserTables.Select(c => new SelectListItem
            {
                Value = c.Username,
                Text = c.Username

            });






            //Send List To ViewBag DrooDownList
            ViewBag.Hey = items;
            ViewBag.MyUser = items2;

            //Add User To Role
            if (Hey != null && MyUser != null)
            {
                if (obj.AddUserToRoleTwo(Hey, MyUser))
                {
                    ViewBag.Message = "User added to Role";
                }
                else
                {
                    ViewBag.Message = "User Not Added, Please try again";
                }

            }


            return View();
        }


        //Remove A User From A Role
        public ActionResult RemoveUserFromRole(string Hey = null, string MyUser = null)
        {

            //Get All Roles into List
            IEnumerable<SelectListItem> items = db.Roles.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name

            });

            //Get All Users Into List
            IEnumerable<SelectListItem> items2 = db.UserTables.Select(c => new SelectListItem
            {
                Value = c.Username,
                Text = c.Username

            });

            //Send To View
            ViewBag.Hey = items;
            ViewBag.MyUser = items2;

            ApplicationUser user = UserManger.Users.ToList().Find(x => x.UserName.Equals(MyUser));

            //Remove User From Role
            if (Hey != null && MyUser != null)
            {
                // obj.RemoveUserFromRole(Hey, MyUser)
                if (UserManger.RemoveFromRoleAsync(user.UserName, Hey).Equals(true))
                {

                    ViewBag.Message = "User Has Been Removed From " + Hey.ToString();

                }
                else
                {
                    ViewBag.Message = "User Has Not Been Removed From " + Hey.ToString();
                }

            }


            return View();





        }


    }
}