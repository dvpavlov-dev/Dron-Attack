using UnityEngine;

namespace _Drone_Attack.UI.Scripts
{
    public class WindowView : MonoBehaviour
    {
        public void ShowWindow()
        {
            gameObject.SetActive(true);
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
        }
    }
}
