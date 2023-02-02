using Microsoft.AspNetCore.SignalR;
using TTRPG_Combat_Turn_Tracker.Server.Services;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Server.Hubs
{
    public class EncounterHub : Hub
    {
        private readonly EncounterService _encounterService;
        public EncounterHub(EncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client Connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public async Task JoinEncounter(string groupName)
        {
            User user = new User(Context.ConnectionId, Context.ConnectionId);

            await _encounterService.JoinGroup(groupName, user);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine($"Client: {Context.ConnectionId}; Joined Group: \"{groupName}\"; with the username: \"{Context.ConnectionId}\"");
            await Clients.Group(groupName).SendAsync("ClientJoined", user);
        }

        public async Task NextTurn(string groupName)
        {
            await Clients.Group(groupName).SendAsync("NextTurn");
        }
    }
}
