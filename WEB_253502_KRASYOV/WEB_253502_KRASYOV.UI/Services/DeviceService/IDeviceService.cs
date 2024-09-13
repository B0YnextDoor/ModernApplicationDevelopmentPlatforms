using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.UI.Services.DeviceService
{
	public interface IDeviceService
	{
		public Task<ResponseData<ListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, int pageNo = 1);
		public Task<ResponseData<Device>> GetDeviceByIdAsync(int id);
		public Task UpdateDeviceAsync(int id, Device product, IFormFile? formFile);
		public Task DeleteDeviceAsync(int id);
		public Task<ResponseData<Device>> CreateDeviceAsync(Device product, IFormFile? formFile);


	}
}
