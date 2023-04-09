using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Post-processing/Multi Pass")]
public class MultiPassRendererFeature : ScriptableRendererFeature
{
    public List<string> lightModePasses;
    private MultiPassPass _mainPass;

    
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_mainPass);
    }

    public override void Create()
    {
        _mainPass = new MultiPassPass(lightModePasses);
    }
}