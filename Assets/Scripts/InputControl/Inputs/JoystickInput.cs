using UnityEngine;
using System;

public class JoystickInput : CustomInput
{
    private JoystickAxis   mAxis;
    private JoystickButton mButton;
    private Joystick       mTarget;

    #region Properties
    public JoystickAxis axis
    {
        get
        {
            return mAxis;
        }
    }

    public JoystickButton button
    {
        get
        {
            return mButton;
        }
    }

    public Joystick target
    {
        get
        {
            return mTarget;
        }
    }
    #endregion

    public JoystickInput(JoystickAxis aAxis, Joystick aTarget=Joystick.AllJoysticks)
    {
        if (aAxis==JoystickAxis.None)
        {
            Debug.LogError("aAxis can't be JoystickAxis.None");
        }

        mAxis   = aAxis;
        mButton = JoystickButton.None;
        mTarget = aTarget;
    }

    public JoystickInput(JoystickButton aButton, Joystick aTarget=Joystick.AllJoysticks)
    {
        if (aButton==JoystickButton.None)
        {
            Debug.LogError("aButton can't be JoystickButton.None");
        }

        mAxis   = JoystickAxis.None;
        mButton = aButton;
        mTarget = aTarget;
    }

    public static JoystickInput FromString(string value)
    {
        if (!value.StartsWith("Joystick "))
        {
            return null;
        }
        
        value=value.Substring(9);

        if (value.Length==0)
        {
            return null;
        }

        Joystick target;

        if (value[0]>='0' && value[0]<='9')
        {
            int index=value.IndexOf(" ");

            if (index<0)
            {
                return null;
            }

            try
            {
                int targetNumber=Convert.ToInt32(value.Substring(0, index));
                
                if (targetNumber<1 || targetNumber>Enum.GetValues(typeof(Joystick)).Length-1)
                {
                    return null;
                }
                
                target=(Joystick)targetNumber;
            }
            catch (Exception)
            {
                return null;
            }

            value=value.Substring(index+1);
        }
        else
        {
            target=Joystick.AllJoysticks;
        }

        if (value.StartsWith("Axis "))
        {
            value=value.Substring(5);

            bool positive;

            if (value.EndsWith(" (-)"))
            {
                positive=false;
            }
            else
            if (value.EndsWith(" (+)"))
            {
                positive=true;
            }
            else
            {
                return null;
            }

            value=value.Remove(value.Length-4);

            try
            {
                int axisNumber=(Convert.ToInt32(value)-1)*2;

                if (!positive)
                {
                    ++axisNumber;
                }
                
                if (axisNumber<0 || axisNumber>=(int)JoystickAxis.None)
                {
                    return null;
                }

                return new JoystickInput((JoystickAxis)axisNumber, target);
            }
            catch (Exception)
            {
                return null;
            }
        }

        if (!value.StartsWith("Button "))
        {
            return null;
        }
        
        value=value.Substring(7);
        
        try
        {
            int button=Convert.ToInt32(value)-1;
            
            if (button<0 || button>=(int)JoystickButton.None)
            {
                return null;
            }
            
            return new JoystickInput((JoystickButton)button, target);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override string ToString()
    {
        string res;

        if (mTarget==Joystick.AllJoysticks)
        {
            res="Joystick ";
        }
        else
        {
            res="Joystick "+((int)mTarget).ToString()+" ";
        }

        if (mAxis!=JoystickAxis.None)
        {
            int axisId=(int)mAxis;
            bool positive;

            if (axisId % 2 == 0)
            {
                axisId=(axisId/2) + 1;
                positive=true;
            }
            else
            {
                axisId=((axisId-1)/2) + 1;
                positive=false;
            }

            res=res+"Axis "+axisId.ToString()+" "+(positive ? "(+)" : "(-)");
        }

        if (mButton!=JoystickButton.None)
        {
            res=res+"Button "+((int)mButton+1).ToString();
        }

        return res;
    }

    public override float getInput()
    {
        if (mButton!=JoystickButton.None)
        {
            return Input.GetButton(getInputName())     ? 1 : 0;
        }

        return getInputByAxis();
    }

    public override float getInputDown()
    {
        if (mButton!=JoystickButton.None)
        {
            return Input.GetButtonDown(getInputName()) ? 1 : 0;
        }
        
        return getInputByAxis();
    }
    
    public override float getInputUp()
    {
        if (mButton!=JoystickButton.None)
        {
            return Input.GetButtonUp(getInputName())   ? 1 : 0;
        }
        
        return getInputByAxis();
    }

    private float getInputByAxis()
    {
        float joyAxis=Input.GetAxis(getInputName());
        
        if (
            ((int)mAxis) % 2 == 1
            &&
            joyAxis<-InputControl.joystickThreshold
           )
        {
            return -joyAxis;
        }

        if (
            ((int)mAxis) % 2 == 0
            &&
            joyAxis>InputControl.joystickThreshold
           )
        {
            return joyAxis;
        }

        return 0;
    }

    private string getInputName()
    {
        string res;
        
        if (mTarget==Joystick.AllJoysticks)
        {
            res="Joystick ";
        }
        else
        {
            res="Joystick "+((int)mTarget).ToString()+" ";
        }
        
        if (mAxis!=JoystickAxis.None)
        {
            int axisId=(int)mAxis;
            
            if (axisId % 2 == 0)
            {
                axisId=(axisId/2) + 1;
            }
            else
            {
                axisId=((axisId-1)/2) + 1;
            }
            
            res=res+"Axis "+axisId.ToString();
        }
        
        if (mButton!=JoystickButton.None)
        {
            res=res+"Button "+((int)mButton+1).ToString();
        }
        
        return res;
    }
}