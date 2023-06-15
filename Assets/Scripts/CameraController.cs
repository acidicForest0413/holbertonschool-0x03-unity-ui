using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Ogg_Helpers;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    [SerializeField] Vector3 offset = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Abort("CameraController: player is null");
            return;
        }
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && offset != Vector3.zero)
            transform.position = player.transform.position + offset;
    }
}