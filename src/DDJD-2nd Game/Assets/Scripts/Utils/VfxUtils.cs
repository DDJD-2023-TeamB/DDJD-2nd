using UnityEngine;
using UnityEngine.VFX;

public static class VfxUtils
{
    private const string VFXPositionPostfix = "_position";
    private const string VFXRotationPostfix = "_angles";
    private const string VFXScalePostfix = "_scale";

    public static void SetVFXTransformProperty(this VisualEffect visualEffect, string propertyName, Transform transform)
    {
        var position = propertyName + VFXPositionPostfix;
        var angles = propertyName + VFXRotationPostfix;
        var scale = propertyName + VFXScalePostfix;

        visualEffect.SetVector3(position, transform.position);
        visualEffect.SetVector3(angles, transform.eulerAngles);
        visualEffect.SetVector3(scale, transform.localScale);
    }
}
