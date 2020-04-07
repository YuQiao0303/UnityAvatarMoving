using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

	public float movespeed = 5;

public GameObject  go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.W))

        {

            go.transform.Translate( 0, 0, movespeed * Time.deltaTime, Space.World);

        }

        if (Input.GetKey(KeyCode.S))

        {

            go.transform.Translate( 0, 0, movespeed * Time.deltaTime * (-1),Space.World);

        }

        if (Input.GetKey(KeyCode.A))

        {

            go.transform.Translate(movespeed * Time.deltaTime*(-1), 0, 0,  Space.World);

        }

        if (Input.GetKey(KeyCode.D))

        {

            go.transform.Translate(movespeed * Time.deltaTime, 0, 0,  Space.World);

        }
    }
}
