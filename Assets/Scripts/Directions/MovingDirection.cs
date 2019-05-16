using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct MovingDirection : IEquatable<MovingDirection> {
    public Directions Value;

    public enum Directions : byte {
        None,
        Up,
        Left,
        Down,
        Right
    }

    #region Constants

    public const Directions NONE = Directions.None;
    public const Directions UP = Directions.Up;
    public const Directions LEFT = Directions.Left;
    public const Directions DOWN = Directions.Down;
    public const Directions RIGHT = Directions.Right;

    #endregion

    #region Static Fields

    public static MovingDirection None = new MovingDirection { Value = NONE };
    public static MovingDirection Up = new MovingDirection { Value = UP };
    public static MovingDirection Left = new MovingDirection { Value = LEFT };
    public static MovingDirection Down = new MovingDirection { Value = DOWN };
    public static MovingDirection Right = new MovingDirection { Value = RIGHT };

    #endregion

    /// <summary>
    /// Returns a predefined <see cref="MovingDirection"/> for a specified <see cref="KeyCode"/>.
    /// </summary>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    public static MovingDirection FromKey(KeyCode keyCode) {
        switch (keyCode) {
            case KeyCode.UpArrow:
                return Up;
            case KeyCode.LeftArrow:
                return Left;
            case KeyCode.DownArrow:
                return Down;
            case KeyCode.RightArrow:
                return Right;
            default:
                return None;
        }
    }

    /// <summary>
    /// If <paramref name="pred"/> is not <see langword="null"/>,
    /// filters out <see cref="KeyCode"/>s in <paramref name="keyCodes"/> using a specified <see cref="Predicate{KeyCode}"/>.
    /// Returns an <see cref="IEnumerable{MovingDirection}"/> for the specified <see cref="KeyCode"/>s.
    /// </summary>
    /// <param name="keyCodes"></param>
    /// <returns></returns>
    public static IEnumerable<MovingDirection> FromKeys(IEnumerable<KeyCode> keyCodes, Predicate<KeyCode> pred = null) {
        return (pred is null ? keyCodes : keyCodes?.Where(keyCode => pred.Invoke(keyCode)))?.Select(keyCode => FromKey(keyCode));
    }

    #region Implementation of IEquatable<MovingDirection>

    public bool Equals(MovingDirection other) {
        return Value.Equals(other.Value);
    }

    #endregion

    #region Implementation of op_Equality and op_Inequality

    public static bool operator ==(MovingDirection movingDirection1, MovingDirection movingDirection2) {
        return movingDirection1.Equals(movingDirection2);
    }

    public static bool operator !=(MovingDirection movingDirection1, MovingDirection movingDirection2) {
        return !movingDirection1.Equals(movingDirection2);
    }

    #endregion

    #region Overridden Implementation of Object methods

    public override bool Equals(object obj) {
        return obj is MovingDirection ? Equals((MovingDirection)obj) : base.Equals(obj);
    }

    public override int GetHashCode() {
        return Value.GetHashCode();
    }

    public override string ToString() {
        switch (Value) {
            case Directions.None:
                return nameof(NONE);
            case Directions.Up:
                return nameof(UP);
            case Directions.Left:
                return nameof(LEFT);
            case Directions.Down:
                return nameof(DOWN);
            case Directions.Right:
                return nameof(RIGHT);
            default:
                return base.ToString();
        }
    }

    #endregion
}