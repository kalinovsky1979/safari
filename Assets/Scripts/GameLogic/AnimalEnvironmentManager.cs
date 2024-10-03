using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimalEnvironmentManager : MonoBehaviour
{
	[SerializeField] private AnimalEnvironmentPicture[] animalEnvironmentPictures;
	[SerializeField] private AnimalEnvironment[] animalEnvironments;
	[SerializeField] private float onePictureMoveDuration = 30;
	[SerializeField] private Transform pointA;
	[SerializeField] private Transform pointC;
	[SerializeField] private Transform environmentPoint;

	private AnimateVector3 moveSlideAnimator;

	public event EventHandler<string> PicturePressed;

	public bool MoveSlidePictures { get; private set; } = true;

	// Start is called before the first frame update
	void Start()
	{
		moveSlideAnimator = new AnimateVector3();
		StartCoroutine(runSlideShow());
	}

	private void Awake()
	{
		environmentPictureSpowner = pointA.gameObject.GetComponent<EnvironmentPictureSpowner>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PicturePress(string pictureName)
	{
		PicturePressed?.Invoke(this, pictureName);
	}

	public void HideAllPictures()
	{
		foreach (var anim in animalEnvironmentPictures)
		{
			anim.Hide();
		}
		MoveSlidePictures = false;
	}

	public void ShowAllPictures()
	{
		foreach (var anim in animalEnvironmentPictures)
		{
			anim.Show();
		}
		MoveSlidePictures = true;
	}

	public IEnumerator ShowEnvironment(string envName)
	{
		var env = animalEnvironments.FirstOrDefault(x => x.AnimalInfo.AnimalName.Equals(envName));

		env.transform.localScale = Vector3.zero;
		env.transform.position = environmentPoint.position;

		yield return StartCoroutine(env.Show());
	}

	public IEnumerator HideEnvironment(string envName)
	{
		var env = animalEnvironments.FirstOrDefault(x => x.AnimalInfo.AnimalName.Equals(envName));

		env.transform.localScale = Vector3.zero;
		env.transform.position = environmentPoint.position;

		yield return StartCoroutine(env.Hide());
	}

	private EnvironmentPictureSpowner environmentPictureSpowner;

	private IEnumerator runSlideShow()
	{
		while (true)
		{
			if (!MoveSlidePictures)
			{
				yield return null;
				continue;
			}

			if (!environmentPictureSpowner.Busy)
			{
				var a = animalEnvironmentPictures.Where(a => !a.IsGoing).ToArray();
				var picture = a[UnityEngine.Random.Range(0, a.Length)];
				picture.Go(pointA.position, pointC.position, onePictureMoveDuration);
			}

			yield return null;
		}
	}
}
