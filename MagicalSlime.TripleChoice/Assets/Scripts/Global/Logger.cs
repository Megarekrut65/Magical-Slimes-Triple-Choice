using JetBrains.Annotations;
using UnityEngine;

namespace Global
{
    public static class CustomLogger
    {
        public static void Log([CanBeNull] object str)
        {
            if(str != null) Debug.Log(str as string);
        }
    }
}