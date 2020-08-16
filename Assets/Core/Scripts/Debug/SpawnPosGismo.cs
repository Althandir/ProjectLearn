using UnityEngine;

public class SpawnPosGismo : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0, 1f, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 2f, 1));
    }
}
