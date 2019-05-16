using UnityEngine;

public static class Vector3IntUtils {
    public static Vector3Int FromDirection(MovingDirection direction) {
        switch (direction.Value) {
            case MovingDirection.NONE:
                return Vector3Int.zero;
            case MovingDirection.UP:
                return Vector3Int.up;
            case MovingDirection.LEFT:
                return Vector3Int.left;
            case MovingDirection.DOWN:
                return Vector3Int.down;
            case MovingDirection.RIGHT:
                return Vector3Int.right;
            default:
                return Vector3Int.zero;
        }
    }

    public static Vector3Int FromDirection(IteratingDirection direction) {
        switch (direction.Value) {
            case IteratingDirection.LEFT_TO_RIGHT:
                return Vector3Int.right;
            case IteratingDirection.RIGHT_TO_LEFT:
                return Vector3Int.left;
            case IteratingDirection.TOP_TO_BOTTOM:
                return Vector3Int.down;
            case IteratingDirection.BOTTOM_TO_TOP:
                return Vector3Int.up;
            default:
                return Vector3Int.zero;
        }
    }
}
