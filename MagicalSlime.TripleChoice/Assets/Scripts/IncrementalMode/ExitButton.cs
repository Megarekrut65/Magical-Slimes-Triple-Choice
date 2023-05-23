using DataManagement;
using UnityEngine;

namespace IncrementalMode
{
    public class ExitButton : MonoBehaviour
    {
        private void Start()
        {
            
            if (SystemInfo.deviceType != DeviceType.Desktop || Application.platform == RuntimePlatform.WindowsEditor)
            {
                gameObject.SetActive(false);
            }
        }

        public void Exit()
        {
            DataSync sync = new DataSync();
            sync.SyncAllData((_,_) =>
            {
                Application.Quit();
            });
        }
    }
}