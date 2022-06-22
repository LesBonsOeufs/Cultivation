///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/06/2022 17:40
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.GabrielBernabeu.Cultivation {
    public class Titlecard : MonoBehaviour
    {
        [SerializeField] private Animator backgroundAnimator;

        private void Update()
        {
            if (Input.GetMouseButton(0))
                ToChooseSeedScreen();
        }

        private void ToChooseSeedScreen()
        {

        }
    }
}
