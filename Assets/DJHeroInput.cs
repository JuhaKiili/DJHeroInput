using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class DJHeroInput : MonoBehaviour
{
    bool[] playerIndexSet;
    PlayerIndex[] playerIndex;
    GamePadState[] state;
    GamePadState[] prevState;

	private static DJHeroInputState[] s_DJHeroInputStates;
	public static DJHeroInputState[] controllers
	{
		get
		{
			if (s_DJHeroInputStates == null)
			{
				s_DJHeroInputStates = new DJHeroInputState[4];
				for (int i=0; i<s_DJHeroInputStates.Length; i++)
					s_DJHeroInputStates[i] = new DJHeroInputState();

			}
			return s_DJHeroInputStates;
		}
	}

    void Start()
    {
        playerIndexSet = new bool[4];
        playerIndex = new PlayerIndex[4];
        state = new GamePadState[4];
        prevState = new GamePadState[4];
    }

    void Update()
    {
		for (int i = 0; i < 4; ++i)
        {
            if (!playerIndexSet[i] || !prevState[i].IsConnected)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex[i] = testPlayerIndex;
                    playerIndexSet[i] = true;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            state[i] = GamePad.GetState(playerIndex[i], GamePadDeadZone.None);

            if (playerIndexSet[i] && prevState[i].IsConnected)
            {
                UpdateState(state[i], prevState[i], i);
            }

            prevState[i] = state[i];
        }
    }

	void UpdateState(GamePadState gamePadState, GamePadState previousState, int index)
	{
		DJHeroInputState inputState = controllers[index];

		inputState.WheelDelta = ApplyDeadZoneAndSensitivity(gamePadState.ThumbSticks.Left.Y, inputState.WheelDeadZone, inputState.WheelSensitivity);
		inputState.Crossfader = ApplyDeadZoneAndSensitivity(gamePadState.ThumbSticks.Right.Y, inputState.CrossfaderDeadZone, inputState.CrossfaderSensitivity);
		inputState.Knob = ApplyDeadZoneAndSensitivity(gamePadState.ThumbSticks.Right.X, 0f, inputState.KnobSensitivity);

		inputState.Red = gamePadState.Buttons.B == ButtonState.Pressed;
		inputState.Green = gamePadState.Buttons.A == ButtonState.Pressed;
		inputState.Blue = gamePadState.Buttons.X == ButtonState.Pressed;
		inputState.YeaBoiee = gamePadState.Buttons.Y == ButtonState.Pressed;
		inputState.Start = gamePadState.Buttons.Start == ButtonState.Pressed;
		inputState.Back = gamePadState.Buttons.Back == ButtonState.Pressed;
		inputState.DPadUp = gamePadState.DPad.Up == ButtonState.Pressed;
		inputState.DPadRight = gamePadState.DPad.Right == ButtonState.Pressed;
		inputState.DPadDown = gamePadState.DPad.Down == ButtonState.Pressed;
		inputState.DPadLeft = gamePadState.DPad.Left == ButtonState.Pressed;
		
		inputState.RedDown = gamePadState.Buttons.B == ButtonState.Pressed && previousState.Buttons.B == ButtonState.Released;
		inputState.GreenDown = gamePadState.Buttons.A == ButtonState.Pressed && previousState.Buttons.A == ButtonState.Released;
		inputState.BlueDown = gamePadState.Buttons.X == ButtonState.Pressed && previousState.Buttons.X == ButtonState.Released;
		inputState.YeaBoieeDown = gamePadState.Buttons.Y == ButtonState.Pressed && previousState.Buttons.Y == ButtonState.Released;
		inputState.StartDown = gamePadState.Buttons.Start == ButtonState.Pressed && previousState.Buttons.Start == ButtonState.Released;
		inputState.BackDown = gamePadState.Buttons.Back == ButtonState.Pressed && previousState.Buttons.Back == ButtonState.Released;
		inputState.DPadUpDown = gamePadState.DPad.Up == ButtonState.Pressed && previousState.DPad.Up == ButtonState.Released;
		inputState.DPadRightDown = gamePadState.DPad.Right == ButtonState.Pressed && previousState.DPad.Right == ButtonState.Released;
		inputState.DPadDownDown = gamePadState.DPad.Down == ButtonState.Pressed && previousState.DPad.Down == ButtonState.Released;
		inputState.DPadLeftDown = gamePadState.DPad.Left == ButtonState.Pressed && previousState.DPad.Left == ButtonState.Released;

		inputState.RedUp = gamePadState.Buttons.B == ButtonState.Released && previousState.Buttons.B == ButtonState.Pressed;
		inputState.GreenUp = gamePadState.Buttons.A == ButtonState.Released && previousState.Buttons.A == ButtonState.Pressed;
		inputState.BlueUp = gamePadState.Buttons.X == ButtonState.Released && previousState.Buttons.X == ButtonState.Pressed;
		inputState.YeaBoieeUp = gamePadState.Buttons.Y == ButtonState.Released && previousState.Buttons.Y == ButtonState.Pressed;
		inputState.StartUp = gamePadState.Buttons.Start == ButtonState.Released && previousState.Buttons.Start == ButtonState.Pressed;
		inputState.BackUp = gamePadState.Buttons.Back == ButtonState.Released && previousState.Buttons.Back == ButtonState.Pressed;
		inputState.DPadUpDown = gamePadState.DPad.Up == ButtonState.Released && previousState.DPad.Up == ButtonState.Pressed;
		inputState.DPadRightDown = gamePadState.DPad.Right == ButtonState.Released && previousState.DPad.Right == ButtonState.Pressed;
		inputState.DPadDownDown = gamePadState.DPad.Down == ButtonState.Released && previousState.DPad.Down == ButtonState.Pressed;
		inputState.DPadLeftDown = gamePadState.DPad.Left == ButtonState.Released && previousState.DPad.Left == ButtonState.Pressed;

	}

	private float ApplyDeadZoneAndSensitivity (float value, float deadZone, float sensitivity)
	{
		float result = value * sensitivity;
		if (Mathf.Abs (result) < deadZone && deadZone > 0f)
			result = 0f;

		return result;
	}

	void LateUpdate ()
	{
		for (int i = 0; i < controllers.Length; i++)
		{
			controllers[i].RedDown = false;
			controllers[i].GreenDown = false;
			controllers[i].BlueDown = false;
			controllers[i].YeaBoieeDown = false;
			controllers[i].StartDown = false;
			controllers[i].BackDown = false;
			controllers[i].DPadUpDown = false;
			controllers[i].DPadRightDown = false;
			controllers[i].DPadDownDown = false;
			controllers[i].DPadLeftDown = false;

			controllers[i].RedUp = false;
			controllers[i].GreenUp = false;
			controllers[i].BlueUp = false;
			controllers[i].YeaBoieeUp = false;
			controllers[i].StartUp = false;
			controllers[i].BackUp = false;
			controllers[i].DPadUpUp = false;
			controllers[i].DPadRightUp = false;
			controllers[i].DPadDownUp = false;
			controllers[i].DPadLeftUp = false;
		}
	}
}

public class DJHeroInputState
{
	public bool Red;
	public bool Green;
	public bool Blue;
	public bool YeaBoiee;
	public bool Start;
	public bool Back;
	public bool DPadUp;
	public bool DPadRight;
	public bool DPadDown;
	public bool DPadLeft;

	public bool RedDown;
	public bool GreenDown;
	public bool BlueDown;
	public bool YeaBoieeDown;
	public bool StartDown;
	public bool BackDown;
	public bool DPadUpDown;
	public bool DPadRightDown;
	public bool DPadDownDown;
	public bool DPadLeftDown;

	public bool RedUp;
	public bool GreenUp;
	public bool BlueUp;
	public bool YeaBoieeUp;
	public bool StartUp;
	public bool BackUp;
	public bool DPadUpUp;
	public bool DPadRightUp;
	public bool DPadDownUp;
	public bool DPadLeftUp;
	
	public float WheelDelta;
	public float Crossfader;
	public float Knob;

	public float WheelDeadZone = 0f;
	public float WheelSensitivity = 10000f;
	public float CrossfaderDeadZone = 0.01f;
	public float CrossfaderSensitivity = 1f;
	public float KnobSensitivity = 360f;
}
