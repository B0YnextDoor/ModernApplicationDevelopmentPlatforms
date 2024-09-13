namespace WEB_253502_KRASYOV.UI.Services.Authentication
{
    public interface IAuthService
    {
        Task<(bool Result, string ErrorMessage)> RegisterUserAsync(string email,string password,
                                                                IFormFile? avatar);
    }
}
