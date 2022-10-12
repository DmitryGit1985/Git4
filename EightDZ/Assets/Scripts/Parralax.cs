using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] private Vector2 pararalaxEffectMultiplyer;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x*pararalaxEffectMultiplyer.x, deltaMovement.y*pararalaxEffectMultiplyer.y, transform.position.z);
        lastCameraPosition = cameraTransform.position;
        float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;

        if (Mathf.Abs(cameraTransform.position.x-transform.position.x) >= textureUnitSizeX)
        {
            transform.position = new Vector3(cameraTransform.position.x+ offsetPositionX, transform.position.y, transform.position.z);
        }
    }
}
