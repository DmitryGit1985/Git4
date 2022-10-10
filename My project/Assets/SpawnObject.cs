using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] float speed = 5.0f;
    private Vector3 lastVelocity;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Vector3 position = new Vector3(Random.Range(-8.0f, 8.0f), 2, Random.Range(-8.0f, 8.0f));
        rigidBody.velocity = position * speed;
        //Как просчитать размер где можно спауниться правильно?
    }
    private void FixedUpdate()
    {
        lastVelocity = rigidBody.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        rigidBody.velocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
        if(rigidBody.gameObject.layer== Spawn.colorLayer)
        {
            RandomColor();
        }
        if (rigidBody.gameObject.layer == Spawn.sizeLayer)
        {
            RandomSize();
        }
        if (rigidBody.gameObject.layer == Spawn.formLayer)
        {
            RandomForm();
        }
        #region test collision
        if (collision.gameObject.tag == "SpawnObject")
        {
            Debug.Log("Bad");
        }
        #endregion
    }
    private void RandomColor()
    {
        rigidBody.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    private void RandomSize()
    {
        int scale = Random.Range(1, 4);
        rigidBody.transform.localScale = new Vector3(scale, scale, scale);
    }
    private void RandomForm()
    {

    }
}
