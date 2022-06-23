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

            if (LocalDataSaving.LoadData() == null)
            {
                ZoomableBg.Instance.ZoomState = ZoomState.CLOSE;
                ZoomableBg.Instance.OnZoomEnded += ToChooseSeedScreen;
            }
            else
            {
                ZoomableBg.Instance.ZoomState = ZoomState.TREE;
                ZoomableBg.Instance.OnZoomEnded += ToTreeScreen;
            }
                
            canvasGroup.DOFade(0f, fadeDuration);
        }

        private void ToChooseSeedScreen(ZoomableBg sender)
        {
            ZoomableBg.Instance.OnZoomEnded -= ToChooseSeedScreen;
            gameObject.SetActive(false);

            ChooseSeedScreen.Instance.In();
        }

        private void ToTreeScreen(ZoomableBg sender)
        {
            ZoomableBg.Instance.OnZoomEnded -= ToTreeScreen;
            gameObject.SetActive(false);

            //TreeScreen.Instance.In();
        }

        private void OnDestroy()
        {
            if (ZoomableBg.Instance != null)
            {
                ZoomableBg.Instance.OnZoomEnded -= ToChooseSeedScreen;
                ZoomableBg.Instance.OnZoomEnded -= ToTreeScreen;
            }
        }
    }
}
