using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEnvironment : MonoBehaviour
{
	[SerializeField] private AnimalInfo animalInfo;
	public AnimalInfo AnimalInfo => animalInfo;

	private AnimateVector3 scaleAnimation;

	private void Start()
	{
		scaleAnimation = new AnimateVector3();
		scaleAnimation.duration = 1f;
		scaleAnimation.OnAnimationStep = (scale) =>
		{
			transform.localScale = scale;
		};
	}

	public IEnumerator Show()
	{
		scaleAnimation.valueA = Vector3.zero;
		scaleAnimation.valueB = Vector3.one;

		while(scaleAnimation.update())
			yield return null;
	}

	public IEnumerator Hide()
	{
		scaleAnimation.valueA = Vector3.one;
		scaleAnimation.valueB = Vector3.zero;

		while (scaleAnimation.update())
			yield return null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 0.2f);
	}
}
