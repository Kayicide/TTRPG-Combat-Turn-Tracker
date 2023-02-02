using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Server.Objects
{
    public class Encounter
    {
        public string Id { get; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        private List<Character> _characters { get; set; } = new List<Character>();

        private int _currentIndex = 0;

        public Encounter(string id)
        {
            Id = id;
        }

        public Encounter(string id, User creatorUser, string name = "Encounter")
        {
            Id = id;
            Name = name;
            Users.Add(creatorUser);
        }

        public Character AddCharacter(Character character)
        {
            _characters.Insert(_currentIndex, character);
            _characters = _characters.OrderByDescending(c => c.Initiative).ToList(); //todo: bug - characters added to the end will be ordered when a character not added to end is added. 

            return character;
        }

        public List<Character> GetAll()
        {
            return _characters;
        }

        public void NextTurn()
        {
            _currentIndex = (_currentIndex + 1) % _characters.Count;
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return Users;
        }
    }
}
