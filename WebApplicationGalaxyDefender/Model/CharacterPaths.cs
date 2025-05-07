namespace WebApplicationGalaxyDefender.Model
{
    public class CharacterPaths
    {
        public int Id { get; set; }
        public int Tier { get; set; }
        public string PathName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int DEF { get; set; }
        public int DMG { get; set; }
        public int Range { get; set; }
        public string TalentName { get; set; }
        public string TalentDescription { get; set; }
        public string CharacterIMG { get; set; }
        public int CharacterId { get; set; }

        public CharacterPaths(int id, int tier, string pathName, string name, string description, int hp, int def, int dmg, int range, string talentName, string talentDescription, string characterImg, int characterId)
        {
            Id = id;
            Tier = tier;
            PathName = pathName;
            Name = name;
            Description = description;
            HP = hp;
            DEF = def;
            DMG = dmg;
            Range = range;
            TalentName = talentName;
            TalentDescription = talentDescription;
            CharacterIMG = characterImg;
            CharacterId = characterId;
        }
    }
}
