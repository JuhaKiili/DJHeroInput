using UnityEngine;
using System.Collections;

public class DJHeroController : MonoBehaviour
{
	public int controllerIndex;

	public Sprite BlueButton;
	public Sprite BlueButtonDown;
	public Sprite RedButton;
	public Sprite RedButtonDown;
	public Sprite GreenButton;
	public Sprite GreenButtonDown;
	public Sprite Yeaboiee;
	public Sprite YeaboieeDown;
	public Sprite SmallButton;
	public Sprite SmallButtonDown;
	public Sprite DPad;
	public Sprite DPadUp;
	public Sprite DPadRight;
	public Sprite DPadDown;
	public Sprite DPadLeft;

	private Transform wheel;
	private Transform knob;	
	private Transform crossfader;
	private float crossfaderMinX;
	private float crossfaderMaxX;

	private SpriteRenderer blueSpriteRenderer;
	private SpriteRenderer greenSpriteRenderer;
	private SpriteRenderer redSpriteRenderer;
	private SpriteRenderer yeaboieeSpriteRenderer;
	private SpriteRenderer startSpriteRenderer;
	private SpriteRenderer backSpriteRenderer;
	private SpriteRenderer dPadSpriteRenderer;
	
	private DJHeroInputState state
	{
		get { return DJHeroInput.controllers[controllerIndex]; }
	}
	
	// Use this for initialization
	void Awake ()
	{
		wheel = transform.FindChild ("Wheel");
		knob = transform.FindChild("Knob");
		crossfader = transform.FindChild("Crossfader");

		blueSpriteRenderer = wheel.transform.FindChild ("Blue").GetComponent<SpriteRenderer> ();
		greenSpriteRenderer = wheel.transform.FindChild ("Green").GetComponent<SpriteRenderer>();
		redSpriteRenderer = wheel.transform.FindChild ("Red").GetComponent<SpriteRenderer>();
		yeaboieeSpriteRenderer = transform.FindChild("Yeaboiee").GetComponent<SpriteRenderer>();
		startSpriteRenderer = transform.FindChild("Start").GetComponent<SpriteRenderer>();
		backSpriteRenderer = transform.FindChild("Back").GetComponent<SpriteRenderer>();
		dPadSpriteRenderer = transform.FindChild("DPad").GetComponent<SpriteRenderer>();

		crossfaderMinX = transform.FindChild ("CrossfaderMin").transform.position.x;
		crossfaderMaxX = transform.FindChild ("CrossfaderMax").transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		wheel.eulerAngles = new Vector3 (0f, 0f, wheel.eulerAngles.z - state.WheelDelta);
		knob.eulerAngles = new Vector3(0f, 0f, -state.Knob);
		crossfader.transform.position = new Vector3 (crossfaderMinX + (state.Crossfader/2f + 0.5f) * (crossfaderMaxX - crossfaderMinX), crossfader.position.y, crossfader.position.z);

		blueSpriteRenderer.sprite = state.Blue ? BlueButtonDown : BlueButton;
		redSpriteRenderer.sprite = state.Red ? RedButtonDown : RedButton;
		greenSpriteRenderer.sprite = state.Green ? GreenButtonDown : GreenButton;
		yeaboieeSpriteRenderer.sprite = state.YeaBoiee ? YeaboieeDown : Yeaboiee;
		startSpriteRenderer.sprite = state.Start ? SmallButtonDown : SmallButton;
		backSpriteRenderer.sprite = state.Back ? SmallButtonDown : SmallButton;

		if (state.DPadUp)
			dPadSpriteRenderer.sprite = DPadUp;
		else if (state.DPadRight)
			dPadSpriteRenderer.sprite = DPadRight;
		else if (state.DPadDown)
			dPadSpriteRenderer.sprite = DPadDown;
		else if (state.DPadLeft)
			dPadSpriteRenderer.sprite = DPadLeft;
		else
			dPadSpriteRenderer.sprite = DPad;
			
	}
}
