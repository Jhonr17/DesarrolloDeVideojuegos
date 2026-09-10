using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform target; // El objetivo que la cámara seguirá

    void LateUpdate()
    {
        if (target != null)
        {
            // Actualiza la posición de la cámara para seguir al objetivo
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }



}
