using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Com.GabrielBernabeu.Cultivation.AR {
    public class ARTree : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private Transform treeContainer = default;
        
        private Transform tree;

        private void Start()
        {
            tree = ARManager.Instance.Tree;
            tree.SetParent(treeContainer, false);
            tree.localPosition = Vector3.zero;
        }

        private void Update()
        {
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
                transform.rotation = lHits[0].pose.rotation;
            }
        }
    }
}
