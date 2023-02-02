using System;
using TTRPG_Combat_Turn_Tracker.Shared.Enums;

namespace TTRPG_Combat_Turn_Tracker.Shared.Objects
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }

        public User(string id, string name, Role role = Role.Player)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
