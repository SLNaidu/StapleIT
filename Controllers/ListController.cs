using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using StapleIT.DAL;
using StapleIT.Models;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace StapleIT.Controllers
{
    public class ListController : Controller
    {
        private StapleITContext _context;
        public ListController(StapleITContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View(_context.List.ToList().OrderByDescending(a => a.CreatedDateTime));
        }

        //View
        public IActionResult CreateList()
        {
            ViewData["UserGroupId"] = new SelectList(_context.UserGroup, "UserGroupId", "UserGroupId");
            return View();
        }

        [HttpPost] //Creating
        public IActionResult Create(List list)
        {
            if (list.ListName == null)
            {
                TempData["msg"] = "Please input a name";
                return RedirectToAction("CreateList");
            }
            else
            {


                //Remember that this shit breaks when the List name is above 15 characters.
                //Hey, I didn't make that limit. I'm just following.
                List list1 = list;
                list1.CreatedDateTime = DateTime.Now;
                list1.ListName = list.ListName;
                list1.UserGroupId = list.UserGroupId;
                _context.Add(list);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //recieves id, matches it to something in the DB and then returns the view with the info to be edited
        public IActionResult Edit(int id)
        {
            var list = _context.List.SingleOrDefault(m => m.ListId == id);
            ViewData["UserGroupId"] = new SelectList(_context.UserGroup, "UserGroupId", "UserGroupId");
            return View(list);
        }

        [HttpPost] //Updating changes
        public IActionResult Edit(List list)
        {
            if (list.ListName == null)
            {
                return RedirectToAction("Edit");
            }
            else 
            { 
                //Remember that this shit breaks when the List name is above 15 characters.
                //Hey, I didn't make that limit. I'm just following.
                _context.Update(list);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            var list = _context.List.SingleOrDefault(m => m.ListId == id);
            return View(list);
        }

        //Actual deletion
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var list = _context.List.SingleOrDefault(m => m.ListId == id);
            _context.List.Remove(list);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Listdetails(int? Id)
        {
            List<ListDetail> Details = _context.ListDetail.ToList();

            var result = from ld in Details.Where(v => v.ListId == Id)
                         join i in _context.Item on ld.ItemId equals i.ItemId
                         join l in _context.List on ld.ListId equals l.ListId
                         select new ListDetailViewModel { Itemname = i.ItemName, ListName = l.ListName, Quantity = ld.Quantity, ListDetailID=ld.ListDetailId };

            return View(result);
        }

        public IActionResult DetailsDelete(int id)
        {
            var details = _context.ListDetail.SingleOrDefault(v => v.ListDetailId == id);
            return View(details);
        }
        
        public IActionResult DetailsDeleteConfirm(int ListDetailId)
        {
            var details = _context.ListDetail.SingleOrDefault(v => v.ListDetailId == ListDetailId);
            _context.ListDetail.Remove(details);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
