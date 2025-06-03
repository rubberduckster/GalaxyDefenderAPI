using Microsoft.Data.SqlClient;
using BCrypt.Net;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

        public bool register(string username, string password) {
            connection.Open();

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            string sqlString = "SELECT COUNT(*) FROM Users WHERE Username = @username";
            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            sqlCommand.Parameters.AddWithValue("@username", username);

            int userExists = (int)sqlCommand.ExecuteScalar();
            if (userExists > 0) 
            {
                return false;
            }

            string _sqlString = "INSERT INTO Users (Username, PasswordHash, isAdmin) VALUES (@username, @passwordHash, 0)";
            SqlCommand _sqlCommand = new SqlCommand(_sqlString, connection);

            _sqlCommand.Parameters.AddWithValue("@username", username);
            _sqlCommand.Parameters.AddWithValue("@passwordHash", passwordHash);

            _sqlCommand.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        public string login(string username, string password)
        {
            connection.Open();

            string sqlString = "SELECT * FROM Users WHERE Username = @username";

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            sqlCommand.Parameters.AddWithValue("@username", username);

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

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("isAdmin", isAdmin.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            DateTime tokenExpiration = DateTime.UtcNow.AddHours(1);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            SigningCredentials signCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor securityToken = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = tokenExpiration,
                SigningCredentials = signCreds
            };

            var token = tokenHandler.CreateToken(securityToken);

            connection.Close();

            return tokenHandler.WriteToken(token);
        }
    }
}
