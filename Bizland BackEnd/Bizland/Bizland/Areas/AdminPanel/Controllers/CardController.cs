using Bizland.DAL;
using Bizland.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bizland.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CardController : Controller
    {
        private AppDbContext _context { get;  }
        public IEnumerable<Card> cards { get; set; }
        public CardController(AppDbContext context)
        {
            _context = context;
            cards = _context.Cards.Where(c => !c.IsDeleted).ToList();

        }
        public IActionResult Index()
        {
            return View(cards);
        }
        public async Task< IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();

            }
            var cardDb = _context.Cards.Where(c => !c.IsDeleted).FirstOrDefault(c=>c.Id==Id);
            if (cardDb==null)
            {
                return NotFound();
            }
          cardDb.IsDeleted = true;
          await  _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var IsExist = _context.Cards.Where(c => !c.IsDeleted).Any(c => c.Job == card.Job);
            if (IsExist)
            {
                ModelState.AddModelError("Job",$"{card.Job} already exist!");
                return View();
            }
            Card newCard = new Card
            {
                Name = card.Name,
                Job = card.Job
            };
           await _context.AddAsync(newCard);
           await _context.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? Id)
        {
            if (Id==null)
            {
                return BadRequest();

            }
            var cardDb = _context.Cards.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == Id);
            if (cardDb==null)
            {
                return NotFound();
            }
            return View(cardDb);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? Id, Card card)
        {

            if (Id == null)
            {
                return BadRequest();

            }
            var cardDb = _context.Cards.Where(c => !c.IsDeleted).FirstOrDefault(c => c.Id == Id);
            if (cardDb == null)
            {
                return NotFound();
            }
            if (card.Job.ToLower()==cardDb.Job.ToLower())
            {
                return RedirectToAction(nameof(Index));
            }
            var IsExist = cards.Any(c => c.Job.ToLower() == cardDb.Job.ToLower());
            if (IsExist)
            {
                ModelState.AddModelError("Job", $"{cardDb.Job} is already exist!");
                return View();
            }
            card.Job = cardDb.Job;
            card.Name = cardDb.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

    }
}
