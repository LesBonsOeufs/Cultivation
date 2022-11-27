///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/06/2022 20:24
///-----------------------------------------------------------------

using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Com.GabrielBernabeu.Common.CustomButtons;
using Com.GabrielBernabeu.Common.DataManagement;
using Com.GabrielBernabeu.Common;

namespace Com.GabrielBernabeu.Cultivation {
    public class ChooseSeedScreen : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.6f;

        [Header("Buttons")]
        [SerializeField] private Button sportButton = default;
        [SerializeField] private Button restingButton = default;
        [SerializeField] private Button socialButton = default;
        [SerializeField] private Button learningButton = default;
        [SerializeField] private Button cancelButton = default;
        [SerializeField] private CustomBouncyButton confirmButton = default;

        [Header("DayToggles")]
        [SerializeField] private CustomToggle monday = default;
        [SerializeField] private CustomToggle tuesday = default;
        [SerializeField] private CustomToggle wednesday = default;
        [SerializeField] private CustomToggle thursday = default;
        [SerializeField] private CustomToggle friday = default;
        [SerializeField] private CustomToggle saturday = default;
        [SerializeField] private CustomToggle sunday = default;

        [Header("ExampleTrees")]
        [SerializeField] private Transform sportTree = default;
        [SerializeField] private Transform restingTree = default;
        [SerializeField] private Transform socialTree = default;
        [SerializeField] private Transform learningTree = default;

        [Space]

        [SerializeField] private CanvasGroup chooseSeedGroup = default;
        [SerializeField] private CanvasGroup confirmSeedGroup = default;
        [SerializeField] private float confirmSeedGroupFinalScale = 1.6f;
        [SerializeField] private TMP_InputField taskInput = default;

        private Vector3 baseExampleTreesScale;
        private Vector3 baseExampleTreesLocalPosition;

        private SeedType chosenSeedType;
        private CanvasGroup canvasGroup;
        private bool lastConfirmButtonPress = false;

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
            cancelButton.onClick.AddListener(ToChooseSeed);

            baseExampleTreesScale = sportTree.localScale;
            baseExampleTreesLocalPosition = sportTree.localPosition;

            sportTree.localScale = Vector3.zero;
            restingTree.localScale = Vector3.zero;
            socialTree.localScale = Vector3.zero;
            learningTree.localScale = Vector3.zero;

            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!confirmButton.IsBeingPressed && lastConfirmButtonPress)
                FinalConfirm();

            lastConfirmButtonPress = confirmButton.IsBeingPressed;
        }

        public void In()
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(1f, fadeDuration);
            canvasGroup.blocksRaycasts = true;

            ToChooseSeed();
            confirmSeedGroup.alpha = 0f;
        }

        public void Out()
        {
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(() => { gameObject.SetActive(false); });
            canvasGroup.blocksRaycasts = false;

            sportTree.DOScale(Vector3.zero, fadeDuration);
            restingTree.DOScale(Vector3.zero, fadeDuration);
            socialTree.DOScale(Vector3.zero, fadeDuration);
            learningTree.DOScale(Vector3.zero, fadeDuration);
        }

        private void ChooseSport()
        {
            ToConfirmSeed(SeedType.SPORT);
        }

        private void ChooseResting()
        {
            ToConfirmSeed(SeedType.RESTING);
        }

        private void ChooseSocial()
        {
            ToConfirmSeed(SeedType.SOCIAL);
        }

        private void ChooseLearning()
        {
            ToConfirmSeed(SeedType.LEARNING);
        }

        private void ToChooseSeed()
        {
            sportTree.DOLocalMove(baseExampleTreesLocalPosition, fadeDuration);
            restingTree.DOLocalMove(baseExampleTreesLocalPosition, fadeDuration);
            socialTree.DOLocalMove(baseExampleTreesLocalPosition, fadeDuration);
            learningTree.DOLocalMove(baseExampleTreesLocalPosition, fadeDuration);

            sportTree.DOScale(baseExampleTreesScale, fadeDuration);
            restingTree.DOScale(baseExampleTreesScale, fadeDuration);
            socialTree.DOScale(baseExampleTreesScale, fadeDuration);
            learningTree.DOScale(baseExampleTreesScale, fadeDuration);

            chooseSeedGroup.DOFade(1f, fadeDuration);
            chooseSeedGroup.blocksRaycasts = true;
            confirmSeedGroup.DOFade(0f, fadeDuration).OnComplete(() => { taskInput.text = ""; });
            confirmSeedGroup.blocksRaycasts = false;
        }

        private void ToConfirmSeed(SeedType type)
        {
            chosenSeedType = type;
            Transform lChosenTree = default;

            switch (chosenSeedType)
            {
                case SeedType.SPORT:
                    lChosenTree = sportTree;
                    break;
                case SeedType.RESTING:
                    lChosenTree = restingTree;
                    break;
                case SeedType.SOCIAL:
                    lChosenTree = socialTree;
                    break;
                case SeedType.LEARNING:
                    lChosenTree = learningTree;
                    break;
            }

            if (lChosenTree != sportTree)
                sportTree.DOScale(Vector3.zero, fadeDuration);
            if (lChosenTree != restingTree)
                restingTree.DOScale(Vector3.zero, fadeDuration);
            if (lChosenTree != socialTree)
                socialTree.DOScale(Vector3.zero, fadeDuration);
            if (lChosenTree != learningTree)
                learningTree.DOScale(Vector3.zero, fadeDuration);

            chooseSeedGroup.DOFade(0f, fadeDuration);
            chooseSeedGroup.blocksRaycasts = false;
            confirmSeedGroup.DOFade(1f, fadeDuration);
            confirmSeedGroup.blocksRaycasts = true;

            lChosenTree.DOMove(new Vector3(0f, -2.1f, -1.5f), fadeDuration);
            lChosenTree.DOScale(new Vector3(1539.126f, 1539.126f, 5391.466f), fadeDuration);
        }

        private void FinalConfirm()
        {
            if (taskInput.text == "")
            {
                TextFeedbackMaker.Instance.CreateText("Please write a task name!", Color.red, 1f, Color.red, 0.5f, 1f, 0f, Color.red,
                                                  0f, 0.7f, false, confirmButton.transform.position, new Vector2(3f, 1f));
            }
            else if (!monday.IsSwitchedOn && !tuesday.IsSwitchedOn && !wednesday.IsSwitchedOn && !thursday.IsSwitchedOn
                     && !friday.IsSwitchedOn && !saturday.IsSwitchedOn && !sunday.IsSwitchedOn)
            {
                TextFeedbackMaker.Instance.CreateText("At least one day must be chosen!", Color.red, 1f, Color.red, 0.5f, 1f, 0f, Color.red,
                                                  0f, 0.7f, false, confirmButton.transform.position, new Vector2(3f, 1f));
            }
            else
            {
                Out();
                confirmSeedGroup.transform.DOScale(confirmSeedGroupFinalScale, fadeDuration)
                    .OnComplete(() => { confirmSeedGroup.transform.localScale = Vector3.one; });

                LocalDataSaving.SaveData(new LocalData(chosenSeedType, taskInput.text,
                                             monday.IsSwitchedOn, tuesday.IsSwitchedOn, wednesday.IsSwitchedOn,
                                             thursday.IsSwitchedOn, friday.IsSwitchedOn, saturday.IsSwitchedOn, sunday.IsSwitchedOn));

                LocalData lLocalData = LocalDataSaving.LoadData().Value;
                MobileNotificationManager.Instance.InitRepeatingNotifications(lLocalData);
                TreeScreen.Instance.Load(lLocalData);

                ZoomableBg.Instance.ZoomState = ZoomState.TREE;
                ZoomableBg.Instance.OnZoomEnded += ToTreeScreen;
            }
        }

        private void ToTreeScreen(ZoomableBg sender)
        {
            ZoomableBg.Instance.OnZoomEnded -= ToTreeScreen;
            TreeScreen.Instance.In();
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
            cancelButton.onClick.RemoveListener(ToChooseSeed);

            if (ZoomableBg.Instance != null)
                ZoomableBg.Instance.OnZoomEnded -= ToTreeScreen;
        }
    }
}
