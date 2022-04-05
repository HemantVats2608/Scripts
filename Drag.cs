using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    GameObject target;
    bool isMouseDrag;
    Vector3 screenPosition;
    Vector3 offset;
    public GameObject droplocation;
    bool isplaced;
    bool isdragging;
    Vector3 initialpos;
    public bool objectdropped;


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);

          

            if (target != null)
            {
                initialpos = target.transform.position;
                isMouseDrag = true;
                Debug.Log("target position :" + target.transform.position);
                //Convert world position to screen position.
                screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDrag = false;
            if(target != null)
			{
                ObjectPlaced();
            }
           
        }

        if (isMouseDrag)
        {
            //track mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

            //It will update target gameobject's current postion.
            target.transform.position = currentPosition;
        }

    }

    
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("droparea"))
		{
            Debug.Log("Drop location reached.");
            objectdropped = true;

        }
	}

	private void OnTriggerExit(Collider other)
	{
        objectdropped = false;
    }

	void ObjectPlaced()
	{
		if (objectdropped)
		{
            Debug.Log("Object placed..............");
            target.transform.position = droplocation.transform.position;
            target.GetComponent<BoxCollider>().enabled = false;
            droplocation.GetComponent<BoxCollider>().enabled = false;

        }
		else
		{
            target.transform.position = initialpos;
		}
	}


    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

}
