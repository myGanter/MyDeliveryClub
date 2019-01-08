namespace StajAppCore.Models.Auth
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
    }
}
