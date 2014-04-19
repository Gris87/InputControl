using UnityEngine;
using System;

/// <summary>
/// <see cref="MouseInput"/> handles mouse input device.
/// </summary>
public class MouseInput : CustomInput
{
    private MouseAxis   mAxis;
    private MouseButton mButton;

    #region Properties
    /// <summary>
    /// Gets the mouse axis.
    /// </summary>
    /// <value>Mouse axis.</value>
    public MouseAxis axis
    {
        get
        {
            return mAxis;
        }
    }

    /// <summary>
    /// Gets the mouse button.
    /// </summary>
    /// <value>Mouse button.</value>
    public MouseButton button
    {
        get
        {
            return mButton;
        }
    }
    #endregion

    /// <summary>
    /// Create a new instance of <see cref="MouseInput"/> that handles specified mouse axis.
    /// </summary>
    /// <param name="axis">Mouse axis.</param>
    public MouseInput(MouseAxis axis)
    {
        if (axis==MouseAxis.None)
        {
            Debug.LogError("axis can't be MouseAxis.None");
        }

        mAxis   = axis;
        mButton = MouseButton.None;
    }

    /// <summary>
    /// Create a new instance of <see cref="MouseInput"/> that handles specified mouse button.
    /// </summary>
    /// <param name="button">Mouse button.</param>
    public MouseInput(MouseButton button)
    {
        if (button==MouseButton.None)
        {
            Debug.LogError("button can't be MouseButton.None");
        }

        mAxis   = MouseAxis.None;
        mButton = button;
    }

    /// <summary>
    /// Parse string argument and try to create <see cref="MouseInput"/> instance.
    /// </summary>
    /// <returns>Parsed MouseInput.</returns>
    /// <param name="value">String representation of MouseInput.</param>
    public static MouseInput FromString(string value)
    {
        if (!value.StartsWith("Mouse "))
        {
            return null;
        }

        value=value.Substring(6);

        if (value.Equals("X (-)"))
        {
            return new MouseInput(MouseAxis.MouseLeft);
        }

        if (value.Equals("X (+)"))
        {
            return new MouseInput(MouseAxis.MouseRight);
        }

        if (value.Equals("Y (-)"))
        {
            return new MouseInput(MouseAxis.MouseDown);
        }

        if (value.Equals("Y (+)"))
        {
            return new MouseInput(MouseAxis.MouseUp);
        }

        if (value.Equals("Wheel (-)"))
        {
            return new MouseInput(MouseAxis.WheelDown);
        }

        if (value.Equals("Wheel (+)"))
        {
            return new MouseInput(MouseAxis.WheelUp);
        }

        if (!value.StartsWith("Button "))
        {
            return null;
        }

        value=value.Substring(7);

        try
        {
            int button=Convert.ToInt32(value)-1;

            if (button<0 || button>=(int)MouseButton.None)
            {
                return null;
            }

            return new MouseInput((MouseButton)button);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents the current <see cref="MouseInput"/>.
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="MouseInput"/>.</returns>
    public override string ToString()
    {
        string res="Mouse ";

        if (mAxis!=MouseAxis.None)
        {
            switch (mAxis)
            {
                case MouseAxis.MouseLeft:
                    res=res+"X (-)";
                break;
                case MouseAxis.MouseRight:
                    res=res+"X (+)";
                break;
                case MouseAxis.MouseUp:
                    res=res+"Y (+)";
                break;
                case MouseAxis.MouseDown:
                    res=res+"Y (-)";
                break;
                case MouseAxis.WheelUp:
                    res=res+"Wheel (+)";
                break;
                case MouseAxis.WheelDown:
                    res=res+"Wheel (-)";
                break;
                default:
                    Debug.LogError("Unknown axis");
                break;
            }
        }

        if (mButton!=MouseButton.None)
        {
            res=res+"Button "+((int)mButton+1).ToString();
        }

        return res;
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

        if (mButton!=MouseButton.None)
        {
            KeyCode mouseButton=(KeyCode) ((int)KeyCode.Mouse0 + (int)mButton);

            return Input.GetKey(mouseButton) ? 1 : 0;
        }

        return getInputByAxis();
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

        if (mButton!=MouseButton.None)
        {
            KeyCode mouseButton=(KeyCode) ((int)KeyCode.Mouse0 + (int)mButton);

            return Input.GetKeyDown(mouseButton) ? 1 : 0;
        }

        return getInputByAxis();
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

        if (mButton!=MouseButton.None)
        {
            KeyCode mouseButton=(KeyCode) ((int)KeyCode.Mouse0 + (int)mButton);

            return Input.GetKeyUp(mouseButton) ? 1 : 0;
        }

        return getInputByAxis();
    }

    /// <summary>
    /// Calls Input.GetAxis for a specified mouse axis.
    /// </summary>
    /// <returns>Value of mouse axis.</returns>
    private float getInputByAxis()
    {
        switch (mAxis)
        {
            case MouseAxis.MouseLeft:  return InputGetAxis("Mouse X",           false);
            case MouseAxis.MouseRight: return InputGetAxis("Mouse X",           true);
            case MouseAxis.MouseUp:    return InputGetAxis("Mouse Y",           true);
            case MouseAxis.MouseDown:  return InputGetAxis("Mouse Y",           false);
            case MouseAxis.WheelUp:    return InputGetAxis("Mouse ScrollWheel", true);
            case MouseAxis.WheelDown:  return InputGetAxis("Mouse ScrollWheel", false);
            default:
                Debug.LogError("Unknown axis");
            break;
        }

        return 0;
    }

    /// <summary>
    /// Calls Input.GetAxis for a specified mouse axis and check direction.
    /// </summary>
    /// <returns>Value of mouse axis.</returns>
    private float InputGetAxis(string axisName, bool positive)
    {
        float value=Input.GetAxis(axisName);

        if (positive)
        {
            return value>0 ?  value : 0;
        }
        else
        {
            return value<0 ? -value : 0;
        }
    }
}
