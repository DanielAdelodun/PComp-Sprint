using UnityEngine;
using FirstGearGames.SmoothCameraShaker; // Make sure the camera shake library is correctly imported

public class Shaking : MonoBehaviour
{
    public ShakeData explosionShakeData;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player hit the sphere! Camera shake should start.");
        CameraShakerHandler.Shake(explosionShakeData);
    }
}

}
