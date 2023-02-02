using Microsoft.AspNetCore.SignalR;
using TTRPG_Combat_Turn_Tracker.Server.Services;
using TTRPG_Combat_Turn_Tracker.Shared.Enums;
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

        public async Task JoinEncounter(string encounterId, string name)
        {
            Console.WriteLine($"Client: {Context.ConnectionId}; Joined Group: \"{encounterId}\"; with the username: \"{Context.ConnectionId}\"");

            var user = new User(Context.ConnectionId, name, Role.Player);
            await _encounterService.JoinEncounter(encounterId, user);
        }

        public async Task CreateEncounter(string encounterId, string name)
        {
            Console.WriteLine($"Client: {Context.ConnectionId}; Created Group: \"{encounterId}\"; with the username: \"{Context.ConnectionId}\"");

            var user = new User(Context.ConnectionId, name, Role.DungeonMaster);
            await _encounterService.CreateEncounter(encounterId, user);
        }

        public async Task NextTurn(string encounterId)
        {
            Console.WriteLine($"Client: {Context.ConnectionId}; For Group: \"{encounterId}\"; Next Turn");

            await _encounterService.NextTurn(encounterId);
        }
    }
}
