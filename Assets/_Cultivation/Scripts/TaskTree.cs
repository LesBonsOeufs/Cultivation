using UnityEngine;

namespace Com.GabrielBernabeu.Cultivation
{
    public class TaskTree : MonoBehaviour
    {
        //Resource finding constants
        private const string MODELS_PATH = "BlenderModels/";
        private const string POT = "Pot";
        private const string STEP_PREFIX = "Stade";
        private const string SPORT_SUFFIX = ".Sp";
        private const string RESTING_SUFFIX = ".R";
        private const string SOCIAL_SUFFIX = ".So";
        private const string LEARNING_SUFFIX = ".L";

        [SerializeField] private Transform _treeContainer = default;

        public Transform TreeContainer => _treeContainer;

        private Transform pot;
        private Transform tree;

        private void Awake()
        {
            Hide();
        }

        public void Init(SeedType type, int age)
        {
            if (pot != null)
                Destroy(pot.gameObject);

            pot = Instantiate(Resources.Load(MODELS_PATH + POT, typeof(GameObject)) as GameObject, TreeContainer).transform;

            if (tree != null)
                Destroy(tree.gameObject);

            string typeSuffix = "";

            switch (type)
            {
                case SeedType.SPORT:
                    typeSuffix = SPORT_SUFFIX;
                    break;
                case SeedType.RESTING:
                    typeSuffix = RESTING_SUFFIX;
                    break;
                case SeedType.SOCIAL:
                    typeSuffix = SOCIAL_SUFFIX;
                    break;
                case SeedType.LEARNING:
                    typeSuffix = LEARNING_SUFFIX;
                    break;
            }

            string stepSuffix = "";

            if (age <= 6)
                stepSuffix = "1";
            else if (age <= 14)
                stepSuffix = "2";
            else if (age <= 22)
                stepSuffix = "3";
            else if (age <= 30)
                stepSuffix = "4";
            else if (age <= 38)
                stepSuffix = "5";
            else if (age <= 46)
                stepSuffix = "6";
            else
                stepSuffix = "7";

            tree = Instantiate(Resources.Load(
                MODELS_PATH + STEP_PREFIX + stepSuffix + typeSuffix, typeof(GameObject)) as GameObject, TreeContainer).transform;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
