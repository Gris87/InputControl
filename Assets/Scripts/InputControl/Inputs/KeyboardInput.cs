using UnityEngine;
using System;

public class KeyboardInput : CustomInput
{
    private KeyCode mKey;
    public KeyCode key
    {
        get
        {
            return mKey;
        }
    }

    public KeyboardInput(KeyCode aKey=KeyCode.None)
    {
        mKey=aKey;
    }

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

    public override string ToString()
    {
        return mKey.ToString();
    }

    public override float getInput()
    {
        return Input.GetKey(mKey)     ? 1 : 0;
    }

    public override float getInputDown()
    {
        return Input.GetKeyDown(mKey) ? 1 : 0;
    }

    public override float getInputUp()
    {
        return Input.GetKeyUp(mKey)   ? 1 : 0;
    }
}