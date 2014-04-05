public class KeyMapping
{
    private string      mName;
    private CustomInput mPrimaryInput;
    private CustomInput mSecondaryInput;
    private CustomInput mThirdInput;

    #region Properties
    public string name
    {
        get
        {
            return mName;
        }
    }

    public CustomInput primaryInput
    {
        get
        {
            return mPrimaryInput;
        }

        set
        {
            if (value==null)
            {
                mPrimaryInput=new KeyboardInput();
            }
            else
            {
                mPrimaryInput=value;
            }
        }
    }

    public CustomInput secondaryInput
    {
        get
        {
            return mSecondaryInput;
        }

        set
        {
            if (value==null)
            {
                mSecondaryInput=new KeyboardInput();
            }
            else
            {
                mSecondaryInput=value;
            }
        }
    }

    public CustomInput thirdInput
    {
        get
        {
            return mThirdInput;
        }

        set
        {
            if (value==null)
            {
                mThirdInput=new KeyboardInput();
            }
            else
            {
                mThirdInput=value;
            }
        }
    }
    #endregion

    public KeyMapping(string aName="", CustomInput aPrimaryInput=null, CustomInput aSecondaryInput=null, CustomInput aThirdInput=null)
    {
        mName=aName;
        primaryInput   = aPrimaryInput;
        secondaryInput = aSecondaryInput;
        thirdInput     = aThirdInput;
    }

    public KeyMapping(KeyMapping another)
    {
        mName=another.mName;
        set(another);
    }

    public void set(KeyMapping another)
    {
        mPrimaryInput   = another.mPrimaryInput;
        mSecondaryInput = another.mSecondaryInput;
        mThirdInput     = another.mThirdInput;
    }

    public float getValue()
    {
        float res=0;
        float cur;

        cur=mPrimaryInput.getInput();

        if (cur>res)
        {
            res=cur;
        }

        cur=mSecondaryInput.getInput();

        if (cur>res)
        {
            res=cur;
        }

        cur=mThirdInput.getInput();

        if (cur>res)
        {
            res=cur;
        }

        return res;
    }

    public float getValueDown()
    {
        float res=0;
        float cur;

        cur=mPrimaryInput.getInputDown();

        if (cur>res)
        {
            res=cur;
        }

        cur=mSecondaryInput.getInputDown();

        if (cur>res)
        {
            res=cur;
        }

        cur=mThirdInput.getInputDown();

        if (cur>res)
        {
            res=cur;
        }

        return res;
    }

    public float getValueUp()
    {
        float res=0;
        float cur;

        cur=mPrimaryInput.getInputUp();

        if (cur>res)
        {
            res=cur;
        }

        cur=mSecondaryInput.getInputUp();

        if (cur>res)
        {
            res=cur;
        }

        cur=mThirdInput.getInputUp();

        if (cur>res)
        {
            res=cur;
        }

        return res;
    }

    public bool isPressed()
    {
        return getValue()     != 0;
    }

    public bool isPressedDown()
    {
        return getValueDown() != 0;
    }

    public bool isPressedUp()
    {
        return getValueUp()   != 0;
    }
}
