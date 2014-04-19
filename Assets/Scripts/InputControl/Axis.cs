using UnityEngine;

/// <summary>
/// <see cref="Axis"/> is a named handler for negative <see cref="KeyMapping"/> and positive <see cref="KeyMapping"/>.
/// </summary>
public class Axis
{
    private string     mName;
    private KeyMapping mNegative;
    private KeyMapping mPositive;
    private bool       mInverted;

    #region Properties
    /// <summary>
    /// Gets the axis name.
    /// </summary>
    /// <value>Axis name.</value>
    public string name
    {
        get
        {
            return mName;
        }
    }

    /// <summary>
    /// Gets or sets the negative KeyMapping. Please note that null value is prohibited.
    /// </summary>
    /// <value>Negative KeyMapping.</value>
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

    /// <summary>
    /// Gets or sets the positive KeyMapping. Please note that null value is prohibited.
    /// </summary>
    /// <value>Positive KeyMapping.</value>
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

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Axis"/> is inverted.
    /// </summary>
    /// <value><c>true</c> if inverted; otherwise, <c>false</c>.</value>
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

    /// <summary>
    /// Create a new instance of <see cref="Axis"/> with specified negative <see cref="KeyMapping"/> and positive <see cref="KeyMapping"/>.
    /// </summary>
    /// <param name="name">Axis name.</param>
    /// <param name="negative">Negative KeyMapping.</param>
    /// <param name="positive">Positive KeyMapping.</param>
    public Axis(string name, KeyMapping negative, KeyMapping positive)
    {
        mName     = name;
        mInverted = false;
        set(negative, positive);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Axis"/> class based on another instance.
    /// </summary>
    /// <param name="another">Another Axis instance.</param>
    public Axis(Axis another)
    {
        mName=another.mName;

        set(another);
    }

    /// <summary>
    /// Set the same negative <see cref="KeyMapping"/> and positive <see cref="KeyMapping"/> as in another instance.
    /// </summary>
    /// <param name="another">Another Axis instance.</param>
    public void set(Axis another)
    {
        mInverted = another.mInverted;
        mNegative = another.mNegative;
        mPositive = another.mPositive;
    }

    /// <summary>
    /// Set negative <see cref="KeyMapping"/> and positive <see cref="KeyMapping"/>.
    /// </summary>
    /// <param name="negative">Negative KeyMapping.</param>
    /// <param name="positive">Positive KeyMapping.</param>
    public void set(KeyMapping negative, KeyMapping positive)
    {
        mNegative = negative;
        mPositive = positive;
    }


    /// <summary>
    /// Returns axis value by using negative <see cref="KeyMapping"/> and positive <see cref="KeyMapping"/>.
    /// </summary>
    /// <returns>Axis value.</returns>
    /// <param name="device">Preferred input device.</param>
    public float getValue(InputDevice device=InputDevice.Any)
    {
        if (mInverted)
        {
            return mNegative.getValue(mName, device)-mPositive.getValue(mName, device);
        }
        else
        {
            return mPositive.getValue(mName, device)-mNegative.getValue(mName, device);
        }
    }
}
