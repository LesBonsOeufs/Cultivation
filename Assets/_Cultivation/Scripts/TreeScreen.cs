///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 24/06/2022 18:05
///-----------------------------------------------------------------

using Com.GabrielBernabeu.Common.DataManagement;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.GabrielBernabeu.Cultivation 
{
    public class TreeScreen : MonoBehaviour
    {
        [SerializeField] private TaskTree taskTree = default;
        [SerializeField] private TextMeshProUGUI taskNameTmp = default;
        [SerializeField] private float fadeDuration = 0.6f;
        [SerializeField] private Button resetBtn = default;

        private CanvasGroup canvasGroup;

        public static TreeScreen Instance { get; private set; }

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

            resetBtn.onClick.AddListener(OnResetButton);

            gameObject.SetActive(false);
        }

        private void OnResetButton()
        {
            LocalDataSaving.DeleteData();
            SceneManager.LoadScene(0);
        }

        public void In()
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(1f, fadeDuration);
            canvasGroup.blocksRaycasts = true;
            taskTree.Show();
        }

        public void Load(LocalData data)
        {
            Debug.Log($"{data.monday} {data.tuesday} {data.wednesday} {data.thursday} " +
                      $"{data.friday} {data.saturday} {data.sunday}");
            taskTree.Init();
            taskNameTmp.text = data.taskName;
            taskTree.Type = data.seedType;
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            resetBtn.onClick.RemoveListener(OnResetButton);
            Instance = null;
        }
    }
}
