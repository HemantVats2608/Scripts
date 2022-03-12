using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragHandler : MonoBehaviour
{
	[SerializeField] private bool isdragging;
	[SerializeField] private Vector3 initialpos;
	private AudioSource audiosource;
	//[SerializeField] private AudioClip pickup, drop;
	private Vector2 offset;
	[SerializeField] public bool isplaced;
	public GameObject droplocation;

	private void Start()
	{
		initialpos = transform.position;
		audiosource = GetComponent<AudioSource>();
		droplocation.SetActive(false);
	}

	private void OnMouseDown()
	{
		Debug.Log("On mouse down");
		isdragging = true;
		if (!isplaced)
		{
			droplocation.SetActive(true);
		}
		
		offset = Getmouseposition() - (Vector2)transform.position;
	//	audiosource.PlayOneShot(pickup);
	}


	private void Update()
	{
		if (isplaced) return;
		if (!isdragging) return;
		Debug.Log("Update method");
		var mouseposition = Getmouseposition();
		transform.position = mouseposition - offset;
	}

	private void OnMouseUp()
	{
		Debug.Log("On mouse up");
		var dropdistance = Vector2.Distance(transform.position, droplocation.transform.position);

		if(dropdistance < 2.5)
		{
			transform.position = droplocation.transform.position;
			transform.localScale = droplocation.transform.localScale;
			droplocation.SetActive(false);
			isplaced = true;
		}
		else
		{
			isdragging = false;
			isplaced = false;
			//transform.position = initialpos;
			transform.DOMove(initialpos, 0.5f);
		}
		droplocation.SetActive(false);
		//	audiosource.PlayOneShot(drop);
	}

	Vector2 Getmouseposition()
	{
		Debug.Log("Get mouse position");
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}



}
