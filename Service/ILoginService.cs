namespace DotNetAssignment.Service
{
    public interface ILoginService
    {
        Task<Response<string>> GenerateToken(LoginModel loginModel);
    }
}
