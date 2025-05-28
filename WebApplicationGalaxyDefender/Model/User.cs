namespace WebApplicationGalaxyDefender.Model
{
    public class User
    {
        public int Id { get; set; }
        public byte isAdmin { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
