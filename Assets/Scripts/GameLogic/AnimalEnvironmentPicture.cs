using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class AnimalEnvironmentPicture : MonoBehaviour
{
	[SerializeField] private AnimalInfo animalInfo;
	[SerializeField] private AnimalEnvironmentManager animalEnvironmentManager;
	public AnimalInfo AnimalInfo => animalInfo;
	private float moveDuration;
	private AnimateVector3 moveSlideAnimator;
	private AnimateVector3 scaleAnimator;

	private PressGesture pressGesture;

	private bool isGoing = false;
	public bool IsGoing => isGoing;

	// Start is called before the first frame update
	void Start()
	{
		//moveSlideAnimator = new AnimateVector3();
		//scaleAnimator = new AnimateVector3();
		//pressGesture = GetComponent<PressGesture>();
		//pressGesture.Pressed += PressGesture_Pressed;
	}

	private void Awake()
	{
		moveSlideAnimator = new AnimateVector3();
		scaleAnimator = new AnimateVector3();
		pressGesture = GetComponent<PressGesture>();
		pressGesture.Pressed += PressGesture_Pressed;
	}

	private void PressGesture_Pressed(object sender, System.EventArgs e)
	{
		animalEnvironmentManager.PicturePress(animalInfo.AnimalName);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void Go(Vector3 a, Vector3 b, float duration)
	{
		StartCoroutine(_go(a, b, duration));
	}

	public void Hide()
	{
		StartCoroutine(_hide(true));
	}

	public void Show()
	{
		StartCoroutine(_hide(false));
	}

	private IEnumerator _hide(bool hide)
	{
		if (hide)
		{
			scaleAnimator.valueA = Vector3.one;
			scaleAnimator.valueB = Vector3.zero;
			scaleAnimator.duration = 0.3f;
			scaleAnimator.OnAnimationStep = (sc) =>
			{
				transform.localScale = sc;
			};
		}
		else
		{
			scaleAnimator.valueA = Vector3.zero;
			scaleAnimator.valueB = Vector3.one;
			scaleAnimator.duration = 0.3f;
			scaleAnimator.OnAnimationStep = (sc) =>
			{
				transform.localScale = sc;
			};
		}

		while(scaleAnimator.update())
			yield return null;
	}

	private IEnumerator _go(Vector3 a, Vector3 b, float d)
	{
		if(isGoing) yield break;
		isGoing = true;

		moveSlideAnimator.duration = d;
		moveSlideAnimator.valueA = a;
		moveSlideAnimator.valueB = b;
		moveSlideAnimator.OnAnimationStep = (pos) =>
		{
			transform.position = pos;
		};

		var _updated = true;
		while(_updated)
		{
			if(animalEnvironmentManager.MoveSlidePictures)
				_updated = moveSlideAnimator.update(null);

			yield return null;
		}

		moveSlideAnimator.Stop();
		isGoing = false;
	}
}
