using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TTRPG_Combat_Turn_Tracker.Shared.Enums;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Client.Services
{
    public class CharacterService
    {
        private List<User> _users = new List<User>();
        private List<Character> _characters = new List<Character>();
        private int _currentIndex = 0;

        private string? _currentEncounterId;
        private HubConnection? _hubConnection;
        private readonly NavigationManager _navManager;

        public CharacterService(NavigationManager navManager)
        {
            _navManager = navManager;

            _characters.Add(new Character("Null", CharacterType.Player, 100) { Initiative = 24, ArmourClass = 22 });
            _characters.Add(new Character("Ashryn Yelhana", CharacterType.Player, 40) { Initiative = 12, ArmourClass = 15 });
            _characters.Add(new Character("Lonara Dilvari", CharacterType.Player, 87) { Initiative = 3, ArmourClass = 24 });
            _characters = _characters.OrderByDescending(c => c.Initiative).ToList();
            NotifyStateChanged();
        }

        public async Task InitializeHubConnectionAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_navManager.ToAbsoluteUri("/hubs/encounterhub"))
            .Build();

            _hubConnection.On<User>("ClientJoined", UserJoined);
            _hubConnection.On<User>("ClientLeft", UserLeft);
            _hubConnection.On("NextTurn", NextTurnReceived);

            await _hubConnection.StartAsync();
        }

        public async Task JoinEncounter(string id)
        {
            if (_hubConnection != null)
            {
                _currentEncounterId = id;
                await _hubConnection.SendAsync("JoinEncounter", id);
                await _hubConnection.SendAsync("AllEncounterUsers", id);
            }
        }


        public void AddCharacter(Character character, bool addToEnd = false)
        {
            if (addToEnd)
            {
                _characters.Add(character);
                return;
            }

            _characters.Insert(_currentIndex, character);
            _characters = _characters.OrderByDescending(c => c.Initiative).ToList(); //todo: bug - characters added to the end will be ordered when a character not added to end is added. 
            NotifyStateChanged();
        }

        public List<Character> GetAll()
        {
            return _characters;
        }

        public async Task NextTurn()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.SendAsync("NextTurn", _currentEncounterId);
            }
        }
        private void NextTurnReceived()
        {
            _currentIndex = (_currentIndex + 1) % _characters.Count;
            NotifyStateChanged();
        }

        public Character GetCurrent()
        {
            return _characters[_currentIndex];
        }

        public Character GetPrevious()
        {
            return _characters[(_currentIndex + _characters.Count - 1) % _characters.Count];
        }

        public Character GetNext()
        {
            return _characters[(_currentIndex + 1) % _characters.Count];
        }

        public void CharacterUpdated()
        {
            NotifyStateChanged();
        }

        private void UserJoined(User user) => _users.Add(user);
        private void UserLeft(User user) => _users.Remove(user);

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    //public class CharacterComparer : IComparer<Character>
    //{
    //    public int Compare(Character x, Character y)
    //    {
    //        return x.Initiative.CompareTo(y.Initiative);
    //    }
    //}

}
