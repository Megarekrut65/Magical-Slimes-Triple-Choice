using LoginRegister;
using UnityEngine;

namespace CreatingSlime
{
    /// <summary>
    /// Hides account button if user is signed in
    /// </summary>
    public class AccountButton : MonoBehaviour
    {
        private void Start()
        {
            if (LoginController.UserSignIn())
            {
                gameObject.SetActive(false);
            }
        }
    }
}