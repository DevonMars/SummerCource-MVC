using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public async Task<IActionResult> Details (int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var book = await _repository.Books.SingleOrDefaultAsync(m => m.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if( id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(book);
                await _repository.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _repository.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var book = await _repository.Books.SingleOrDefaultAsync(m => m.Id == id);
            _repository.Books.Remove(book);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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