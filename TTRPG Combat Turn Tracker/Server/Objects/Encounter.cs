using Microsoft.AspNetCore.SignalR;
using TTRPG_Combat_Turn_Tracker.Server.Hubs;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Server.Objects
{
    public class Encounter
    {
        private readonly IHubContext<EncounterHub> _hubContext;
        private readonly List<User> _users;
        private readonly string _encounterId;

        public Encounter(string encounterId, IHubContext<EncounterHub> hubContext, User creator)
        {
            _encounterId = encounterId;
            _hubContext = hubContext;
            _users = new List<User>{creator};
        }

        public async Task Join(User user)
        {
            _users.Add(user);
            await _hubContext.Groups.AddToGroupAsync(user.Id, _encounterId);
            await _hubContext.Clients.Groups(_encounterId).SendAsync("ClientJoined", user);
        }

        public async Task Leave(User user)
        {
            _users.Remove(user);
            await _hubContext.Groups.RemoveFromGroupAsync(user.Id, _encounterId);
            await _hubContext.Clients.Groups(_encounterId).SendAsync("ClientLeft", user);
        }

        public async Task NextTurn()
        {
            await _hubContext.Clients.Group(_encounterId).SendAsync("NextTurn");
        }
    }
}
