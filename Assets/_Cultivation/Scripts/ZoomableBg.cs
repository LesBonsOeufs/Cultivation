///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/06/2022 19:44
///-----------------------------------------------------------------

using UnityEngine;
using DG.Tweening;

namespace Com.GabrielBernabeu.Cultivation 
{
    public enum ZoomState
    {
        FAR,
        CLOSE,
        TREE
    }

    public delegate void ZoomableBgEventHandler(ZoomableBg sender);
    public class ZoomableBg : MonoBehaviour
    {
        [SerializeField] private float zoomDuration = 0.85f;

        private RectTransform rectTransform;
        public event ZoomableBgEventHandler OnZoomEnded;

        public ZoomState ZoomState
        {
            get
            {
                return _zoomState;
            }
            set
            {
                if (_zoomState == value)
                    return;

                _zoomState = value;

                switch (_zoomState)
                {
                    case ZoomState.FAR:
                        ToFar();
                        break;
                    case ZoomState.CLOSE:
                        ToClose();
                        break;
                    case ZoomState.TREE:
                        ToGround();
                        break;
                }
            }
        }
        private ZoomState _zoomState;

        public static ZoomableBg Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0f, 187.57f);
            rectTransform.sizeDelta = new Vector2(2320f, 1982.5f);
        }

        private void ToFar()
        {
            rectTransform.DOAnchorPos(new Vector2(0f, 187.57f), zoomDuration);
            rectTransform.DOSizeDelta(new Vector2(2320f, 1982.5f), zoomDuration).OnComplete(OnZoomEndedInvoke);
        }

        private void ToClose()
        {
            rectTransform.DOAnchorPos(new Vector2(992f, 2405f), zoomDuration);
            rectTransform.DOSizeDelta(new Vector2(9430.14f, 8058.28f), zoomDuration).OnComplete(OnZoomEndedInvoke);
        }

        private void ToGround()
        {
            rectTransform.DOAnchorPos(new Vector2(-69f, 1768f), zoomDuration);
            rectTransform.DOSizeDelta(new Vector2(6622.82f, 5659.36f), zoomDuration).OnComplete(OnZoomEndedInvoke);
        }

        private void OnZoomEndedInvoke()
        {
            OnZoomEnded?.Invoke(this);
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            Instance = null;
            OnZoomEnded = null;
        }
    }
}
