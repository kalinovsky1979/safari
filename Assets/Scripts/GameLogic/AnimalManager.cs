using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
	//[SerializeField] private Transform animalPointA;
	//[SerializeField] private Transform animalPointB;

	[SerializeField] private Transform animalPositionBase;
	[SerializeField] private Transform animalPositionScene;

	[SerializeField] private Animal[] animals;

	private Animal[] animalsInstances = null;
	private List<Animal> selectedAnimals;

	private Animal animalCurrent;
	public Animal AnimalCurrent => animalCurrent;

	private Vector3[] animalPositions;

	private RandomList<Animal> animalRandomList;

	// Start is called before the first frame update
	void Start()
	{
		animalsInstances = null;
		animalPositions = null;

		animalRandomList = new RandomList<Animal>(animals
			.Select(a => { a.transform.position = animalPositionBase.position; a.transform.rotation = Quaternion.identity; return a; })
			.ToArray(), canRepeatTwice: false);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public IEnumerator NextAnimal()
	{
		if (trasiting) yield break;
		if (animalsShown) yield break;

		trasiting = true;
		animalsShown = true;
		animalCurrent = animalRandomList.Next();
		animalCurrent.gameObject.transform.position = animalPositionBase.position;

		yield return StartCoroutine(animalCurrent.Move(animalPositionScene.position));

		trasiting = false;
	}

	public IEnumerator AwayAnimal()
	{
		if(trasiting) yield break;
		if(!animalsShown) yield break;
		trasiting = true;
		animalsShown = false;
		yield return StartCoroutine(animalCurrent.Move(animalPositionBase.position));
		trasiting = false;
	}

	//public IEnumerator HideAnimalRow()
	//{
	//	if(!animalsShown) yield break;
	//	animalsShown = false;

	//	foreach (var animal in selectedAnimals)
	//	{
	//		yield return StartCoroutine(animal.DisappearCoroutine());
	//	}
	//}

	private bool animalsShown = false;
	private bool trasiting = false;

	//public IEnumerator ShowAnimals()
	//{
	//	if(animalsShown) yield break;

	//	animalsShown = true;

	//	int iPoint = 0;
	//	foreach (var animal in selectedAnimals)
	//	{
	//		yield return StartCoroutine(animal.AppearCoroutine(animalPositions[iPoint]));
	//		iPoint++;
	//	}
	//}

	//public IEnumerator NextAnimalRow()
	//{
	//	newAnimalsRow();

	//	yield return null;
	//}

	//private void newAnimalsRow()
	//{
	//	if (animalPositions == null)
	//		animalPositions = calculatePositionsBetween(animalPointA.position, animalPointB.position, 5);

	//	if (animalsInstances == null)
	//	{
	//		animalsInstances = new Animal[animalPrefabs.Length];

	//		int iA = 0;
	//		foreach (var animal in animalPrefabs)
	//		{
	//			animalsInstances[iA] = Instantiate(animal);
	//			animalsInstances[iA].gameObject.SetActive(false);
	//			iA++;
	//		}
	//	}
		
	//	if (selectedAnimals == null)
	//		selectedAnimals = new List<Animal>();

	//	selectedAnimals.Clear();

	//	for (int i = animalsInstances.Length - 1; i > 0; i--)
	//	{
	//		int randomIndex = Random.Range(0, i + 1);
	//		var temp = animalsInstances[i];
	//		animalsInstances[i] = animalsInstances[randomIndex];
	//		animalsInstances[randomIndex] = temp;
	//	}

	//	selectedAnimals = animalsInstances.Take(5).ToList();

	//	animalCurrent = selectedAnimals[Random.Range(0, 5)];
	//}

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
		Gizmos.DrawSphere(animalPositionBase.position, 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(animalPositionScene.position, 0.1f);
	}
}
