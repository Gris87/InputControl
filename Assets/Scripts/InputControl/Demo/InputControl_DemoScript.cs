using UnityEngine;
using System.Collections.Generic;

public class InputControl_DemoScript : MonoBehaviour
{
	private bool pause;
	private int  selectedKey;

	private CharacterController controller;

	// Use this for initialization
	void Start()
	{
		controller=GetComponent<CharacterController>();

		pause=false;
		selectedKey=-1;

		InputControl.setKey("Up",    KeyCode.W, KeyCode.UpArrow,    new JoystickInput(JoystickAxis.Axis2Negative));
		InputControl.setKey("Down",  KeyCode.S, KeyCode.DownArrow,  new JoystickInput(JoystickAxis.Axis2Positive));
		InputControl.setKey("Left",  KeyCode.A, KeyCode.LeftArrow,  new JoystickInput(JoystickAxis.Axis1Negative));
		InputControl.setKey("Right", KeyCode.D, KeyCode.RightArrow, new JoystickInput(JoystickAxis.Axis1Positive));
		InputControl.setKey("Jump",  KeyCode.Space,                 new JoystickInput(JoystickButton.Button1));

		InputControl.setAxis("Vertical",   "Down", "Up");
		InputControl.setAxis("Horizontal", "Left", "Right");
	}
	
	// Update is called once per frame
	void Update()
	{
		if (InputControl.GetKeyDown(KeyCode.Escape))
		{
			if (selectedKey>=0)
			{
				setInputForKey(new KeyboardInput());
			}
			else
			{
				pause=!pause;
			}
		}

		if (!pause)
		{
			Vector3 movement=new Vector3();

			movement.x=InputControl.GetAxis("Horizontal")*5;

			if (InputControl.GetButton("Jump"))
			{
				movement.y=5;
			}
			else
			{
				movement.y=-5;
			}

			movement.z=InputControl.GetAxis("Vertical")*5;

			controller.Move(movement*Time.deltaTime);
		}
		else
		{
			if (selectedKey>=0)
			{
				CustomInput input=InputControl.currentInput();

				if (input!=null)
				{
					setInputForKey(input);
				}
			}
		}
	}

	void OnGUI()
	{
		if (pause)
		{
			if (selectedKey>=0 && Event.current.type!=EventType.Repaint && Event.current.type!=EventType.Layout)
			{
				Event.current.Use();
			}

			GUI.Label(new Rect(20, 20, 200, 20), "Press Esc to play");

			List<KeyMapping> keys=InputControl.getKeys();

			for (int i=0; i<keys.Count; ++i)
			{
				GUI.Label(new Rect(Screen.width*0.195f-55, Screen.height*0.5f-200+i*25, 50, 20), keys[i].name);

				if (GUI.Button(new Rect(Screen.width*0.195f, Screen.height*0.5f-200+i*25, Screen.width*0.2f, 20), selectedKey==(i*3)   ? "..." : keys[i].primaryInput.ToString()))
				{
					selectedKey=(i*3);
				}

				if (GUI.Button(new Rect(Screen.width*0.4f,  Screen.height*0.5f-200+i*25, Screen.width*0.2f, 20), selectedKey==(i*3)+1 ? "..." : keys[i].secondaryInput.ToString()))
				{
					selectedKey=(i*3)+1;
				}

				if (GUI.Button(new Rect(Screen.width*0.605f, Screen.height*0.5f-200+i*25, Screen.width*0.2f, 20), selectedKey==(i*3)+2 ? "..." : keys[i].thirdInput.ToString()))
				{
					selectedKey=(i*3)+2;
				}
			}

			if (GUI.Button(new Rect(Screen.width*0.5f-100, Screen.height*0.5f+100, 200, 20), "Close"))
			{
				pause=false;
			}
		}
		else
		{
			GUI.Label(new Rect(20, 20, 200, 20), "Press Esc to setup controls");
		}
	}

	private void setInputForKey(CustomInput input)
	{
		int column = selectedKey          % 3;
		int row    = (selectedKey-column) / 3;

		switch (column)
		{
			case 0:
				InputControl.getKeys()[row].primaryInput   = input;
			break;
			case 1:
				InputControl.getKeys()[row].secondaryInput = input;
			break;
			case 2:
				InputControl.getKeys()[row].thirdInput     = input;
			break;
		}

		selectedKey=-1;
	}
}
