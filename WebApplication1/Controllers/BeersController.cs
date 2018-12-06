using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeerProduction.OPC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerProduction.OPC;

namespace WebApplication1.Models
{
    public class BeersController : Controller
    {
        private readonly WebApplication1Context _context;

        public BeersController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Beers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Beer.ToListAsync());
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer
                .FirstOrDefaultAsync(m => m.id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Start,End,Good,Bad,Speed")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }

        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return View(beer);
        }

        // POST: Beers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Start,End,Good,Bad,Speed")] Beer beer)
        {
            if (id != beer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.id))
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
            return View(beer);
        }

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer
                .FirstOrDefaultAsync(m => m.id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _context.Beer.FindAsync(id);
            _context.Beer.Remove(beer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
            return _context.Beer.Any(e => e.id == id);
        }

        public void OptimizeAll()
        {

        }

        public void PilsnerOptimize()
        {
            for (int i = 1; i <= 10; i++)
            {
                Pilsner b = new Pilsner();
                b.Speed = (i / 10) * Pilsner.MaxSpeed;

                OpcStart.Instance.SetCntrlCmd(Buttons.ABORT);
                OpcStart.Instance.SetCmdChangeRequest(true);

                System.Threading.SpinWait.SpinUntil(() => OpcStart.State == States.Aborted);
                OpcStart.Instance.SetCntrlCmd(Buttons.CLEAR);
                OpcStart.Instance.SetCmdChangeRequest(true);

                System.Threading.SpinWait.SpinUntil(() => OpcStart.State == States.Stopped);
                OpcStart.Instance.SetCntrlCmd(Buttons.RESET);
                OpcStart.Instance.SetCmdChangeRequest(true);

                System.Threading.SpinWait.SpinUntil(() => OpcStart.State == States.Idle);

                OpcStart.Instance.SetNextProductAmount(100);
                OpcStart.Instance.SetNextProductID(Pilsner.BeerOpcCmd);
                OpcStart.Instance.SetMachSpeed(b.Speed);

                System.Threading.SpinWait.SpinUntil(() => OpcStart.machinespeed == b.Speed && OpcStart.nextProductAmount == 100 && OpcStart.nextProductID == Pilsner.BeerOpcCmd);
                OpcStart.Instance.SetCntrlCmd(Buttons.START);
                OpcStart.Instance.SetCmdChangeRequest(true);

            }
        }

    }
}
