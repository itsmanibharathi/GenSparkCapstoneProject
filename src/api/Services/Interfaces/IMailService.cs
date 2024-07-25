namespace api.Services.Interfaces
{
    public interface IMailService
    {
        public Task<bool> Send(string to, string subject, string body);
    }
}
