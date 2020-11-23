using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPostRender : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera minimapCamera;

    [Header("Settings")]
    [SerializeField] private float lineWidth;
    [SerializeField] private Color lineColor;
    //[SerializeField] private Collider mapCollider;
    //[SerializeField] private Collider2D mapCollider2D;
    [SerializeField] private Material cameraBoxMaterial;

    private void OnPostRender()
    {
        Vector3 minViewportPoint = minimapCamera.WorldToViewportPoint(GetCameraFrustumPoint(new Vector3(0f, 0f)));
        Vector3 maxViewportPoint = minimapCamera.WorldToViewportPoint(GetCameraFrustumPoint(new Vector3(Screen.width, Screen.height)));
        float minX = minViewportPoint.x;
        float minY = minViewportPoint.y;
        float maxX = maxViewportPoint.x;
        float maxY = maxViewportPoint.y;

        GL.PushMatrix();
        {
            cameraBoxMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.QUADS);
            GL.Color(lineColor);
            {
                GL.Vertex(new Vector3(minX, minY + lineWidth, 0));
                GL.Vertex(new Vector3(minX, minY - lineWidth, 0));
                GL.Vertex(new Vector3(maxX, minY - lineWidth, 0));
                GL.Vertex(new Vector3(maxX, minY + lineWidth, 0));

                GL.Vertex(new Vector3(minX + lineWidth, minY, 0));
                GL.Vertex(new Vector3(minX - lineWidth, minY, 0));
                GL.Vertex(new Vector3(minX - lineWidth, maxY, 0));
                GL.Vertex(new Vector3(minX + lineWidth, maxY, 0));

                GL.Vertex(new Vector3(minX, maxY + lineWidth, 0));
                GL.Vertex(new Vector3(minX, maxY - lineWidth, 0));
                GL.Vertex(new Vector3(maxX, maxY - lineWidth, 0));
                GL.Vertex(new Vector3(maxX, maxY + lineWidth, 0));

                GL.Vertex(new Vector3(maxX + lineWidth, minY, 0));
                GL.Vertex(new Vector3(maxX - lineWidth, minY, 0));
                GL.Vertex(new Vector3(maxX - lineWidth, maxY, 0));
                GL.Vertex(new Vector3(maxX + lineWidth, maxY, 0));
            }
            GL.End();
        }
        GL.PopMatrix();
    }

    private Vector3 GetCameraFrustumPoint(Vector3 position)
    {
        Vector3 result = new Vector3();
        Ray ray = mainCamera.ScreenPointToRay(position);

        //bool hasHitSomething = mapCollider.Raycast(ray, out RaycastHit raycastHit, Camera.main.transform.position.y * 2);
        RaycastHit2D raycastHit = Physics2D.GetRayIntersection(ray);
        bool hasHitSomething = raycastHit.collider != null;
        if (hasHitSomething)
        {
            result = raycastHit.point;
        }
        return result;
    }
}
