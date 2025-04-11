using UnityEngine;

namespace Pixelation {
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Pixelation")]
public class Pixelation : ImageEffect {
    private static readonly int BlockSize   = Shader.PropertyToID("block_size");
    private static readonly int Count       = Shader.PropertyToID("block_count");
    private static readonly int PaletteTex  = Shader.PropertyToID("palette_tex");
    private static readonly int PaletteSize = Shader.PropertyToID("palette_size");

    [Range(64.0f, 512.0f)] public float   blockCount = 128;
    public                        Color[] palette;

    private Camera _camera;

    private void Awake() {
        _camera = GetComponent<Camera>();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        var k = _camera.aspect;
        var count = new Vector2(blockCount, blockCount / k);
        var size = new Vector2(1.0f                    / count.x, 1.0f / count.y);

        Material.SetVector(Count, count);
        Material.SetVector(BlockSize, size);
        SetPaletteTexture(Material, palette);

        Graphics.Blit(source, destination, Material);
    }

    private static void SetPaletteTexture(Material mat, Color[] colors) {
        var size = colors.Length;
        var paletteTexture = new Texture2D(size, 1, TextureFormat.RGBA32, false);

        for (var i = 0; i < size; i++) {
            paletteTexture.SetPixel(i, 0, colors[i]);
        }

        paletteTexture.Apply();

        mat.SetTexture(PaletteTex, paletteTexture);
        mat.SetFloat(PaletteSize, size);
    }
}
}
