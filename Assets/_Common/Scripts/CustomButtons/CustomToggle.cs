using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.GabrielBernabeu.Common.CustomButtons {
    public delegate void CustomToggleEventHandler(bool isActive);
    public class CustomToggle : MonoBehaviour, IPointerUpHandler
    {
        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            protected set
            {
                if (_isActive == value)
                    return;

                _isActive = value;
                OnValueChangedInvocation();
            }
        }
        private bool _isActive = false;

        public event CustomToggleEventHandler OnValueChanged;

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            IsActive = !IsActive;
        }

        protected virtual void OnValueChangedInvocation()
        {
            OnValueChanged?.Invoke(IsActive);
        }

        private void OnDestroy()
        {
            OnValueChanged = null;
        }
    }
}
