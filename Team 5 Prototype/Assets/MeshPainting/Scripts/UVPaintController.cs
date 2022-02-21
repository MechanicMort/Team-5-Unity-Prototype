using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class UVPaintController : MonoBehaviour
{
    private RenderTexture renderTextureTemp;
    private Material matUVPaint;
    private MeshRenderer renderer;
    private MeshFilter filter;
    private MeshFilter proFiler;
    private Texture2D tex;

    public delegate void OnUVPaintControllerInit();
    public OnUVPaintControllerInit onUVPaintControllerInit;

    private WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    struct DelayedDrawParam
    {
        public RenderTexture rt;
        public Vector3 pos;
        public Vector3 dir;
        public Vector3 mask;
        public float strength;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();
        if (!renderer || !filter)
        {
            throw new MissingComponentException();  
        }
        renderTextureTemp = UVPaintManager.instance.rtTemp;
        matUVPaint = UVPaintManager.instance.matPaint;

        tex = new Texture2D(renderTextureTemp.width, renderTextureTemp.height, TextureFormat.RGBAFloat, false);
        Color[] colors = Enumerable.Repeat(Color.black, tex.width * tex.height).ToArray();
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.SetPixels(colors);
        tex.Apply();
        matUVPaint.SetTexture("_TexPaint", tex);

        renderer.material.SetTexture("_TexMask", tex); //Modify material to generate a new copy.

        onUVPaintControllerInit?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PaintOnGO(Vector3 posPaint, Vector3 dirPaint, Vector3 maskPaint, float strengthPaint)
    {
        if (renderer.enabled)
        {
            DelayedDrawParam param = new DelayedDrawParam();
            param.rt = renderTextureTemp;
            param.pos = posPaint;
            param.dir = dirPaint;
            param.mask = maskPaint;
            param.strength = strengthPaint;
            DelayedPaint(param);
        }
    }

    private void DelayedPaint(DelayedDrawParam param)
    {

        CommandBuffer cmd = CommandBufferPool.Get();
        matUVPaint.SetVector("posWPainter", param.pos);
        matUVPaint.SetVector("dirWPainter", param.dir);
        matUVPaint.SetVector("paintMask", param.mask);
        matUVPaint.SetFloat("paintStrength", param.strength);
        matUVPaint.SetTexture("_TexPaint", tex);
        Matrix4x4 M = transform.localToWorldMatrix;
        cmd.SetRenderTarget(param.rt);
        cmd.ClearRenderTarget(true, true, Color.black, 1.0f);
        cmd.DrawMesh(filter.mesh, M, matUVPaint);
        Graphics.ExecuteCommandBuffer(cmd);
        cmd.Clear();
        CommandBufferPool.Release(cmd);

        RenderTexture.active = param.rt;
        tex.ReadPixels(new Rect(0, 0, param.rt.width, param.rt.height), 0, 0);
        tex.Apply();
    }
}
