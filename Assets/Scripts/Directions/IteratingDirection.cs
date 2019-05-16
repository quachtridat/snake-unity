public struct IteratingDirection : System.IEquatable<IteratingDirection> {
    public Directions Value;

    public enum Directions {
        None,
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }

    #region Constants

    public const Directions NONE = Directions.None;
    public const Directions LEFT_TO_RIGHT = Directions.LeftToRight;
    public const Directions RIGHT_TO_LEFT = Directions.RightToLeft;
    public const Directions TOP_TO_BOTTOM = Directions.TopToBottom;
    public const Directions BOTTOM_TO_TOP = Directions.BottomToTop;

    #endregion

    #region Static Fields

    public static IteratingDirection None = new IteratingDirection { Value = NONE };
    public static IteratingDirection LeftToRight = new IteratingDirection { Value = LEFT_TO_RIGHT };
    public static IteratingDirection RightToLeft = new IteratingDirection { Value = RIGHT_TO_LEFT };
    public static IteratingDirection TopToBottom = new IteratingDirection { Value = TOP_TO_BOTTOM };
    public static IteratingDirection BottomToTop = new IteratingDirection { Value = BOTTOM_TO_TOP };

    #endregion

    #region Implementation of IEquatable<MovingDirection>

    public bool Equals(IteratingDirection other) {
        return Value.Equals(other.Value);
    }

    #endregion

    #region Implementation of op_Equality and op_Inequality

    public static bool operator ==(IteratingDirection movingDirection1, IteratingDirection movingDirection2) {
        return (object)movingDirection1 == null || (object)movingDirection2 == null ? System.Object.Equals(movingDirection1, movingDirection2) : movingDirection1.Equals(movingDirection2);
    }

    public static bool operator !=(IteratingDirection movingDirection1, IteratingDirection movingDirection2) {
        return (object)movingDirection1 == null || (object)movingDirection2 == null ? !System.Object.Equals(movingDirection1, movingDirection2) : !movingDirection1.Equals(movingDirection2);
    }

    #endregion

    #region Overridden Implementation of Object methods

    public override bool Equals(object obj) {
        return obj is IteratingDirection ? Equals((IteratingDirection)obj) : base.Equals(obj);
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
    }

    public override string ToString() {
        switch (Value) {
            case Directions.None:
                return nameof(NONE);
            case Directions.LeftToRight:
                return nameof(LEFT_TO_RIGHT);
            case Directions.RightToLeft:
                return nameof(RIGHT_TO_LEFT);
            case Directions.TopToBottom:
                return nameof(TOP_TO_BOTTOM);
            case Directions.BottomToTop:
                return nameof(BOTTOM_TO_TOP);
            default:
                return base.ToString();
        }
    }

    #endregion
}
