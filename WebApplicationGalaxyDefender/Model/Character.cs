namespace WebApplicationGalaxyDefender.Model
{
    public class Character
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UnitType { get; set; }
        public int HP { get; set; }
        public int DEF { get; set; }
        public int DMG { get; set; }
        public int Range { get; set; }
        public string TalentName { get; set; }
        public string TalentDescription { get; set; }
        public string CharacterIMG { get; set; }
        public int GalaxyId { get; set; }


        public Character(int id, string gender, string name, string description, string unitType, int hp, int def, int dmg, int range, string talentName, string talentDescription, string characterImg, int galaxyId)
        {
            Id = id;
            Gender = gender;
            Name = name;
            Description = description;
            UnitType = unitType;
            HP = hp;
            DEF = def;
            DMG = dmg;
            Range = range;
            TalentName = talentName;
            TalentDescription = talentDescription;
            CharacterIMG = characterImg;
            GalaxyId = galaxyId;
        }
    }
}
