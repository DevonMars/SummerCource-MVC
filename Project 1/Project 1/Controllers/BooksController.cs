using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _repository;

        public BooksController(ApplicationDbContext repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Books.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Book book)
        {
            if(ModelState.IsValid)
            {
                _repository.Add(book);
                await _repository.SaveChangesAsync();
                return RedirectToAction("Index");
                    
            }

            return View(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
        }
    }
}