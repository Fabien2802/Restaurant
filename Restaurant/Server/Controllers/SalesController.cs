using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Server.Data;
using Restaurant.Server.IRepository;
using Restaurant.Shared.Domain;

namespace Restaurant.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public SalesController(ApplicationDbContext context)
        public SalesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Sales
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        public async Task<IActionResult> GetSales()
        {
            //return await _context.Sales.ToListAsync();
            var sales = await _unitOfWork.Sales.GetAll(includes: q => q.Include(x => x.Orders));
            return Ok(sales);
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Sale>> GetSale(int id)
        public async Task<IActionResult> GetSale(int id)
        {
            //var Sale = await _context.Sales.FindAsync(id);
            var Sale = await _unitOfWork.Sales.Get(q => q.SaleID == id);

            if (Sale == null)
            {
                return NotFound();
            }

            return Ok(Sale);
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale Sale)
        {
            if (id != Sale.SaleID)
            {
                return BadRequest();
            }

            //_context.Entry(Sale).State = EntityState.Modified;
            _unitOfWork.Sales.Update(Sale);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!SaleExists(id))
                if (!await SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale Sale)
        {
            //_context.Sales.Add(Sale);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Sales.Insert(Sale);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetSale", new { id = Sale.SaleID }, Sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            //var Sale = await _context.Sales.FindAsync(id);
            var Sale = await _unitOfWork.Sales.Get(q => q.SaleID == id);
            if (Sale == null)
            {
                return NotFound();
            }

            //_context.Sales.Remove(Sale);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Sales.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool SaleExists(int id)
        private async Task<bool> SaleExists(int id)
        {
            //return (_context.Sales?.Any(e => e.SaleID == id)).GetValueOrDefault();
            var Sale = await _unitOfWork.Sales.Get(q => q.SaleID == id);
            return Sale != null;
        }
    }
}
