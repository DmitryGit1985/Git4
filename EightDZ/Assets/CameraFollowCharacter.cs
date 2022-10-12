using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCharacter : MonoBehaviour
{
    [SerializeField] private CharacterController2D characterController2D;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(characterController2D.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
