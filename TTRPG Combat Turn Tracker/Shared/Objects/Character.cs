using TTRPG_Combat_Turn_Tracker.Shared.Enums;

namespace TTRPG_Combat_Turn_Tracker.Shared.Objects
{
    public class Character
    {
        public string Name { get; set; }
        public CharacterType Type { get; set; }
        public string FirstName => Type == CharacterType.Enemy ? Name : Name.Split()[0]; //so we don't chop off a number for a monster called "Goblin 1" for example
        public int Initiative { get; set; }
        public int CurrentHealth => Health.Current;
        public int MaxHealth => Health.Max;
        public int ArmourClass { get; set; }
        public Health Health { get; }
        public Character(string name, CharacterType type, int health)
        {
            Name = name;
            Type = type;
            Health = new Health(health, health);
        }


    }
}
