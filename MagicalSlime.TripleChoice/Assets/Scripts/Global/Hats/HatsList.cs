using UnityEngine;

namespace Global.Hats
{
    /// <summary>
    /// Gets hat data by key
    /// </summary>
    public class HatsList : MonoBehaviour
    {
        [SerializeField]
        private Hat[] hats;

        public static Hat[] Hats => Instance? Instance.hats: new Hat[]{};

        public static HatsList Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public static Hat GetCurrentHat()
        {
            if (Instance == null) return null;
            string current = DataSaver.LoadCurrentHat();
            
            return GetHat(current);
        }

        public static Hat GetHat(string hatName)
        {
            if (Instance == null) return null;
            
            foreach (Hat hat in Hats)
            {
                if (hat.key == hatName) return hat;
            }

            return null;
        }
    }
}