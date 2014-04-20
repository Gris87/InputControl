InputControl
============

InputControl works similarly to the Unity built in input manager.
InputControl allows user to customize control in Runtime.

First of all, you have to initialize InputControl.
So, you need to call commands below somewhere in your code. The best way to call it in the very first script:

Example:

void Start()
{
    InputControl.setKey("Up",        KeyCode.W,            KeyCode.UpArrow,    new JoystickInput(JoystickAxis.Axis2Negative));
    InputControl.setKey("Down",      KeyCode.S,            KeyCode.DownArrow,  new JoystickInput(JoystickAxis.Axis2Positive));
    InputControl.setKey("Left",      KeyCode.A,            KeyCode.LeftArrow,  new JoystickInput(JoystickAxis.Axis1Negative));
    InputControl.setKey("Right",     KeyCode.D,            KeyCode.RightArrow, new JoystickInput(JoystickAxis.Axis1Positive));
    InputControl.setKey("Run",       KeyCode.LeftShift,    KeyCode.RightShift, new JoystickInput(JoystickButton.Button1));
    InputControl.setKey("Jump",      KeyCode.Space,        KeyCode.None,       new JoystickInput(JoystickButton.Button2));
    InputControl.setKey("Fire1",     MouseButton.Left,     KeyCode.None,       new JoystickInput(JoystickButton.Button3));
    InputControl.setKey("Fire2",     MouseButton.Right,    KeyCode.None,       new JoystickInput(JoystickButton.Button4));
    InputControl.setKey("LookUp",    MouseAxis.MouseUp,    KeyCode.None,       new JoystickInput(JoystickAxis.Axis4Negative));
    InputControl.setKey("LookDown",  MouseAxis.MouseDown,  KeyCode.None,       new JoystickInput(JoystickAxis.Axis4Positive));
    InputControl.setKey("LookLeft",  MouseAxis.MouseLeft,  KeyCode.None,       new JoystickInput(JoystickAxis.Axis3Negative));
    InputControl.setKey("LookRight", MouseAxis.MouseRight, KeyCode.None,       new JoystickInput(JoystickAxis.Axis3Positive));

    InputControl.setAxis("Vertical",   "Down",     "Up");
    InputControl.setAxis("Horizontal", "Left",     "Right");
    InputControl.setAxis("Mouse X",    "LookLeft", "LookRight");
    InputControl.setAxis("Mouse Y",    "LookDown", "LookUp");
}

You can easy change this configuration in Runtime by calling InputControl.setKey() and InputControl.setAxis()

It is possible to get current active input with InputControl.currentInput():

CustomInput curInput=InputControl.currentInput();
Debug.Log(curInput==null ? "null" : curInput.ToString());

To invert mouse Y axis:
InputControl.invertMouseY=true;

You can setup mouse sensitivity by:
InputControl.mouseSensitivity=0.5f;
