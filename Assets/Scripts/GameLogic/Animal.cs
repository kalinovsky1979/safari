using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class Animal : MonoBehaviour
{
	[SerializeField] private AnimalInfo animalInfo;
	public AnimalInfo AnimalInfo => animalInfo;

	private AnimateVector3 animateScale;
	private AnimateVector3 moveAnimal;

	private Vector3 originalScale;

	// Start is called before the first frame update
	void Start()
	{
		moveAnimal = new AnimateVector3();
		moveAnimal.duration = 1.0f;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnEnable()
	{
		originalScale = transform.localScale;
	}

	public IEnumerator Move(Vector3 target)
	{
		moveAnimal.valueA = transform.position;
		moveAnimal.valueB = target;
		moveAnimal.OnAnimationStep = (pos) =>
		{
			transform.position = pos;
		};

		while(moveAnimal.update(null))
			yield return null;
	}

	public IEnumerator AppearCoroutine(Vector3 pos)
	{
		transform.position = pos;

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
