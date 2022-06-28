using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Com.GabrielBernabeu.Cultivation.AR {
    public class ARTree : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private Transform treeContainer = default;

        [Header("InfoTextElements")]
        [SerializeField] private TextMeshProUGUI infoTmp = default;
        [SerializeField] private string infoTextIfMoving = "Touch to place!";
        [SerializeField] private string infoTextIfPlaced = "Touch to move!";
        
        private Transform tree;

        private bool IsPlaced
        {
            get
            {
                return _isPlaced;
            }

            set
            {
                _isPlaced = value;
                infoTmp.text = _isPlaced ? infoTextIfPlaced : infoTextIfMoving;
            }
        }
        private bool _isPlaced;

        private void Start()
        {
            IsPlaced = false;
            tree = ARManager.Instance.Tree;
            tree.SetParent(treeContainer, false);
            tree.localPosition = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                IsPlaced = !IsPlaced;

            if (!IsPlaced)
                UpdateTreePosition();
        }

        private void UpdateTreePosition()
        {
            Vector2 lScreenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            List<ARRaycastHit> lHits = new List<ARRaycastHit>();
            arRaycastManager.Raycast(lScreenPosition, lHits, TrackableType.Planes);

            if (lHits.Count > 0)
            {
                transform.position = lHits[0].pose.position;
                //No rotation so that user can rotate around the object
                //transform.rotation = lHits[0].pose.rotation;
            }
        }
    }
}
