using UnityEngine;
using System;



/// <summary>
/// <see cref="KeyboardInput"/> handles keyboard input device.
/// </summary>
public class KeyboardInput : CustomInput
{
    private KeyCode mKey;

    private string  mCachedToString;



    #region Properties

    #region key
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

    #endregion



    /// <summary>
    /// Create a new instance of <see cref="KeyboardInput"/> that handles specified keyboard key.
    /// </summary>
    /// <param name="key">Keyboard key.</param>
    /// <param name="modifiers">Key modifiers.</param>
    public KeyboardInput(KeyCode key = KeyCode.None, KeyModifier modifiers = KeyModifier.NoModifier)
    {
        mKey       = key;
        mModifiers = modifiers;

        mCachedToString = null;
    }

    /// <summary>
    /// Parse string argument and try to create <see cref="KeyboardInput"/> instance.
    /// </summary>
    /// <returns>Parsed KeyboardInput.</returns>
    /// <param name="value">String representation of KeyboardInput.</param>
    public static KeyboardInput FromString(string value)
    {
        KeyModifier modifiers = modifiersFromString(ref value);

        try
        {
            KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), value);
            return new KeyboardInput(key, modifiers);
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
        if (mCachedToString == null)
        {
            mCachedToString = modifiersToString() + mKey.ToString();
        }

        return mCachedToString;
    }

    /// <summary>
    /// Returns input value while the user holds down the key.
    /// </summary>
    /// <returns>Input value if button or axis is still active.</returns>
    /// <param name="exactKeyModifiers">If set to <c>true</c> check that only specified key modifiers are active, otherwise check that at least specified key modifiers are active.</param>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public override float getInput(bool exactKeyModifiers = false, string axis = "", InputDevice device = InputDevice.Any)
    {
        if (
            device != InputDevice.Any
            &&
            device != InputDevice.KeyboardAndMouse
            ||
            !checkModifiersForKeys(exactKeyModifiers)
           )
        {
            return 0;
        }

        float sensitivity = 1;

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
            sensitivity = 0.1f;
        }

        return Input.GetKey(mKey) ? sensitivity : 0;
    }

    /// <summary>
    /// Returns input value during the frame the user starts pressing down the key.
    /// </summary>
    /// <returns>Input value if button or axis become active during this frame.</returns>
    /// <param name="exactKeyModifiers">If set to <c>true</c> check that only specified key modifiers are active, otherwise check that at least specified key modifiers are active.</param>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public override float getInputDown(bool exactKeyModifiers = false, string axis = "", InputDevice device = InputDevice.Any)
    {
        if (
            device != InputDevice.Any
            &&
            device != InputDevice.KeyboardAndMouse
            ||
            !checkModifiersForKeys(exactKeyModifiers)
           )
        {
            return 0;
        }

        float sensitivity = 1;

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
            sensitivity = 0.1f;
        }

        return Input.GetKeyDown(mKey) ? sensitivity : 0;
    }

    /// <summary>
    /// Returns input value during the frame the user releases the key.
    /// </summary>
    /// <returns>Input value if button or axis stopped being active during this frame.</returns>
    /// <param name="exactKeyModifiers">If set to <c>true</c> check that only specified key modifiers are active, otherwise check that at least specified key modifiers are active.</param>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public override float getInputUp(bool exactKeyModifiers = false, string axis = "", InputDevice device = InputDevice.Any)
    {
        if (
            device != InputDevice.Any
            &&
            device != InputDevice.KeyboardAndMouse
            ||
            !checkModifiersForKeys(exactKeyModifiers)
           )
        {
            return 0;
        }

        float sensitivity = 1;

        if (
            axis != null
            &&
            (
             axis.Equals("Mouse X")
             ||
             axis.Equals("Mouse Y")
            )
           )
        {
            sensitivity = 0.1f;
        }

        return Input.GetKeyUp(mKey) ? sensitivity : 0;
    }

    /// <summary>
    /// Verifies that specified key modifiers are active during current frame.
    /// </summary>
    /// <returns>Specified key modifiers are active during current frame.</returns>
    /// <param name="exactKeyModifiers">If set to <c>true</c> check that only specified key modifiers are active, otherwise check that at least specified key modifiers are active.</param>
    private bool checkModifiersForKeys(bool exactKeyModifiers = false)
    {
        if (!exactKeyModifiers && mModifiers == KeyModifier.NoModifier)
        {
            return true;
        }

        if (mCachedModifiersFrame != Time.frameCount)
        {
            KeyModifier res = KeyModifier.NoModifier;

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                res |= KeyModifier.Ctrl;
            }

            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                res |= KeyModifier.Alt;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                res |= KeyModifier.Shift;
            }

            mCachedModifiersFrame = Time.frameCount;
            mCachedModifiersState = res;
        }

        if (exactKeyModifiers)
        {
            if (
                mKey == KeyCode.LeftControl
                ||
                mKey == KeyCode.RightControl
               )
            {
                return (mModifiers | KeyModifier.Ctrl) == mCachedModifiersState;
            }

            if (
                mKey == KeyCode.LeftAlt
                ||
                mKey == KeyCode.RightAlt
               )
            {
                return (mModifiers | KeyModifier.Alt) == mCachedModifiersState;
            }

            if (
                mKey == KeyCode.LeftShift
                ||
                mKey == KeyCode.RightShift
               )
            {
                return (mModifiers | KeyModifier.Shift) == mCachedModifiersState;
            }
        }
        else
        {
            return (mModifiers & mCachedModifiersState) == mModifiers;
        }

        return mModifiers == mCachedModifiersState;
    }
}
