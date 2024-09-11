using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
	[SerializeField] private Transform animalPointA;
	[SerializeField] private Transform animalPointB;

	[SerializeField] private AnimalThinker[] animalPrefabs;

	private AnimalThinker[] animalsInstances = null;
	private List<AnimalThinker> selectedAnimals;

	private AnimalThinker animalDropped;

	private Vector3[] animalPositions;

	// Start is called before the first frame update
	void Start()
	{
		animalsInstances = null;
		animalPositions = null;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public IEnumerator HideAnimalsExceptDropped()
	{
		yield return null;
	}

	public IEnumerator HideDroppedAnimal()
	{
		yield return null;
	}

	private bool animalsShown = false;

	public IEnumerator ShowAnimals()
	{
		if(animalsShown) yield break;

		animalsShown = true;

		int iPoint = 0;
		foreach (var animal in selectedAnimals)
		{
			yield return StartCoroutine(animal.AppearCoroutine(animalPositions[iPoint]));
			iPoint++;
		}
	}

	public IEnumerator NextQuest()
	{
		newAnimalsSelection();

		yield return null;
	}

	private void newAnimalsSelection()
	{
		if (animalPositions == null)
			animalPositions = calculatePositionsBetween(animalPointA.position, animalPointB.position, 5);

		if (animalsInstances == null)
		{
			animalsInstances = new AnimalThinker[animalPrefabs.Length];

			int iA = 0;
			foreach (var animal in animalPrefabs)
			{
				animalsInstances[iA] = Instantiate(animal);
				animalsInstances[iA].gameObject.SetActive(false);
				iA++;
			}
		}
		
		if (selectedAnimals == null)
			selectedAnimals = new List<AnimalThinker>();

		selectedAnimals.Clear();

		for (int i = animalsInstances.Length - 1; i > 0; i--)
		{
			int randomIndex = UnityEngine.Random.Range(0, i + 1);
			var temp = animalsInstances[i];
			animalsInstances[i] = animalsInstances[randomIndex];
			animalsInstances[randomIndex] = temp;
		}

		selectedAnimals = animalsInstances.Take(5).ToList();

		animalDropped = selectedAnimals[Random.Range(0, 5)];

		//int iPoint = 0;
		//foreach (var animal in selectedAnimals)
		//{
		//	// Better make a method like animal.MakeInvisible()
		//	animal.gameObject.transform.position = animalPositions[iPoint];
		//	animal.gameObject.transform.localScale = new Vector3(0, 0, 0);
		//	iPoint++;
		//}
	}

	private Vector3[] calculatePositionsBetween(Vector3 a, Vector3 b, int n)
	{
		if (n < 2)
			throw new System.Exception("Number of points must be at least 2.");

		Vector3[] points = new Vector3[n];
		for (int i = 0; i < n; i++)
		{
			float t = (float)i / (n - 1);
			points[i] = Vector3.Lerp(a, b, t);
		}

		return points;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(animalPointA.position, 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(animalPointB.position, 0.1f);
	}
}
