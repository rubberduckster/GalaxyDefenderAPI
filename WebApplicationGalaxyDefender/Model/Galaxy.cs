namespace WebApplicationGalaxyDefender.Model
{
    public class Galaxy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Galaxy(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
