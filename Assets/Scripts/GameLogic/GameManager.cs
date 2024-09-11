using Assets.Scripts.EventBusLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
	[SerializeField] private AnimalManager animalManager;

	public event EventHandler GameOver;

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void CdTimer_Expired(object sender, EventArgs e)
	{

	}

	public void PlayGame()
	{

	}

	public void PauseGame()
	{
		throw new NotImplementedException();
	}

	public void ResumeGame()
	{
		throw new NotImplementedException();
	}

	public void StopGame()
	{
		throw new NotImplementedException();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(nextQuestion());
		}
	}

	private IEnumerator nextQuestion()
	{
		yield return StartCoroutine(animalManager.NextQuest());
		yield return StartCoroutine(animalManager.ShowAnimals());
	}
}
