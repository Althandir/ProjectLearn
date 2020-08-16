using UnityEngine;

namespace Core.TargetGate
{
    public class TargetGate : MonoBehaviour
    {
        static TargetGate s_TargetGate;

        public static TargetGate Reference { get => s_TargetGate; }

        private void Awake()
        {
            CreateSingleton();
        }

        private void OnDestroy()
        {
            DestroySingleton();
        }

        #region SingletonHandling
        private void CreateSingleton()
        {
            if (!s_TargetGate)
            {
                s_TargetGate = this;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void DestroySingleton()
        {
            if (s_TargetGate == this.transform)
            {
                s_TargetGate = null;
            }
        }
        #endregion
    }

}
