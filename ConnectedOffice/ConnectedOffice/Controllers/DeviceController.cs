using ConnectedOffice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectedOffice.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeviceController : ControllerBase
	{
		private readonly masterContext _context;

		public DeviceController(masterContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
		{
			if (_context.Devices == null)
			{
				return NotFound();
			}
			return await _context.Devices.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Device>> GetDevice(Guid id)
		{
			if (_context.Devices == null)
			{
				return NotFound();
			}
			var device = await _context.Devices.FindAsync(id);

			if (device == null)
			{
				return NotFound();
			}

			return device;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutDevice(Guid id, Category category)
		{
			if (id != category.CategoryId)
			{
				return BadRequest();
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DeviceExists(id))
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
		public async Task<ActionResult<Device>> PostDevice(Device device)
		{
			if (_context.Devices == null)
			{
				return Problem("Entity set 'masterContext.Devices'  is null.");
			}
			_context.Devices.Add(device);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (DeviceExists(device.DeviceId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDevice(Guid id)
		{
			if (_context.Devices == null)
			{
				return NotFound();
			}
			var device = await _context.Devices.FindAsync(id);
			if (device == null)
			{
				return NotFound();
			}

			_context.Devices.Remove(device);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool DeviceExists(Guid id)
		{
			return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
		}
	}
}
