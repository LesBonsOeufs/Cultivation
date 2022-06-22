///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/06/2022 20:24
///-----------------------------------------------------------------

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Com.GabrielBernabeu.Cultivation {
    public class ChooseSeedScreen : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.6f;

        [Header("Buttons")]
        [SerializeField] private Button sportButton = default;
        [SerializeField] private Button restingButton = default;
        [SerializeField] private Button socialButton = default;
        [SerializeField] private Button learningButton = default;

        [Header("ExampleTrees")]
        [SerializeField] private Transform sportTree = default;
        [SerializeField] private Transform restingTree = default;
        [SerializeField] private Transform socialTree = default;
        [SerializeField] private Transform learningTree = default;

        private Vector3 baseExampleTreesScale;
        
        private CanvasGroup canvasGroup;

        public Vector3 TreesScales
        {
            get
            {
                return sportTree.localScale;
            }

            set
            {
                sportTree.localScale = value;
                restingTree.localScale = value;
                socialTree.localScale = value;
                learningTree.localScale = value;
            }
        }

        public static ChooseSeedScreen Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;

            sportButton.onClick.AddListener(ChooseSport);
            restingButton.onClick.AddListener(ChooseResting);
            socialButton.onClick.AddListener(ChooseSocial);
            learningButton.onClick.AddListener(ChooseLearning);

            baseExampleTreesScale = sportTree.localScale;

            sportTree.localScale = Vector3.zero;
            restingTree.localScale = Vector3.zero;
            socialTree.localScale = Vector3.zero;
            learningTree.localScale = Vector3.zero;
        }

        public void In()
        {
            canvasGroup.DOFade(1f, fadeDuration);
            canvasGroup.blocksRaycasts = true;

            sportTree.DOScale(baseExampleTreesScale, fadeDuration);
            restingTree.DOScale(baseExampleTreesScale, fadeDuration);
            socialTree.DOScale(baseExampleTreesScale, fadeDuration);
            learningTree.DOScale(baseExampleTreesScale, fadeDuration);
        }

        public void Out()
        {
            canvasGroup.DOFade(0f, fadeDuration);
            canvasGroup.blocksRaycasts = false;

            sportTree.DOScale(Vector3.zero, fadeDuration);
            restingTree.DOScale(Vector3.zero, fadeDuration);
            socialTree.DOScale(Vector3.zero, fadeDuration);
            learningTree.DOScale(Vector3.zero, fadeDuration);
        }

        private void ChooseSport()
        {
            Debug.Log("Sport!");
        }

        private void ChooseResting()
        {
            Debug.Log("Resting!");
        }

        private void ChooseSocial()
        {
            Debug.Log("Social!");
        }

        private void ChooseLearning()
        {
            Debug.Log("Learning!");
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            Instance = null;
            sportButton.onClick.RemoveListener(ChooseSport);
            restingButton.onClick.RemoveListener(ChooseResting);
            socialButton.onClick.RemoveListener(ChooseSocial);
            learningButton.onClick.RemoveListener(ChooseLearning);
        }
    }
}
