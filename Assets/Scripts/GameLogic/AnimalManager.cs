using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
	[SerializeField] private Transform animalPointA;
	[SerializeField] private Transform animalPointB;

	[SerializeField] private AnimalThinker[] animalPrefabs;

	private AnimalThinker[] animalPrefabsLocalClone;
	private List<AnimalThinker> selectedAnimals;

	private AnimalThinker animalDropped;

	// Start is called before the first frame update
	void Start()
	{
		
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

	public IEnumerator ShowAnimals()
	{
		yield return null;
	}

	public IEnumerator NextQuest()
	{
		newAnimalsSelection();

		yield return null;
	}

	private void newAnimalsSelection()
	{
		if(animalPrefabsLocalClone == null)
			animalPrefabsLocalClone = (AnimalThinker[])animalPrefabs.Clone();

		if(selectedAnimals == null)
			selectedAnimals = new List<AnimalThinker>();

		selectedAnimals.Clear();

		for (int i = animalPrefabsLocalClone.Length - 1; i > 0; i--)
		{
			int randomIndex = UnityEngine.Random.Range(0, i + 1);
			var temp = animalPrefabsLocalClone[i];
			animalPrefabsLocalClone[i] = animalPrefabsLocalClone[randomIndex];
			animalPrefabsLocalClone[randomIndex] = temp;
		}

		selectedAnimals = animalPrefabsLocalClone.Take(5).ToList();

		animalDropped = selectedAnimals[Random.Range(0, 5)];
	} 
}
