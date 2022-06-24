///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/06/2022 17:40
///-----------------------------------------------------------------

using UnityEngine;
using DG.Tweening;
using Com.GabrielBernabeu.Common.DataManagement;
using System;

namespace Com.GabrielBernabeu.Cultivation {
    public class Titlecard : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.6f;
        
        private CanvasGroup canvasGroup;
        private Action doNext;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            LocalData? lSavedData = LocalDataSaving.LoadData();

            if (lSavedData == null)
                doNext = ZoomToChooseScreen;
            else
            {
                doNext = ZoomToTreeScreen;
                TreeScreen.Instance.Load(lSavedData.Value);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                EnterGame();
        }

        private void EnterGame()
        {
            enabled = false;
            canvasGroup.DOFade(0f, fadeDuration);
            doNext();
        }

        private void ZoomToChooseScreen()
        {
            ZoomableBg.Instance.ZoomState = ZoomState.CLOSE;
            ZoomableBg.Instance.OnZoomEnded += ToChooseSeedScreen;
        }

        private void ZoomToTreeScreen()
        {
            ZoomableBg.Instance.ZoomState = ZoomState.TREE;
            ZoomableBg.Instance.OnZoomEnded += ToTreeScreen;
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

            TreeScreen.Instance.In();
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
