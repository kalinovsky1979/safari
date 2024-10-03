using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentPictureSpowner : MonoBehaviour
{
	private bool _busy = false;
	public bool Busy => _busy;

	private void OnTriggerEnter(Collider other)
	{
		_busy = true;
	}

	private void OnTriggerExit(Collider other)
	{
		_busy = false;
	}
}
