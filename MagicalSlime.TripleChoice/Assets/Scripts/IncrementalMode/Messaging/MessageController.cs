using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncrementalMode.Messaging
{
    /// <summary>
    /// Shows text with moving in scene center during some time.
    /// </summary>
    public class MessageController:MonoBehaviour
    {
        [SerializeField] private GameObject parent;
        private Transform _parentTransform;
        [SerializeField] private GameObject messageObj;
        private readonly List<GameObject> _messagesPool = new List<GameObject>();

        private void Start()
        {
            _parentTransform = parent.transform;
        }

        public void Message(string message)
        {
            GameObject obj;
            if (_messagesPool.Count < 10)
            {
                obj= Instantiate(messageObj, _parentTransform, true);
                _messagesPool.Add(obj);
            }
            else
            {
                obj = _messagesPool[0];
                _messagesPool.RemoveAt(0);
                _messagesPool.Add(obj);
            }
            
            obj.transform.localScale = new Vector3(1f, 1f, 1f);

            MessageObject mes = obj.GetComponent<MessageObject>();
            mes.SetMessage(message);
            mes.AnimationStart();
        }
    }
}