using Microsoft.Data.SqlClient;

namespace WebApplicationGalaxyDefender.Repository
{

    public interface IGalaxyRepos
    {

    }

    public class Galaxyrepos
    {
        public SqlConnection connection = new SqlConnection("Server=localhost;Database=GalaxyDefenders;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
    }
}
