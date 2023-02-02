using Microsoft.AspNetCore.SignalR;
using TTRPG_Combat_Turn_Tracker.Server.Hubs;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Server.Objects
{
    public class Encounter
    {
        private readonly IHubContext<EncounterHub> _hubContext;
        private readonly List<User> _users;
        private readonly List<Character> _characters;
        private readonly string _encounterId;

        public Encounter(string encounterId, IHubContext<EncounterHub> hubContext, User creator)
        {
            _encounterId = encounterId;
            _hubContext = hubContext;
            _users = new List<User>{creator};
            _characters = new List<Character>();
        }

        public async Task Join(User user)
        {
            _users.Add(user);
            await _hubContext.Groups.AddToGroupAsync(user.Id, _encounterId);
            await Group.SendAsync("ClientJoined", user);
        }

        public async Task Leave(User user)
        {
            _users.Remove(user);
            await _hubContext.Groups.RemoveFromGroupAsync(user.Id, _encounterId);
            await Group.SendAsync("ClientLeft", user);
        }

        public async Task NextTurn()
        {
            await Group.SendAsync("NextTurn");
        }

        public async Task AddCharacter(Character character)
        {
            _characters.Add(character);
            await Group.SendAsync("AddCharacter", character);
        }

        public async Task RemoveCharacter(Character character)
        {
            _characters.Remove(character);
            await Group.SendAsync("RemoveCharacter", character);
        }
        public IClientProxy Group => _hubContext.Clients.Group(_encounterId);
    }
}
