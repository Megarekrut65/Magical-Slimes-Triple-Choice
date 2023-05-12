using UnityEngine;

namespace Global.Entity
{
    public class EntityList:MonoBehaviour
    {
        [SerializeField] private EntityData[] data;

        public static EntityData[] Data => Instance? Instance.data: new EntityData[]{};

        public static EntityList Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public static EntityData GetEntity(string key)
        {
            if (Instance == null) return null;
            foreach (EntityData current in Data)
            {
                if (current.key == key) return current;
            }

            return null;
        }
    }
}