using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.API.Services.DeviceService
{
	public interface IDeviceService
	{
		public Task<ResponseData<ListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3);
		public Task<ResponseData<Device>> GetDeviceByIdAsync(int id);
		public Task UpdateDeviceAsync(int id, Device product);
		public Task DeleteDeviceAsync(int id);
		public Task<ResponseData<Device>> CreateDeviceAsync(Device product);
		public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
	}
}
