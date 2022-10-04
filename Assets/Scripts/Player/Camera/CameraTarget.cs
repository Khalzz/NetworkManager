using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class CameraTarget : NetworkBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] Transform player;
    [SerializeField] float treshold;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        vCam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (!IsOwner) return;

        Vector3 cursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + cursorPosition) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -treshold + player.position.x, treshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -treshold + player.position.y, treshold + player.position.y);

        this.transform.position = targetPos;

        vCam.Follow = gameObject.transform;
    }
}
