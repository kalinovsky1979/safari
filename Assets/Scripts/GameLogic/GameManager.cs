using Assets.Scripts.EventBusLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
	[SerializeField] private GameScoresManager gameScoresManager;
	[SerializeField] private CountdownTimer cdTimer;
	[SerializeField] private int ApplesGoal;

	EventBus eventBus;
	public event EventHandler GameOver;

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		cdTimer.Expired += CdTimer_Expired;
	}

	private void CdTimer_Expired(object sender, EventArgs e)
	{

	}

	public void PlayGame()
	{
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
}
