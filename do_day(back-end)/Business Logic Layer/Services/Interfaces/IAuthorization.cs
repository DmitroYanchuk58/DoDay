namespace Business_Logic_Layer.Services.Interfaces
{
    public interface IAuthorization
    {
        public Task Register(string username, string password, string email, string firstName, string lastName);

        public Task<bool> Login(string email, string password);
    }
}
