using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDisolver : MonoBehaviour
{

	public static ScreenDisolver fadeinstance;

	[SerializeField] private CanvasGroup _canvasgroup;
	[SerializeField] private bool fadein;
	[SerializeField] private bool fadeout;


	private void Awake()
	{
		if(fadeinstance == null)
		{
			fadeinstance = this;
		}
	}


	private void Update()
	{
		if (fadein)
		{
			if(_canvasgroup.alpha < 1)
			{
				_canvasgroup.alpha += Time.deltaTime;

				if(_canvasgroup.alpha >= 1)
				{
					fadein = false;
				}
			}
		}

		if (fadeout)
		{
			if (_canvasgroup.alpha >= 0)
			{
				_canvasgroup.alpha -= Time.deltaTime;

				if (_canvasgroup.alpha == 0)
				{
					fadeout = false;
				}
			}
		}
	}


	public void FadeIN()
	{
		fadein = true;
	}

	public void FadeOut()
	{
		fadeout = true;
	}
}
