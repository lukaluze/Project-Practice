using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Gamemagager : MonoBehaviour
{
    public Transform prefab;
    int frame;
    bool spawn = false;
    // Start is called before the first frame update

    void Start()
    {
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn == false)
        {
            frame += 1;

            if (frame >= 400)
            {
                newObject();
                spawn = true;
            }
        }
        
    }

    public void newObject()
    {
        Instantiate(prefab, new Vector3(0, 3, 0), Quaternion.identity);
    }
}
