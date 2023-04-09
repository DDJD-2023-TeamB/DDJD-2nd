using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class MultiPassPass : ScriptableRenderPass
{
    private List<ShaderTagId> _tags;

    public MultiPassPass(List<string> tags)
    {
        _tags = new List<ShaderTagId>();
        foreach (string tag in tags)
        {
            _tags.Add(new ShaderTagId(tag));
        }
        this.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData){
        FilteringSettings filteringSettings = FilteringSettings.defaultValue;

        foreach(ShaderTagId pass in _tags){
            DrawingSettings drawingSettings = CreateDrawingSettings(pass, ref renderingData, SortingCriteria.CommonOpaque);
            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
        }

        context.Submit();
    }
}
