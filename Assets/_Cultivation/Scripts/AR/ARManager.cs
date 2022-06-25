///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 25/06/2022 22:16
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.GabrielBernabeu.Cultivation.AR {
    public class ARManager : MonoBehaviour
    {
        [SerializeField] private int baseSceneIndex = 0;
        [SerializeField] private int arSceneIndex = 1;

        public Transform Tree { get; private set; }

        public static ARManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void OpenAR(TaskTree tree)
        {
            tree.transform.SetParent(transform);
            Tree = tree.transform;
            SceneManager.LoadScene(arSceneIndex);
        }

        public void CloseAR()
        {
            SceneManager.LoadScene(baseSceneIndex);
        }

        private void OnDestroy()
        {
            if (Instance != this)
                return;

            Instance = null;
        }
    }
}
