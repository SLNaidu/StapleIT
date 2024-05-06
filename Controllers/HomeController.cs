using StapleIT.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StapleIT.DAL;
using StapleIT.Models;
using System.Data;
using System.Dynamic;
using System.Reflection;

namespace StapleIT.Controllers
{
    public class HomeController : Controller
    {
        private StapleITContext _context;
        public HomeController(StapleITContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Items = _context.Item.ToList().OrderBy(a=>a.ItemName);
            mymodel.Lists = _context.List.ToList().OrderBy(a => a.ListName);
            return View(mymodel);
        }

        public async Task<IActionResult> AddtoList(int? id, int ListId)
        {
            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            var List = await _context.List
                .FirstOrDefaultAsync(m => m.ListId == ListId);
            if (item == null || List == null)
            {
                return NotFound();
            }
            var ListDetail = new ListDetail { Quantity =  1};
            var viewModel = new AddList
            {
                Item = item,
                Lists = List,
                Details= ListDetail
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddtoList(AddList addList)
        {
            int ListId = addList.Lists.ListId;
            int ItemId = addList.Item.ItemId;
            int quantity = addList.Details.Quantity;
            var listdetails = new ListDetail
            {
                Quantity = quantity,
                ListId= ListId,
                ItemId = ItemId
            };

            _context.ListDetail.Add(listdetails);
            _context.SaveChanges();
            TempData["msg"] = "Item Added to List..!";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Search()
        {
            var model = new ItemSearchViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(ItemSearchViewModel model)
        {
            var results = _context.Item.Where(pa =>
                 pa.ItemName.ToLower().Contains(model.Items.ItemName.ToLower
                    ())).ToList();

            if (results.Count < 1)
            {
             
                model.SearchError = "No Items found with the search criteria provided";

            }
            model.ResultList = results;
            model.rList = _context.List.ToList();
            return View(model);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Item.SingleOrDefault(m => m.ItemId == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var item = _context.Item.SingleOrDefault(m => m.ItemId == id);
            _context.Item.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
