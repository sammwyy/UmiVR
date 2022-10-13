using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class DebugUI : MonoBehaviour
{

    // Debug Information
    private int _fps = 0;
    private long _lastUpdate = 0;
    private long _lastUpdateAgo = 0;
    private int _memoryUsed = 0;
    private int _heapSize = 0;
    private int _vramAlloc = 0;
    private int _modelMaterials = 0;
    private int _modelMeshes = 0;
    private int _modelShaders = 0;

    // Fields
    private TextMeshProUGUI _text;
    private string _template = "";

    public void UpdateHeavyDebugInformation()
    {
        // Model Rendering
        List<Material> materials = new List<Material>();
        List<Shader> shaders = new List<Shader>();

        this._modelMaterials = 0;
        this._modelMeshes = GameObject.FindObjectsOfType<MeshRenderer>().Length;
        this._modelShaders = 0;

        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            this._modelMaterials += renderer.materials.Length;

            foreach (Material material in renderer.materials)
            {
                if (!materials.Contains(material))
                {
                    materials.Add(material);
                    this._modelMaterials++;
                }

                Shader shader = material.shader;
                if (!shaders.Contains(shader))
                {
                    shaders.Add(shader);
                    this._modelShaders++;
                }
            }
        }
    }

    public void UpdateDebugInformation()
    {
        long ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        // Performance
        _lastUpdateAgo = ms - _lastUpdate;
        _lastUpdate = ms;
        _fps = (int)(1.0f / Time.deltaTime);

        // Resource usage
        _memoryUsed = (int)((Profiler.GetMonoUsedSizeLong() / 1024) / 1024);
        _heapSize = (int)((Profiler.GetMonoHeapSizeLong() / 1024) / 1024);
        _vramAlloc = (int)((Profiler.GetAllocatedMemoryForGraphicsDriver() / 1024) / 1024);

    }

    public string GetDebugInformation()
    {
        return _template
            .Replace("{fps}", this._fps.ToString())
            .Replace("{last_update}", this._lastUpdate.ToString())
            .Replace("{last_update_ago}", this._lastUpdateAgo.ToString())
            .Replace("{heap_size}", this._heapSize.ToString())
            .Replace("{memory_used}", this._memoryUsed.ToString())
            .Replace("{vram_alloc}", this._vramAlloc.ToString())
            .Replace("{model_materials}", this._modelMaterials.ToString())
            .Replace("{model_meshes}", this._modelMeshes.ToString())
            .Replace("{model_shaders}", this._modelShaders.ToString())
        ;
    }

    void RenderDebugInformation()
    {
        this._text.text = this.GetDebugInformation();
    }

    void Start()
    {
        this.UpdateHeavyDebugInformation();

        this._text = GetComponent<TextMeshProUGUI>();
        this._template = _text.text;

        InvokeRepeating("RenderDebugInformation", 0, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateDebugInformation();
    }
}
