using UnityEngine;
using UnityEngine.UI;

namespace GameOver
{
    public class CupController : MonoBehaviour
    {
        [SerializeField] private Sprite[] cups;
        [SerializeField] private Image cup;

        private void Start()
        {
            cup.sprite = cups[Random.Range(0, cups.Length)];
        }
    }
}