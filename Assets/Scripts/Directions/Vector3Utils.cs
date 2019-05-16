using UnityEngine;

public static class Vector3Utils {
    public static Vector3 FromDirection(MovingDirection direction) {
        switch (direction.Value) {
            case MovingDirection.NONE:
                return Vector3.zero;
            case MovingDirection.UP:
                return Vector3.up;
            case MovingDirection.LEFT:
                return Vector3.left;
            case MovingDirection.DOWN:
                return Vector3.down;
            case MovingDirection.RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 FromDirection(IteratingDirection direction) {
        switch (direction.Value) {
            case IteratingDirection.LEFT_TO_RIGHT:
                return Vector3.right;
            case IteratingDirection.RIGHT_TO_LEFT:
                return Vector3.left;
            case IteratingDirection.TOP_TO_BOTTOM:
                return Vector3.down;
            case IteratingDirection.BOTTOM_TO_TOP:
                return Vector3.up;
            default:
                return Vector3.zero;
        }
    }
}
