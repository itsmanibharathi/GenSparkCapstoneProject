namespace api.Services.Interfaces
{
    public interface IUserVerifyService
    {
        public Task<string> VertifyUser(int id, string token);
    }
}
