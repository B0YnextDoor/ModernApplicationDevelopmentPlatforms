using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using WEB_253502_KRASYOV.Domain.Models;
using WEB_253502_KRASYOV.UI.HelperClasses;
using WEB_253502_KRASYOV.UI.Models;
using WEB_253502_KRASYOV.UI.Services.Authentication;
using WEB_253502_KRASYOV.UI.Services.CartService;
using WEB_253502_KRASYOV.UI.Services.CategoryService;
using WEB_253502_KRASYOV.UI.Services.DeviceService;
using WEB_253502_KRASYOV.UI.Services.FileService;

namespace WEB_253502_KRASYOV.UI.Extensions
{
	public static class HostingExtensions
	{
		public static void RegisterCustomMemoryServices(this WebApplicationBuilder builder) {
			builder.Services.AddScoped<ICategoryService, MemoryCategoryService>()
				.AddScoped<IDeviceService, MemoryDeviceService>();
		}

		public static void RegisterCustomServices(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<ICategoryService, ApiCategoryService>()
                .AddScoped<IDeviceService, ApiDeviceService>()
                .AddScoped<IFileService, ApiFileService>();
			builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));
            builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession();
            builder.Services.AddScoped<Cart>(SessionCart.GetCart);
		}

		public static void RegisterHttpClient(this WebApplicationBuilder builder) {
            builder.Services.AddHttpContextAccessor();
            var uriData = builder.Services.BuildServiceProvider()
				.GetRequiredService<IOptions<UriData>>().Value;
            builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>("api", options =>
				options.BaseAddress = new Uri(uriData.ApiUri));
			builder.Services.AddHttpClient<IDeviceService, ApiDeviceService>("api", options =>
				options.BaseAddress = new Uri(uriData.ApiUri));
            builder.Services.AddHttpClient<IFileService, ApiFileService>("fileapi", options =>
                options.BaseAddress = new Uri($"{uriData.ApiUri}Files"));
            builder.Services.AddHttpClient<ITokenAccessor, KeycloakTokenAccessor>();
            builder.Services.AddHttpClient<IAuthService, KeycloakAuthService>();
        }

		public static void AddAuthentication(this WebApplicationBuilder builder) {
            var keycloakData = builder.Configuration.GetSection("Keycloak").Get<KeycloakData>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie().AddJwtBearer().AddOpenIdConnect(options =>
            {
                options.Authority = $"{keycloakData.Host}/auth/realms/{keycloakData.Realm}";
                options.ClientId = keycloakData.ClientId;
                options.ClientSecret = keycloakData.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.Scope.Add("openid");
                options.SaveTokens = true;
                options.RequireHttpsMetadata = false;
                options.MetadataAddress =
                    $"{keycloakData.Host}/realms/{keycloakData.Realm}/.well-known/openid-configuration";
            });
		}

        public static void AddLogging(this WebApplicationBuilder builder) {
			builder.Host.UseSerilog((context, configuration) =>
			{
				configuration.ReadFrom.Configuration(context.Configuration);
			});
		}
    }
}
