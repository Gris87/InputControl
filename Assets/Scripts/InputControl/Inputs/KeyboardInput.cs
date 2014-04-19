using UnityEngine;
using System;

/// <summary>
/// <see cref="KeyboardInput"/> handles keyboard input device.
/// </summary>
public class KeyboardInput : CustomInput
{
    private KeyCode mKey;

    #region Properties
    /// <summary>
    /// Gets the keyboard key.
    /// </summary>
    /// <value>Keyboard key.</value>
    public KeyCode key
    {
        get
        {
            return mKey;
        }
    }
    #endregion

    /// <summary>
    /// Create a new instance of <see cref="KeyboardInput"/> that handles specified keyboard key.
    /// </summary>
    /// <param name="key">Keyboard key.</param>
    public KeyboardInput(KeyCode key=KeyCode.None)
    {
        mKey=key;
    }

    /// <summary>
    /// Parse string argument and try to create <see cref="KeyboardInput"/> instance.
    /// </summary>
    /// <returns>Parsed KeyboardInput.</returns>
    /// <param name="value">String representation of KeyboardInput.</param>
    public static KeyboardInput FromString(string value)
    {
        try
        {
            KeyCode key=(KeyCode)Enum.Parse(typeof(KeyCode), value);
            return new KeyboardInput(key);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="KeyboardInput"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="KeyboardInput"/>.</returns>
    public override string ToString()
    {
        return mKey.ToString();
    }

    /// <summary>
    /// Returns input value while the user holds down the key.
    /// </summary>
    /// <returns>Input value if button or axis is still active.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public override float getInput(string axis="", InputDevice device=InputDevice.Any)
    {
        if (device!=InputDevice.Any && device!=InputDevice.KeyboardAndMouse)
        {
            return 0;
        }

        float sensitivity=1;

        if (
            axis!=null
            &&
            (
             axis.Equals("Mouse X")
             ||
             axis.Equals("Mouse Y")
            )
           )
        {
            sensitivity=0.1f;
        }

        return Input.GetKey(mKey)     ? sensitivity : 0;
    }

    /// <summary>
    /// Returns input value during the frame the user starts pressing down the key.
    /// </summary>
    /// <returns>Input value if button or axis become active during this frame.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public override float getInputDown(string axis="", InputDevice device=InputDevice.Any)
    {
        if (device!=InputDevice.Any && device!=InputDevice.KeyboardAndMouse)
        {
            return 0;
        }

        float sensitivity=1;

        if (
            axis!=null
            &&
            (
             axis.Equals("Mouse X")
             ||
             axis.Equals("Mouse Y")
            )
           )
        {
            sensitivity=0.1f;
        }

        return Input.GetKeyDown(mKey) ? sensitivity : 0;
    }

    /// <summary>
    /// Returns input value during the frame the user releases the key.
    /// </summary>
    /// <returns>Input value if button or axis stopped being active during this frame.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public override float getInputUp(string axis="", InputDevice device=InputDevice.Any)
    {
        if (device!=InputDevice.Any && device!=InputDevice.KeyboardAndMouse)
        {
            return 0;
        }

        float sensitivity=1;

        if (
            axis!=null
            &&
            (
             axis.Equals("Mouse X")
             ||
             axis.Equals("Mouse Y")
            )
           )
        {
            sensitivity=0.1f;
        }

        return Input.GetKeyUp(mKey)   ? sensitivity : 0;
    }
}
