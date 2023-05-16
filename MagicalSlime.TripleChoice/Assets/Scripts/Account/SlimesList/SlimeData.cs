using System;

namespace Account.SlimesList
{
    [Serializable]
    public class SlimeData
    {
        public string name;
        public string energy;
        public int level;
        public string key;

        public bool Equals(SlimeData other)
        {
            return name == other.name && level == other.level && key == other.key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, level, key);
        }
    }
}