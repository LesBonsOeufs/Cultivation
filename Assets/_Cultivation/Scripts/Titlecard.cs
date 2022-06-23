///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/06/2022 17:40
///-----------------------------------------------------------------

using UnityEngine;
using DG.Tweening;
using Com.GabrielBernabeu.Common.DataManagement;

namespace Com.GabrielBernabeu.Cultivation {
    public class Titlecard : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.6f;
        
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                EnterGame();
        }

        private void EnterGame()
        {
            enabled = false;
            ZoomableBg.Instance.ZoomState = ZoomState.CLOSE;
            ZoomableBg.Instance.OnZoomEnded += ZoomableBg_OnZoomEnded;

            canvasGroup.DOFade(0f, fadeDuration);
        }

        private void ZoomableBg_OnZoomEnded(ZoomableBg sender)
        {
            ZoomableBg.Instance.OnZoomEnded -= ZoomableBg_OnZoomEnded;
            ToChooseSeedScreen();
        }

        private void ToChooseSeedScreen()
        {
            gameObject.SetActive(false);

            if (LocalDataSaving.LoadData() == null)
                ChooseSeedScreen.Instance.In();
            //else TreeScreen.In();
        }

        private void OnDestroy()
        {
            if (ZoomableBg.Instance != null)
                ZoomableBg.Instance.OnZoomEnded -= ZoomableBg_OnZoomEnded;
        }
    }
}
