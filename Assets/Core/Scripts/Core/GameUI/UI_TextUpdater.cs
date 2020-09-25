using UnityEngine;
using TMPro;

namespace GameUI
{ 
    public class UI_TextUpdater : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _text;

        protected virtual void Start()
        {
            Debug.LogWarning("Baseclass UI_TextUpdater used!");
        }
    }
}
