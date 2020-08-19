using UnityEngine;

namespace EditorExtras
{
    public class SpawnPosGizmo : MonoBehaviour
    {
        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}

