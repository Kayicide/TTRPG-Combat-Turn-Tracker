namespace TTRPG_Combat_Turn_Tracker.Shared.Objects
{
    public class Health
    {
        private int _max;
        private int _tempMax;
        private int _current;
        private int _tempCurrent;

        public Health(int max, int current)
        {
            _current = current;
            _max = max;
            _tempCurrent = 0;
            _tempMax = 0;
        }

        public int Max => _max + _tempMax;
        public int Current => _current + _tempCurrent;
        public int GetMaxWithoutTemp => _max;
        public int GetCurrentWithoutTemp => _current;
        public int GetTempOnly => _tempCurrent;
        public int GetTempMaxOnly => _tempMax;

        public int SetTempCurrent (int tempCurrent) => _tempCurrent = tempCurrent;
        public int SetTempMax (int tempMax) => _tempMax = tempMax;
        public int SetCurrent (int current) => _current = current;
        public int SetMax (int max) => _max = max;

        public int Damage(int amount)
        {
            if (_tempCurrent > 0)
            {
                _tempCurrent -= amount;

                if (_tempCurrent >= 0) 
                    return Current;

                amount = -_tempCurrent;
                _tempCurrent = 0;
            }
            _current -= amount;
            return Current;
        }

        public int Heal(int amount)
        {
            _current += amount;

            if(_current > Max)
                _current = Max;

            return Current;
        }

    }
}
