using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInfo : MonoBehaviour
{
	[SerializeField] private string animalName;
	public string AnimalName => animalName;
}
