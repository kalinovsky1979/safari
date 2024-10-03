using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShakeVector3 : StaticStepBase
{
	public Func<float, Vector3> points;
	public float duration;

	public Action<Vector3> OnAnimationStep;

	private float elapsedTime = 0f;
	private bool isAnimating = false;
	private Vector3 currentValue;
	private int iPoint = 0;

	public override void Stop()
	{
		elapsedTime = 0;
		iPoint = 0;
		isAnimating = false;
	}

	public override bool update(ChainRunner prg = null)
	{
		if (!isAnimating)
		{
			elapsedTime = 0;
			isAnimating = true;
		}

		if (isAnimating)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / duration);

			if (points != null)
			{
				// If points is a function, use it to calculate the current value
				currentValue = points(t);
			}

			OnAnimationStep?.Invoke(currentValue);

			if (t >= 1f)
			{
				isAnimating = false;
				elapsedTime = 0;
				iPoint = 0;
				return false;
			}
		}

		return true;
	}
}
