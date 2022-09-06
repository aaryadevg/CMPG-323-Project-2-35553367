using ConnectedOffice.Auth;
using ConnectedOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectedOffice.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ZoneController : ControllerBase
	{
		private readonly masterContext _context;

		public ZoneController(masterContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Zone>>> GetZone()
		{
			if (_context.Devices == null)
			{
				return NotFound();
			}
			return await _context.Zones.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Zone>> GetZone(Guid id)
		{
			if (_context.Zones == null)
			{
				return NotFound();
			}
			var zone = await _context.Zones.FindAsync(id);

			if (zone == null)
			{
				return NotFound();
			}

			return zone;
		}

		[HttpGet("{id}/Devices")]
		public async Task<ActionResult<IEnumerable<Device>>> GetDevicesInZone(Guid id)
		{
			if (_context.Devices == null)
			{
				return NotFound();
			}

			return await _context.Devices.Where(device => device.ZoneId == id).ToListAsync();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutZone(Guid id, Zone zone)
		{
			if (id != zone.ZoneId)
			{
				return BadRequest();
			}

			_context.Entry(zone).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ZoneExists(id))
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


		[HttpPost]
		public async Task<ActionResult<Zone>> PostZone(Zone zone)
		{
			if (_context.Zones == null)
			{
				return Problem("Entity set 'masterContext.Zones'  is null.");
			}
			_context.Zones.Add(zone);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (ZoneExists(zone.ZoneId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetZone", new { id = zone.ZoneId}, zone);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = Roles.ADMIN)]
		public async Task<IActionResult> DeleteZone(Guid id)
		{
			if (_context.Zones == null)
			{
				return NotFound();
			}
			var zone = await _context.Zones.FindAsync(id);
			if (zone == null)
			{
				return NotFound();
			}

			_context.Zones.Remove(zone);
			await _context.SaveChangesAsync();

			return NoContent();
		}



		private bool ZoneExists(Guid id)
		{
			return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
		}
	}
}

