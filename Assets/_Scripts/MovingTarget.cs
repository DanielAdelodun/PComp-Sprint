using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [Range(1, 100)]
    public float movementSpeed;
    [Range(1, 100)]
    public float radius;
    private float angle;
    private AudioSource hitSound;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        angle += movementSpeed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius * 0.5f;
        float z = transform.position.z;

        transform.position = new Vector3(x, y + 2.5f, z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
    }
}
