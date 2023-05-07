using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] objectsToPlace;
    public int numberOfObjects;
    public Vector3 tileSize;

    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 position = GetRandomPosition();
            GameObject objectToPlace = objectsToPlace[Random.Range(0, objectsToPlace.Length)];

            if (!CheckCollision(objectToPlace, position))
            {
                Instantiate(objectToPlace, position, Quaternion.identity);
            }
            else
            {
                Destroy(objectToPlace);
                i--;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(transform.position.x, transform.position.x + tileSize.x),
            Random.Range(transform.position.y, transform.position.y + tileSize.y),
            Random.Range(transform.position.z, transform.position.z + tileSize.z));
    }

    bool CheckCollision(GameObject objectToCheck, Vector3 position)
    {
        Collider[] colliders = Physics.OverlapBox(position, objectToCheck.transform.localScale / 2f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != objectToCheck && !collider.CompareTag("floor"))
            {
                return true;
            }
        }

        return false;
    }
}
