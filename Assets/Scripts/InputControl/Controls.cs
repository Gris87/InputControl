using UnityEngine;
using System.Collections.ObjectModel;



/// <summary>
/// <see cref="Controls"/> is a set of user defined buttons and axes. It is better to store this file somewhere in your project.
/// </summary>
public static class Controls
{
    /// <summary>
    /// <see cref="Buttons"/> is a set of user defined buttons.
    /// </summary>
    public struct Buttons
    {
        public KeyMapping up;
        public KeyMapping down;
        public KeyMapping left;
        public KeyMapping right;
        public KeyMapping jump;
    }

    /// <summary>
    /// <see cref="Axes"/> is a set of user defined axes.
    /// </summary>
    public struct Axes
    {
        public Axis vertical;
        public Axis horizontal;
    }

    public static Buttons buttons;
    public static Axes    axes;



    static Controls()
    {
        buttons.up      = InputControl.setKey("Up",    KeyCode.W,     KeyCode.UpArrow,    new JoystickInput(JoystickAxis.Axis2Negative));
        buttons.down    = InputControl.setKey("Down",  KeyCode.S,     KeyCode.DownArrow,  new JoystickInput(JoystickAxis.Axis2Positive));
        buttons.left    = InputControl.setKey("Left",  KeyCode.A,     KeyCode.LeftArrow,  new JoystickInput(JoystickAxis.Axis1Negative));
        buttons.right   = InputControl.setKey("Right", KeyCode.D,     KeyCode.RightArrow, new JoystickInput(JoystickAxis.Axis1Positive));
        buttons.jump    = InputControl.setKey("Jump",  KeyCode.Space, KeyCode.None,       new JoystickInput(JoystickButton.Button1));

        axes.vertical   = InputControl.setAxis("Vertical",   buttons.down, buttons.up);
        axes.horizontal = InputControl.setAxis("Horizontal", buttons.left, buttons.right);

        load();
    }

    public static void init()
    {
        // Nothing. It just call static constructor if needed
    }

    public static void save()
    {
        // It is just an example. You may remove it or modify it if you want
        ReadOnlyCollection<KeyMapping> keys = InputControl.getKeysList();

        foreach(KeyMapping key in keys)
        {
            PlayerPrefs.SetString("Controls." + key.name + ".primary",   key.primaryInput.ToString());
            PlayerPrefs.SetString("Controls." + key.name + ".secondary", key.secondaryInput.ToString());
            PlayerPrefs.SetString("Controls." + key.name + ".third",     key.thirdInput.ToString());
        }

        PlayerPrefs.Save();
    }

    public static void load()
    {
        // It is just an example. You may remove it or modify it if you want
        ReadOnlyCollection<KeyMapping> keys = InputControl.getKeysList();

        foreach(KeyMapping key in keys)
        {
            string inputStr;

            inputStr = PlayerPrefs.GetString("Controls." + key.name + ".primary");

            if (inputStr != "")
            {
                key.primaryInput = customInputFromString(inputStr);
            }

            inputStr = PlayerPrefs.GetString("Controls." + key.name + ".secondary");

            if (inputStr != "")
            {
                key.secondaryInput = customInputFromString(inputStr);
            }

            inputStr = PlayerPrefs.GetString("Controls." + key.name + ".third");

            if (inputStr != "")
            {
                key.thirdInput = customInputFromString(inputStr);
            }
        }
    }

    private static CustomInput customInputFromString(string value)
    {
        CustomInput res;

        res = KeyboardInput.FromString(value);

        if (res != null)
        {
            return res;
        }

        res = MouseInput.FromString(value);

        if (res != null)
        {
            return res;
        }

        res = JoystickInput.FromString(value);

        if (res != null)
        {
            return res;
        }

        return null;
    }
}

