using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwewyPinsApi.Models;

namespace TwewyPinsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PinsController : ControllerBase
    {
        private readonly PinsContext _context;

        public PinsController(PinsContext context)
        {
            _context = context;
        }

        // GET: api/Pins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PinsDTO>>> GetPinItems()
        {
            return await _context.PinItems
                .Select(x => PinToDTO(x))
                .ToListAsync();
            //using PinToDTO to map the results

            //return await _context.PinItems.ToListAsync();
        }

        // GET: api/Pins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PinsDTO>> GetPins(long id)
        {
            var pins = await _context.PinItems.FindAsync(id);

            if (pins == null)
            {
                return NotFound();
            }

            return PinToDTO(pins);
        }

        // PUT: api/Pins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPins(long id, Pins pins)
        {
            if (id != pins.Id)
            {
                return BadRequest();
            }

            _context.Entry(pins).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PinsExists(id))
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

        // POST: api/Pins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Pins>> PostPins(Pins pins)
        //{
        //    _context.PinItems.Add(pins);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetPins), new { id = pins.Id }, pins);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, PinsDTO pinsDTO)
        {
            if (id != pinsDTO.Id)
            {
                return BadRequest();
            }

            var pinItem = await _context.PinItems.FindAsync(id);
            if (pinItem == null)
            {
                return NotFound();
            }

            pinItem.Mutation = pinsDTO.Mutation;
            pinItem.Info = pinsDTO.Info;
            pinItem.User = pinsDTO.User;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PinsExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PinsDTO>> CreatePin(PinsDTO pinsDTO)
        {
            var pin = new Pins
            {
                Mutation = pinsDTO.Mutation,
                Info = pinsDTO.Info,
                User = pinsDTO.User
            };

            _context.PinItems.Add(pin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPinItems),
                new { id = pin.Id },
                PinToDTO(pin));
        }

        // DELETE: api/Pins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePins(long id)
        {
            var pins = await _context.PinItems.FindAsync(id);
            if (pins == null)
            {
                return NotFound();
            }

            _context.PinItems.Remove(pins);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool PinsExists(long id)
        //{
        //    return _context.PinItems.Any(e => e.Id == id);
        //}
        private bool PinsExists(long id) =>
            _context.PinItems.Any(e => e.Id == id);

        private static PinsDTO PinToDTO(Pins pins) =>
            new PinsDTO
            {
                Id = pins.Id,
                Info = pins.Info,
                Mutation = pins.Mutation,
                User = pins.User
            };
    }
}
