using System;
using System.Collections.Generic;

namespace LoginRegister
{
    public class Info
    {
        public string slimeName;
        public int maxLevel;
        public string maxEnergy;
        public int cups;
        public int diamonds;
        public string energy;
        public int level;

        private bool Equals(Info other)
        {
            return slimeName == other.slimeName 
                   && maxLevel == other.maxLevel 
                   && maxEnergy == other.maxEnergy 
                   && cups == other.cups 
                   && diamonds == other.diamonds 
                   && energy == other.energy 
                   && level == other.level;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Info)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(slimeName, maxLevel, maxEnergy, cups, diamonds, energy, level);
        }

        public bool IsEmpty()
        {
            return slimeName == "" && maxLevel == 0 && maxEnergy == "0" && energy == "0" && level == 0;
        }
    }
}