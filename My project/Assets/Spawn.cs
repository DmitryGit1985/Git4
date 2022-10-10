using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    public readonly static int colorLayer = 6;
    public readonly static int sizeLayer = 7;
    public readonly static int formLayer = 8;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int objectType=Random.Range(0, 3);
            GameObject newSpawnObject =Instantiate(spawnObject, transform.position, Quaternion.identity);
            if (objectType == (int)ObjectTypes.Color)
            {
                newSpawnObject.layer = colorLayer;
            }
            if (objectType == (int)ObjectTypes.Size)
            {
                newSpawnObject.layer = sizeLayer;
            }
            if (objectType == (int)ObjectTypes.Form)
            {
                newSpawnObject.layer = formLayer;
            }
        }
    }
}
