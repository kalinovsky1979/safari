using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTable : MonoBehaviour
{
	AnimateVector3 animateScale;

	AnimateShakeVector3 animateShakeVector3;

	float amplitude = 0.4f;
	float frequency = 3.0f;
	float damping = 2f;

	private void Start()
	{
		animateScale = new AnimateVector3();
		animateShakeVector3 = new AnimateShakeVector3();
		animateShakeVector3.points = (t) =>
		{
			float decayedAmplitude = amplitude * Mathf.Exp(-damping * t);

			var r = new Vector3(
					1 + decayedAmplitude * amplitude * Mathf.Sin(t * Mathf.PI * 2f * frequency), 
					1 + decayedAmplitude * amplitude * Mathf.Sin(t * Mathf.PI * 2f * frequency),
					1 + decayedAmplitude * amplitude * Mathf.Sin(t * Mathf.PI * 2f * frequency));

			//Debug.Log($"{t} : {r}");

			return r;
		};
		animateShakeVector3.duration = 0.3f;
		animateShakeVector3.OnAnimationStep = (scale) =>
		{
			transform.localScale = scale;
		};
	}

	public void HideTable()
	{
		transform.localScale = Vector3.zero;
	}

	public IEnumerator ShowTableAnimate()
	{
		yield return StartCoroutine(scaleAnimation(Vector3.zero, new Vector3(1, 1, 1)));
	}

	public IEnumerator HideTableAnimate()
	{
		yield return StartCoroutine(scaleAnimation(new Vector3(1, 1, 1), Vector3.zero));
	}

	private bool animating = false;
	private IEnumerator scaleAnimation(Vector3 a, Vector3 b)
	{
		if(animating) yield break;
		animating = true;

		animateScale.valueA = a;
		animateScale.valueB = b;
		animateScale.duration = 0.3f;
		animateScale.OnAnimationStep = (scale) =>
		{
			transform.localScale = scale;
		};

		while(animateScale.update())
			yield return null;

		animating = false;
	}

	public IEnumerator Shake()
	{
		while(animateShakeVector3.update())
			yield return null;
	}
}
