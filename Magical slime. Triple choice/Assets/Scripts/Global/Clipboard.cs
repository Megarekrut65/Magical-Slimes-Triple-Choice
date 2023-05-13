using UnityEngine;

namespace Global
{
    public static class Clipboard
    {
        public static void Copy(string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }

        public static string Paste()
        {
            return GUIUtility.systemCopyBuffer;
        }
    }
}