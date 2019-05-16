using System;
using UnityEngine;

public class SingleColorTextureBuilder : ITextureBuilder {

    [Serializable]
    public struct SingleColorTextureInfo {
        private Color _color;

        public Color Color { get => _color; set => _color = value; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public SingleColorTextureInfo textureInfo;

    /// <summary>
    /// Builds a <see cref="Texture2D"/> from <see cref="SingleColorTextureInfo"/>.
    /// </summary>
    /// <param name="textureInfo"></param>
    /// <returns></returns>
    public Texture2D Build() {
        return Build(textureInfo);
    }

    /// <summary>
    /// Builds a <see cref="Texture2D"/> from <see cref="SingleColorTextureInfo"/>.
    /// </summary>
    /// <param name="textureInfo"></param>
    /// <returns></returns>
    public static Texture2D Build(SingleColorTextureInfo textureInfo) {
        var color = textureInfo.Color;
        var texture = new Texture2D(textureInfo.Width, textureInfo.Height);
        for (int y = 0, h = textureInfo.Height; y < h; ++y)
            for (int x = 0, w = textureInfo.Width; x < w; ++x)
                texture.SetPixel(x, y, color);
        texture.Apply();
        return texture;
    }
}