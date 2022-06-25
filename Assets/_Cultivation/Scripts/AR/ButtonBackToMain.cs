using UnityEngine;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.Cultivation.AR
{
    [RequireComponent(typeof(Button))]
    public class ButtonBackToMain : MonoBehaviour
    {
        private Button button;

        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ARManager.Instance.CloseAR);
        }

        private void OnDestroy()
        {
            if (ARManager.Instance != null)
                button.onClick.RemoveListener(ARManager.Instance.CloseAR);
        }
    }
}
