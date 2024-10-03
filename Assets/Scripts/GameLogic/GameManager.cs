using Assets.Scripts.EventBusLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
	[SerializeField] private AnimalManager animalManager;
	[SerializeField] private AnimalEnvironmentManager environmentManager;
	[SerializeField] private QuestionTable questionTable;
	[SerializeField] private TextMeshProUGUI questText;
	[SerializeField] private string[] cheeringStrings;

	public event EventHandler GameOver;

	private AudioSource audioSource;

	private RandomList<string> cheeringStringsRandomList;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		cheeringStringsRandomList = new RandomList<string>(cheeringStrings);

		environmentManager.PicturePressed += EnvironmentManager_PicturePressed;
		StartCoroutine(startingCoroutine());
	}

	private bool answerIsProcessing = false;

	private IEnumerator startingCoroutine()
	{
		//questionTable.HideTable();
		yield return StartCoroutine(nextAnimal());
	}

	private void EnvironmentManager_PicturePressed(object sender, string e)
	{
		if (answerIsProcessing) return;

		answerIsProcessing = true;

		if (animalManager.AnimalCurrent.AnimalInfo.AnimalName.Equals(e))
		{
			StartCoroutine(rightAnswer());
		}
		else
		{
			StartCoroutine(wrongAnswer(e));
		}
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
		//if(transition) return;

		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	StartCoroutine(rightAnswer());
		//}
	}

	private bool transition = false;

	private IEnumerator rightAnswer()
	{
		transition = true;

		//yield return StartCoroutine(questionTable.HideTableAnimate());

		questText.text = cheeringStringsRandomList.Next();

		//yield return StartCoroutine(questionTable.ShowTableAnimate());

		environmentManager.HideAllPictures();
		yield return StartCoroutine(environmentManager.ShowEnvironment(animalManager.AnimalCurrent.AnimalInfo.AnimalName));

		yield return new WaitForSeconds(10);

		//yield return StartCoroutine(questionTable.HideTableAnimate());

		yield return StartCoroutine(environmentManager.HideEnvironment(animalManager.AnimalCurrent.AnimalInfo.AnimalName));
		environmentManager.ShowAllPictures();

		yield return StartCoroutine(nextAnimal());

		answerIsProcessing = false;
		transition = false;
	}

	private IEnumerator wrongAnswer(string animName)
	{
		transition = true;

		questText.text = "NOT QUITE, TRY AGAIN!";

		yield return StartCoroutine(questionTable.Shake());

		environmentManager.HideAllPictures();
		yield return StartCoroutine(environmentManager.ShowEnvironment(animName));

		yield return new WaitForSeconds(3);

		yield return StartCoroutine(environmentManager.HideEnvironment(animName));
		environmentManager.ShowAllPictures();

		questText.text = $"WHERE DOES {animalManager.AnimalCurrent.AnimalInfo.AnimalName} LIVE?";

		answerIsProcessing = false;
		transition = false;
	}

	private IEnumerator nextAnimal()
	{
		transition = true;
		yield return StartCoroutine(animalManager.AwayAnimal());
		yield return StartCoroutine(animalManager.NextAnimal());
		questText.text = $"WHERE DOES {animalManager.AnimalCurrent.AnimalInfo.AnimalName} LIVE?";
		//yield return StartCoroutine(questionTable.ShowTableAnimate());
		transition = false;
	}
}
