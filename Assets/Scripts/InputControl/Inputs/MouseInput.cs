using UnityEngine;
using System;

public class MouseInput : CustomInput
{
    private MouseAxis   mAxis;
    private MouseButton mButton;
    
    #region Properties    
    public MouseAxis axis
    {
        get
        {
            return mAxis;
        }
    }
    
    public MouseButton button
    {
        get
        {
            return mButton;
        }
    }
    #endregion

    public MouseInput(MouseAxis aAxis)
    {
        if (aAxis==MouseAxis.None)
        {
            Debug.LogError("aAxis can't be MouseAxis.None");
        }

        mAxis   = aAxis;
        mButton = MouseButton.None;
    }
    
    public MouseInput(MouseButton aButton)
    {
        if (aButton==MouseButton.None)
        {
            Debug.LogError("aButton can't be MouseButton.None");
        }

        mAxis   = MouseAxis.None;
        mButton = aButton;
    }

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

    public override float getInput()
    {
        if (mButton!=MouseButton.None)
        {
            KeyCode mouseButton=(KeyCode) ((int)KeyCode.Mouse0 + (int)mButton);

            return Input.GetKey(mouseButton) ? 1 : 0;
        }

        return getInputByAxis();
    }

    public override float getInputDown()
    {
        if (mButton!=MouseButton.None)
        {
            KeyCode mouseButton=(KeyCode) ((int)KeyCode.Mouse0 + (int)mButton);
            
            return Input.GetKeyDown(mouseButton) ? 1 : 0;
        }
        
        return getInputByAxis();
    }
    
    public override float getInputUp()
    {
        if (mButton!=MouseButton.None)
        {
            KeyCode mouseButton=(KeyCode) ((int)KeyCode.Mouse0 + (int)mButton);
            
            return Input.GetKeyUp(mouseButton) ? 1 : 0;
        }
        
        return getInputByAxis();
    }

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