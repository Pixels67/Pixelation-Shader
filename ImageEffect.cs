using UnityEngine;

namespace Pixelation {
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class ImageEffect : MonoBehaviour {
    public  Shader   shader;
    private Material _material;


    protected virtual void Start() {
        if (!shader || !shader.isSupported)
            enabled = false;
    }


    protected Material Material {
        get {
            if (_material != null) return _material;

            _material = new Material(shader) {
                hideFlags = HideFlags.HideAndDontSave
            };

            return _material;
        }
    }


    protected virtual void OnDisable() {
        if (_material) {
            DestroyImmediate(_material);
        }
    }
}
}
