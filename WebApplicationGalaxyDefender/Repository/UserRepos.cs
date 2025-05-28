using Microsoft.Data.SqlClient;
using BCrypt.Net;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplicationGalaxyDefender.Repository
{
    public class UserRepos
    {
        private readonly IConfiguration _config;

        public UserRepos(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection connection = new SqlConnection("Server=localhost;Database=GalaxyDefenders;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public void register(string username, string password) {
            connection.Open();

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            string sqlString = "SELECT COUNT(*) FROM Users WHERE Username = @username";


        }

        public string login(string username, string password)
        {
            connection.Open();

            string sqlString = "SELECT * FROM Users";

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            sqlCommand.Parameters.AddWithValue("username", username);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            string storedHash = reader["PasswordHash"].ToString();
            int isAdmin = Convert.ToInt32(reader["isAdmin"]);

            if (!BCrypt.Net.BCrypt.Verify(password, storedHash))
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor();
        }
    }
}
