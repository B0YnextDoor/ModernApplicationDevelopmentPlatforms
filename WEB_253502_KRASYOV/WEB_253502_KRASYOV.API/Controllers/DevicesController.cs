using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253502_KRASYOV.API.Data;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.API.Services.DeviceService;

namespace WEB_253502_KRASYOV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: api/Devices
        [HttpGet]
		[Route("{category?}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<List<Device>>>> GetDevices(string? category, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _deviceService.GetDeviceListAsync(category, pageNo, pageSize));
        }

        // GET: api/Devices/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<Device>>> GetDevice(int id)
        {
            var response = await _deviceService.GetDeviceByIdAsync(id);

            if (!response.Successfull) return Problem(response.ErrorMessage);

            return Ok(response);
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult> PutDevice(int id, Device device)
        {
            await _deviceService.UpdateDeviceAsync(id, device);
            return Ok();
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult<ResponseData<Device>>> PostDevice(Device device)
        {
            var response = await _deviceService.CreateDeviceAsync(device);
			if (!response.Successfull) return Problem(response.ErrorMessage);
            return Ok(response);
		}

        // DELETE: api/Devices/5
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return Ok();

        }

    }
}
