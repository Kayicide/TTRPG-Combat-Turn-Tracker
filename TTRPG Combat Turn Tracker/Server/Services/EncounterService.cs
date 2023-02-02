using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using TTRPG_Combat_Turn_Tracker.Server.Hubs;
using TTRPG_Combat_Turn_Tracker.Server.Objects;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Server.Services
{
    public class EncounterService
    {
        private readonly IHubContext<EncounterHub> _hubContext;
        private readonly Dictionary<string, Encounter> _encounters;

        public EncounterService(IHubContext<EncounterHub> hubContext)
        {
            _hubContext = hubContext;
            _encounters = new Dictionary<string, Encounter>();
        }

        public async Task JoinEncounter(string encounterId, User user)
        {
            if (_encounters.ContainsKey(encounterId))
                await _encounters[encounterId].Join(user);
        }

        public async Task CreateEncounter(string encounterId, User user)
        {
            if (!_encounters.ContainsKey(encounterId))
                _encounters[encounterId] = new Encounter(encounterId, _hubContext, user);
        }

        public async Task NextTurn(string encounterId)
        {
            if (_encounters.ContainsKey(encounterId))
            {
                await _encounters[encounterId].NextTurn();
            }
        }

        public async Task AddCharacter(string encounterId, Character character)
        {
            await _encounters[encounterId].AddCharacter(character);
        }

        public async Task RemoveCharacter(string encounterId, Character character)
        {
            await _encounters[encounterId].RemoveCharacter(character);
        }

    }
}
