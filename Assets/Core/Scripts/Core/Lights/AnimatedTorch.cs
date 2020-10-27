using System.Collections;
using UnityEngine;

public class AnimatedTorch : MonoBehaviour
{
    [SerializeField] float _delay = 1;

    private void Start()
    {
        StartCoroutine(RotateTorch());
    }

    IEnumerator RotateTorch()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_delay);
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Reset()
    {
        _delay = 1;
    }
}
