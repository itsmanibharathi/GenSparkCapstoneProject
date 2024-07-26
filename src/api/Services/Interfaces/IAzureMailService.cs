namespace api.Services.Interfaces
{
    public interface IAzureMailService
    {
        public Task<bool> Send(string to, string subject, string body);
    }
}
