using UnityEngine;

public class Axis
{
    private string     mName;
    private KeyMapping mNegative;
    private KeyMapping mPositive;
    private bool       mInverted;

    #region Properties
    public string name
    {
        get
        {
            return mName;
        }
    }

    public KeyMapping negative
    {
        get
        {
            return mNegative;
        }

        set
        {
            if (value==null)
            {
                Debug.LogError("value can't be null");
            }

            mNegative=value;
        }
    }

    public KeyMapping positive
    {
        get
        {
            return mPositive;
        }

        set
        {
            if (value==null)
            {
                Debug.LogError("value can't be null");
            }

            mPositive=value;
        }
    }

    public bool inverted
    {
        get
        {
            return mInverted;
        }

        set
        {
            mInverted=value;
        }
    }
    #endregion

    public Axis(string aName, KeyMapping aNegative, KeyMapping aPositive)
    {
        mName     = aName;
        mInverted = false;
        set(aNegative, aPositive);
    }

    public Axis(Axis another)
    {
        mName     = another.mName;
        mInverted = another.mInverted;
        set(another);
    }

    public void set(Axis another)
    {
        mNegative = another.mNegative;
        mPositive = another.mPositive;
    }

    public void set(KeyMapping aNegative, KeyMapping aPositive)
    {
        mNegative = aNegative;
        mPositive = aPositive;
    }

    public float getValue()
    {
        if (mInverted)
        {
            return mNegative.getValue()-mPositive.getValue();
        }
        else
        {
            return mPositive.getValue()-mNegative.getValue();
        }
    }
}
