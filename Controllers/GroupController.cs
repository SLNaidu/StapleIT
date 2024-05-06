using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using StapleIT.DAL;
using StapleIT.Models;

namespace StapleIT.Controllers
{
    public class GroupController : Controller
    {
        private StapleITContext _context;
        public GroupController(StapleITContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View(_context.Group.ToList());
        }
        public IActionResult CreateGroup()
        {

            return View();
        }
        public IActionResult Create(Group group)
        {
            _context.Group.Add(group);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Groupdetails(int? Id)
        {
            List<Group> Groups = _context.Group.ToList();
            var group = Groups.Where(v => v.GroupId == Id).FirstOrDefault();
            List<UserGroup> userGroup = _context.UserGroup.ToList();

            var result = from ld in userGroup.Where(v => v.GroupId == Id)
                         join i in _context.User on ld.UserId equals i.UserId
                         join l in _context.Group on ld.GroupId equals l.GroupId
                         select new GroupDetailViewModel { Fname = i.FName, Lname = i.LName, groupName=l.GroupName, Email = i.EmailAdd, groupId=l.GroupId};
            if(result.ToList().Count == 0)
            {
                result = from ld in _context.Group.Where(v => v.GroupId == Id)
                         select new GroupDetailViewModel { groupId = ld.GroupId, groupName = ld.GroupName };
            }

            //if (result.ToList().Count == 0)
            //{
            //    result = result.Any() ? result : new List<GroupDetailViewModel> { new GroupDetailViewModel { groupName = group.GroupName, groupId = group.GroupId } };
            //}
            return View(result);
        }
        public IActionResult AddUser(int Id)
        {

            var viewModel = new UserListViewModel
            {
                groupId = Id,
                Users = _context.User.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(int userId, int groupId)
        {
            UserGroup userGroup = new UserGroup();
            userGroup.GroupId = groupId;
            userGroup.UserId = userId;
            _context.UserGroup.Add(userGroup);
            _context.SaveChanges();
            Console.WriteLine("test");

            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteUserGroup(int Id) 
        {
            var viewModel = new UserListViewModel
            {
                groupId = Id,
                Users = _context.User.ToList()
            };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserGroup(int userId, int groupId)
        {
           var userGroup = _context.UserGroup.Where(m=>m.UserId == userId && m.GroupId == groupId).FirstOrDefault();
            if (userGroup != null)
            {
                _context.UserGroup.Remove(userGroup);
                _context.SaveChanges();
                Console.WriteLine("test");
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
