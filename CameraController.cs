using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 cameraPosition = transform.position;

        cameraPosition.x = playerPosition.x;
        cameraPosition.y = playerPosition.y;

        transform.position = cameraPosition;
    }
}
