using UnityEngine;

namespace Global
{
    public static class CopyToClipboard
    {
        public static void Copy(string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }
}