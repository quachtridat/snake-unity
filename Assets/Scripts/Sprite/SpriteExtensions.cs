using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteExtensions {
    // https://forum.unity.com/threads/213571/
    public static Rect UV(this Sprite sprite) {
        Rect rect = sprite.rect;
        Texture tex = sprite.texture;

        rect.x /= tex.width;
        rect.y /= tex.height;

        rect.width /= tex.width;
        rect.height /= tex.height;

        return rect;
    }
}