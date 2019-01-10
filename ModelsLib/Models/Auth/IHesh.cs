namespace StajAppCore.Models.Auth
{
    public interface IHesh
    {
        string Password { get; set; }
        string Salt { get; set; }
    }
}
