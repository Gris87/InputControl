using UnityEngine;



/// <summary>
/// <see cref="CustomInput"/> is an interface for handling some input device(keyboard, mouse, joystick).
/// </summary>
public abstract class CustomInput
{
	protected KeyModifier mModifiers;

	private   string      mCachedModifiersToString = null;



	protected static int         mCachedModifiersFrame = 0;
	protected static KeyModifier mCachedModifiersState = KeyModifier.NoModifier;




    /// <summary>
    /// Returns input value while the user holds down the key.
    /// </summary>
    /// <returns>Input value if button or axis is still active.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public abstract float getInput(string axis = "", InputDevice device = InputDevice.Any);

    /// <summary>
    /// Returns input value during the frame the user starts pressing down the key.
    /// </summary>
    /// <returns>Input value if button or axis become active during this frame.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public abstract float getInputDown(string axis = "", InputDevice device = InputDevice.Any);

    /// <summary>
    /// Returns input value during the frame the user releases the key.
    /// </summary>
    /// <returns>Input value if button or axis stopped being active during this frame.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public abstract float getInputUp(string axis = "", InputDevice device = InputDevice.Any);

    // TODO: Modifiers

	/// <summary>
	/// Verifies that specified key modifiers are active during current frame.
	/// </summary>
	/// <returns>Specified key modifiers are active during current frame.</returns>
	protected bool checkModifiers()
	{
		// TODO: Check it
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

		return mModifiers == mCachedModifiersState;
	}

	/// <summary>
	/// Parse string argument and try to get key modifiers.
	/// </summary>
	/// <param name="value">String representation of key modifiers with the rest of CustomInput string representation.</param>
	/// <param name="modifiers">Parsed key modifiers.</param>
	protected static void modifiersFromString(out string value, out KeyModifier modifiers)
	{
		modifiers = KeyModifier.NoModifier;

		do
		{
			if (value.StartsWith("Ctrl+"))
			{
				value      = value.Substring(5);
				modifiers |= KeyModifier.Ctrl;

				continue;
			}

			if (value.StartsWith("Alt+"))
			{
				value      = value.Substring(4);
				modifiers |= KeyModifier.Alt;
				
				continue;
			}

			if (value.StartsWith("Shift+"))
			{
				value      = value.Substring(6);
				modifiers |= KeyModifier.Shift;
				
				continue;
			}

			break;
		} while(true);
	}

	/// <summary>
	/// Returns a <see cref="System.String"/> that represents key modifiers.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents key modifiers.</returns>
	protected string modifiersToString()
	{
		if (mCachedModifiersToString == null)
		{
			string res = "";
			
			if (mModifiers & KeyModifier.Ctrl)
			{
				res += "Ctrl+";
			}

			if (mModifiers & KeyModifier.Alt)
			{
				res += "Alt+";
			}

			if (mModifiers & KeyModifier.Shift)
			{
				res += "Shift+";
			}
			
			mCachedModifiersToString = res;
		}
		
		return mCachedModifiersToString;
	}
}
