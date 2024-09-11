using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class AnimalThinker : MonoBehaviour
{

	private AnimateVector3 animateScale;

	private Vector3 originalScale;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public IEnumerator AppearCoroutine(Vector3 pos)
	{
		transform.position = pos;
		originalScale = transform.localScale;

		Debug.Log(transform.position);

		if (animateScale == null)
		{
			animateScale = new AnimateVector3();
			animateScale.OnAnimationStep = (x) =>
			{
				transform.localScale = x;
			};
		}

		animateScale.valueA = new Vector3(0, 0, 0);
		animateScale.valueB = originalScale;
		animateScale.duration = 0.5f;

		gameObject.SetActive(true);

		while (animateScale.update(null))
			yield return null;
	}

	public IEnumerator DisappearCoroutine()
	{
		animateScale.valueA = transform.localScale;
		animateScale.valueB = new Vector3(0, 0, 0);
		animateScale.duration = 0.3f;

		while (animateScale.update(null))
			yield return null;
	}
}
