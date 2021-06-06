using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemittanceWebApp.Data;
using RemittanceWebApp.Helpers;
using RemittanceWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RemittanceWebApp.Controllers
{
    public class CardRequestController : Controller
    {
        private readonly RemittanceDbContext _context;

        public CardRequestController(RemittanceDbContext context)
        {
            _context = context;
        }

        // GET: CardRequest
        public async Task<IActionResult> Index()
        {
            ViewBag.Channels = Helper.GetChannelList();
            var list = await _context.CardRequestLog.ToListAsync<CardRequestLog>();
            DateTime today = new DateTime();
            list = list.Where(x => x.LogDate.Date == today.Date).ToList();
            return View(list);
        }

        public async Task<IActionResult> CardRequestSearch(string userId, string channelId, string profileNo, string RefNo, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var list = await _context.CardRequestLog.ToListAsync<CardRequestLog>();
                if (list.Count > 0)
                {
                    if (!string.IsNullOrEmpty(userId))
                        list = list.Where(x => x.UserId == userId).ToList();
                    if (!string.IsNullOrEmpty(channelId))
                        list = list.Where(x => x.ChannelId == channelId).ToList();
                    if (!string.IsNullOrEmpty(profileNo))
                        list = list.Where(x => x.ProfileNumber == profileNo).ToList();
                    if (!string.IsNullOrEmpty(RefNo))
                        list = list.Where(x => x.Ref_No == RefNo).ToList();
                    if (!string.IsNullOrEmpty(fromDate.ToString("dd/MM/yyyy")) && !string.IsNullOrEmpty(toDate.ToString("dd/MM/yyyy")))
                        list = list.Where(x => x.LogDate.Date >= fromDate.Date && x.LogDate.Date <= toDate.Date).ToList();
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: CardRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardRequestLog = await _context.CardRequestLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardRequestLog == null)
            {
                return NotFound();
            }

            return View(cardRequestLog);
        }

        // GET: CardRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CardRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] CardRequestLog cardRequestLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardRequestLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cardRequestLog);
        }

        // GET: CardRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardRequestLog = await _context.CardRequestLog.FindAsync(id);
            if (cardRequestLog == null)
            {
                return NotFound();
            }
            return View(cardRequestLog);
        }

        // POST: CardRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LogDate,LogTime,FiscalYear,Ref_No,UserId,ChannelId,ProfileNumber,Request,Response")] CardRequestLog cardRequestLog)
        {
            if (id != cardRequestLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardRequestLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardRequestLogExists(cardRequestLog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cardRequestLog);
        }

        // GET: CardRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardRequestLog = await _context.CardRequestLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardRequestLog == null)
            {
                return NotFound();
            }

            return View(cardRequestLog);
        }

        // POST: CardRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardRequestLog = await _context.CardRequestLog.FindAsync(id);
            _context.CardRequestLog.Remove(cardRequestLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardRequestLogExists(int id)
        {
            return _context.CardRequestLog.Any(e => e.Id == id);
        }
    }
}
