using UnityEngine;

public class PS_DestroyOnDisable : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
