using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Repository;
using WebApplicationGalaxyDefender.DataModels;

namespace WebApplicationGalaxyDefender.Service
{
    public class GalaxyService
    {

        private Galaxyrepos _galaxyrepos;

        public GalaxyService(Galaxyrepos galaxyrepos)
        {
            _galaxyrepos = galaxyrepos;
        }

        public List<Galaxy> GetGalaxies()
        {
            return _galaxyrepos.GetGalaxies();
        }

        public Galaxy GetGalaxyById(int galaxyId)
        {
            return _galaxyrepos.GetGalaxyById(galaxyId);
        }

        public Galaxy PostGalaxy(GalaxyData data)
        {
            return _galaxyrepos.CreateGalaxy(data);
        }

        public void DeleteGalaxy(int galaxyId)
        {
            _galaxyrepos.DeleteGalaxy(galaxyId);
        }

        public Galaxy PutGalaxy(GalaxyData data)
        {
            return _galaxyrepos.UpdateGalaxy(data);
        }
    }
}
