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
    public class ItemsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //public ItemsController(ApplicationDbContext context)
        public ItemsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Items
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        public async Task<IActionResult> GetItems()
        {
            //return await _context.Items.ToListAsync();
            var items = await _unitOfWork.Items.GetAll();
            return Ok(items);
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Item>> GetItem(int id)
        public async Task<IActionResult> GetItem(int id)
        {
            //var Item = await _context.Items.FindAsync(id);
            var Item = await _unitOfWork.Items.Get(q => q.ItemID == id);

            if (Item == null)
            {
                return NotFound();
            }

            return Ok(Item);
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item Item)
        {
            if (id != Item.ItemID)
            {
                return BadRequest();
            }

            //_context.Entry(Item).State = EntityState.Modified;
            _unitOfWork.Items.Update(Item);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ItemExists(id))
                if (!await ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item Item)
        {
            //_context.Items.Add(Item);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Items.Insert(Item);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetItem", new { id = Item.ItemID }, Item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            //var Item = await _context.Items.FindAsync(id);
            var Item = await _unitOfWork.Items.Get(q => q.ItemID == id);
            if (Item == null)
            {
                return NotFound();
            }

            //_context.Items.Remove(Item);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Items.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ItemExists(int id)
        private async Task<bool> ItemExists(int id)
        {
            //return (_context.Items?.Any(e => e.ItemID == id)).GetValueOrDefault();
            var Item = await _unitOfWork.Items.Get(q => q.ItemID == id);
            return Item != null;
        }
    }
}
