using Assets.Scripts.EventBusLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager, IEventBusParticipant
{
	[SerializeField] private GameScoresManager gameScoresManager;
	[SerializeField] private CountdownTimer cdTimer;
	[SerializeField] private int ApplesGoal;

	EventBus eventBus;
	public event EventHandler GameOver;

	private AudioSource audioSource;

	private bool gameIsAlreadyOver = false;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		cdTimer.Expired += CdTimer_Expired;
	}

	private void CdTimer_Expired(object sender, EventArgs e)
	{
		if(!gameIsAlreadyOver) GameOver?.Invoke(this, EventArgs.Empty);
	}

	public void PlayGame()
	{
		gameIsAlreadyOver = false;
		gameScoresManager.ResetScores();
		cdTimer.Go();
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

	public void Bind(EventBus bus)
	{
		eventBus = bus;

		eventBus.MoreApplesFound.AddListener(AddApples);
	}

	private void AddApples(int a)
	{
		if(gameIsAlreadyOver) return;

		gameScoresManager.AddScore(a);

		if (ApplesGoal <= gameScoresManager.Scores)
		{
			gameIsAlreadyOver = true;
			GameOver?.Invoke(this, EventArgs.Empty);
		}
	}
}
