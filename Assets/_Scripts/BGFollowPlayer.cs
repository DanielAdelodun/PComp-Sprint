using UnityEngine;

public class BGFollowPlayer : MonoBehaviour
{
    public Transform player;  
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector2(player.position.x, transform.position.y);
    }
}
