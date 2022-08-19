using ToDoList.Models;
using ToDoList.Models.DTOs;
using ToDoList.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private IToDoService toDoService;
        private UserManager<User> userManager;
        public ToDoController(IToDoService toDoService, UserManager<User> userManager)
        {
            this.toDoService = toDoService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<ToDoDTO> toDoes = toDoService.GetAll();

            return View(toDoes);
        }
        public async Task<IActionResult> UserToDoes()
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(Index));
            }
            List<ToDoDTO> toDoes = toDoService.GetUserToDoes(user.Id);
            return View(toDoes);
        }
        public IActionResult Details(int id)
        {
            ToDoDTO toDo = toDoService.GetDtoById(id);
            return View(toDo);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            //ToDoDTO toDoDTO = toDoService.GetDtoById(id);
            ToDo toDo = toDoService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (toDo.UserId != user.Id)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(toDoService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, ToDo toDo)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            if (toDoService.GetById(id).UserId != user.Id)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            toDoService.Edit(toDo);
            return RedirectToAction(nameof(UserToDoes));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ToDo toDo)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user is null)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            toDoService.Create(toDo, user);
            return RedirectToAction(nameof(UserToDoes));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            ToDo toDo = toDoService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            if (toDo.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            return View(toDo);
        }
        public async Task<IActionResult> ConfirmDeleteAsync(int id)
        {
            User user = await userManager.GetUserAsync(User).ConfigureAwait(false);
            ToDo toDo = toDoService.GetById(id);
            if (user is null)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            if (toDo.UserId != user.Id)
            {
                return RedirectToAction(nameof(UserToDoes));
            }
            toDoService.Delete(id);
            return RedirectToAction(nameof(UserToDoes));
        }
        [HttpGet]
        public IActionResult ViewToDo(int id)
        {
            return View(toDoService.GetById(id));
        }
    }
}
