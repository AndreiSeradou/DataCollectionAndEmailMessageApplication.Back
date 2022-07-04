namespace OmegaSoftware.TestProject.BL.Domain.Interfaces.Services
{
    public interface IEmailSenderService
    {
        public Task Send(string email, string content);
    }
}
