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
using System;
using Com.GabrielBernabeu.Common;

namespace Com.GabrielBernabeu.Cultivation 
{
    public class TreeScreen : MonoBehaviour
    {
        [SerializeField] private TaskTree taskTree = default;
        [SerializeField] private TextMeshProUGUI taskNameTmp = default;
        [SerializeField] private float fadeDuration = 0.6f;

        [Header("Buttons")]
        [SerializeField] private Button resetBtn = default;
        [SerializeField] private Button taskCompletedBtn = default;

        private CanvasGroup canvasGroup;
        private LocalData loadedData;

        public bool IsDayCorrect
        {
            get
            {
                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        if (loadedData.monday)
                            return true;
                        break;
                    case DayOfWeek.Tuesday:
                        if (loadedData.tuesday)
                            return true;
                        break;
                    case DayOfWeek.Wednesday:
                        if (loadedData.wednesday)
                            return true;
                        break;
                    case DayOfWeek.Thursday:
                        if (loadedData.thursday)
                            return true;
                        break;
                    case DayOfWeek.Friday:
                        if (loadedData.friday)
                            return true;
                        break;
                    case DayOfWeek.Saturday:
                        if (loadedData.saturday)
                            return true;
                        break;
                    case DayOfWeek.Sunday:
                        if (loadedData.sunday)
                            return true;
                        break;
                }

                return false;
            }
        }

        public bool WasPressedToday
        {
            get
            {
                return DateTime.Now.ToShortDateString() == loadedData.lastTaskDoneDate;
            }
        }

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
            taskCompletedBtn.onClick.AddListener(OnTaskCompletedButton);

            gameObject.SetActive(false);
        }

        private void OnResetButton()
        {
            LocalDataSaving.DeleteData();
            SceneManager.LoadScene(0);
        }

        private void OnTaskCompletedButton()
        {
            if (IsDayCorrect)
            {
                if (!WasPressedToday)
                {
                    Debug.Log("well done!");
                    loadedData.lastTaskDoneDate = DateTime.Now.ToShortDateString();
                    LocalDataSaving.SaveData(loadedData);
                }
                else
                {
                    Debug.Log("Button already pressed today!");
                    TextFeedbackMaker.Instance.CreateText("Button already pressed today!", Color.red, 1f, Color.red, 1f, 0f, 1f, Color.black,
                                                      3f, 2f, true, Camera.main.transform.position, new Vector2(3f, 1f));
                }
            }
            else
            {
                Debug.Log("Incorrect day!");
                TextFeedbackMaker.Instance.CreateText("Incorrect day!", Color.red, 1f, Color.red, 1f, 0f, 1f, Color.black,
                                                      3f, 2f, true, Camera.main.transform.position, new Vector2(3f, 1f));
            }
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

            loadedData = data;
            taskTree.Init();
            taskNameTmp.text = loadedData.taskName;
            taskTree.Type = loadedData.seedType;
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            resetBtn.onClick.RemoveListener(OnResetButton);
            taskCompletedBtn.onClick.RemoveListener(OnTaskCompletedButton);
            Instance = null;
        }
    }
}
