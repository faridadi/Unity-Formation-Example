using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    Vector3 newPosition;
    SteerSeekArrive formation;
    // Start is called before the first frame update
    void Start()
    {
        formation = this.GetComponent<SteerSeekArrive>();
        //newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                //transform.position = newPosition;
                formation._target.transform.position = newPosition;
            }
        }
    }
}
