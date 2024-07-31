namespace api.Services.Interfaces
{
    public interface IUserVerifyService
    {
        public Task<string> VertifyUserAsync(int id, string token);
    }
}
