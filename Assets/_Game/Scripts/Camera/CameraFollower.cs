using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;
    
    private Vector3 distance;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        distance = target.position - transform.position;
        player = target.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.y >= 0)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newDistance = new Vector3(distance.x * player.scale, distance.y * player.scale, distance.z * player.scale);

        Vector3 newPosiotion = target.position - newDistance;

        transform.position = Vector3.Lerp(currentPosition, newPosiotion, smooth * Time.deltaTime);
    }
}
