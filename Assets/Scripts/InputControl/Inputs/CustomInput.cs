/// <summary>
/// <see cref="CustomInput"/> is the base class for child class that processing input device.
/// </summary>
public abstract class CustomInput
{
    /// <summary>
    /// Returns input value while the user holds down the key.
    /// </summary>
    /// <returns>Input value if button or axis is still active.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public abstract float getInput(string axis="", InputDevice device=InputDevice.Any);

    /// <summary>
    /// Returns input value during the frame the user starts pressing down the key.
    /// </summary>
    /// <returns>Input value if button or axis become active during this frame.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public abstract float getInputDown(string axis="", InputDevice device=InputDevice.Any);

    /// <summary>
    /// Returns input value during the frame the user releases the key.
    /// </summary>
    /// <returns>Input value if button or axis stopped being active during this frame.</returns>
    /// <param name="axis">Specific actions for axis (Empty by default).</param>
    /// <param name="device">Preferred input device.</param>
    public abstract float getInputUp(string axis="", InputDevice device=InputDevice.Any);
}
