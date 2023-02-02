using System.Collections.Concurrent;
using TTRPG_Combat_Turn_Tracker.Server.Objects;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Server.Services
{
    public class EncounterService
    {
        private readonly ConcurrentDictionary<string, Encounter> _encounters = new ConcurrentDictionary<string, Encounter>();

        public async Task JoinGroup(string id, User user)
        {
            _encounters.AddOrUpdate(id,
                new Encounter(id, user),
                (key, existingVal) =>
                {
                    existingVal.AddUser(user);
                    return existingVal;
                });
        }

        public async Task CreateGroup(string id, User user)
        {
            _encounters.TryAdd(id, new Encounter(id, user));
            await JoinGroup(id, user);
        }

        public async Task LeaveGroup(string id, User user)
        {
            _encounters.AddOrUpdate(id,
                new Encounter(id),
                (key, existingVal) =>
                {
                    existingVal.RemoveUser(user);
                    return existingVal;
                });
        }

        public async Task<List<User>> GetGroupMembers(string groupName)
        {
            _encounters.TryGetValue(groupName, out Encounter? encounter);
            return encounter?.GetAllUsers().ToList() ?? new List<User>();
        }

    }
}
