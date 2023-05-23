using System;
using Firebase.Database;

namespace FightingMode.Lobby.Global
{
    public class RoomSearcher
    {
        private readonly float _minDistance;
        private readonly Point _main;
        private readonly bool _fast;

        private readonly float _levelPart;


        public RoomSearcher(float minDistance, Point main, bool fast, float levelPart)
        {
            _minDistance = minDistance;
            _main = main;
            _fast = fast;
            _levelPart = levelPart;
            
            _main.y *= _levelPart;
        }

        public string Search(MutableData data)
        {
            float min = 0f;
            string minCode = "";

            foreach (MutableData snapshot in data.Children)
            {
                bool isFree = Convert.ToBoolean(snapshot.Child("isFree").Value);
                if (!isFree) continue;
                    
                int cups = Convert.ToInt32(snapshot.Child("cups").Value);
                int maxLevel = Convert.ToInt32(snapshot.Child("maxLevel").Value);
                Point enemy = new Point(cups, _levelPart*maxLevel);
                float distance = _main.Distance(enemy);
                        
                if (minCode == "" || distance < min)
                {
                    minCode = snapshot.Key;
                    min = distance;
                }
                if(min <= _minDistance) break;
            }

            if (minCode != "" && (_fast || min <= _minDistance))
            {
                return minCode;
            }

            return "";
        }
    }
}