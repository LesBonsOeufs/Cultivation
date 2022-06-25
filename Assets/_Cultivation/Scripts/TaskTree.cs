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

        private Transform pot;
        private Transform tree;

        public SeedType? Type
        {
            get
            {
                return _type;
            }

            set
            {
                
            }
        }
        private SeedType? _type = null;

        private void Awake()
        {
            Hide();
        }

        public void Init(SeedType type)
        {
            if (pot != null)
                Destroy(pot.gameObject);

            pot = Instantiate(Resources.Load(MODELS_PATH + POT, typeof(GameObject)) as GameObject, transform).transform;

            if (tree != null)
                Destroy(tree.gameObject);

            string pathSuffix = "";

            switch (type)
            {
                case SeedType.SPORT:
                    pathSuffix = SPORT_SUFFIX;
                    break;
                case SeedType.RESTING:
                    pathSuffix = RESTING_SUFFIX;
                    break;
                case SeedType.SOCIAL:
                    pathSuffix = SOCIAL_SUFFIX;
                    break;
                case SeedType.LEARNING:
                    pathSuffix = LEARNING_SUFFIX;
                    break;
            }

            tree = Instantiate(Resources.Load(
                MODELS_PATH + STEP_PREFIX + "1" + pathSuffix, typeof(GameObject)) as GameObject, transform).transform;
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
