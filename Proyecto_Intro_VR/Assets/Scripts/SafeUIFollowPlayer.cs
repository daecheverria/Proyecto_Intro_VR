using UnityEngine;

public class SafeUIFollowPlayer : MonoBehaviour
{
    [Header("Referencia a la cámara del jugador")]
    public Transform playerCamera;

    [Header("Offset de posición respecto al jugador")]
    public Vector3 offset = new(0, -0.2f, 1.5f); // Frente y ligeramente abajo

    void OnEnable()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera no asignada.");
            return;
        }

        // Reposicionar el canvas delante del jugador
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * offset.z + playerCamera.up * offset.y + playerCamera.right * offset.x;
        transform.position = targetPosition;

        // Hacer que mire hacia el jugador
        transform.LookAt(playerCamera);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Solo rotación en Y
    }
}



