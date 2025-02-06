using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject projectile;
    [Range(1, 1000)]
    public float launchSpeed;

    public void Fire(Vector2 MousePosition)
    {
        Vector3 position = Camera.main.transform.position;
        position.z += 1.0f;
        GameObject newProjectile = Instantiate(projectile, position, Quaternion.identity);
        
        Ray ray = Camera.main.ScreenPointToRay(MousePosition);
        Vector3 direction = ray.direction.normalized;
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.AddForce(direction * launchSpeed);

        Destroy(newProjectile, 5.0f);
    }
}
