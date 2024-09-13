using Microsoft.EntityFrameworkCore;
using WEB_253502_KRASYOV.API.Data;
using WEB_253502_KRASYOV.Domain.Entities;
using WEB_253502_KRASYOV.Domain.Models;

namespace WEB_253502_KRASYOV.API.Services.DeviceService
{
	public class DeviceService : IDeviceService
	{
		private readonly int _maxPageSize = 20;
		private readonly AppDbContext _context;
		public DeviceService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseData<ListModel<Device>>> GetDeviceListAsync(string? categoryNormalizedName, 
											int pageNo = 1, int pageSize = 3)
		{
			if (pageSize > _maxPageSize) pageSize = _maxPageSize;
			var query = _context.Devices.AsQueryable();
			var dataList = new ListModel<Device>();
			query = query.Where(d => 
				categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));
			// количество элементов в списке
			var count = await query.CountAsync();
			if (count == 0)
			{
				return ResponseData<ListModel<Device>>.Success(dataList);
			}
			// количество страниц
			int totalPages = (int)Math.Ceiling(count / (double)pageSize);
			if (pageNo > totalPages)
				return ResponseData<ListModel<Device>>.Error("No such page");
			dataList.Items = await query
				.OrderBy(d => d.Id)
				.Skip((pageNo - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			dataList.CurrentPage = pageNo;
			dataList.TotalPages = totalPages;
			return ResponseData<ListModel<Device>>.Success(dataList);
		}

		public async Task<ResponseData<Device>> CreateDeviceAsync(Device product)
		{
			var newDevice = await _context.Devices.AddAsync(product);
			await _context.SaveChangesAsync();
			return ResponseData<Device>.Success(newDevice.Entity);
		}

		public async Task DeleteDeviceAsync(int id)
		{
			var device = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
			if (device is null) return;
			_context.Entry(device).State = EntityState.Deleted;
			await _context.SaveChangesAsync();
		}

		public async Task<ResponseData<Device>> GetDeviceByIdAsync(int id)
		{
			var device = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
			if (device is null) return ResponseData<Device>.Error($"No such object with id : {id}");
			return ResponseData<Device>.Success(device);
		}


		public async Task UpdateDeviceAsync(int id, Device product)
		{
			var device = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
			if (device is null) return;
			device.Name = product.Name;
			device.Description = product.Description;
			device.Category = product.Category;
			device.CategoryId = product.CategoryId;
			device.Price = product.Price;
			device.Image = product.Image;
			_context.Entry(device).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
		{
			throw new NotImplementedException();
		}

	}
}
