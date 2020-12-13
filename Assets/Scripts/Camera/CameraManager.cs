using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : AbstractSingleton<CameraManager>
{
    [Header("Self references")]
    [SerializeField] private CameraHandler cameraHandler;
    [SerializeField] private MinimapHandler minimapHandler;
    [SerializeField] private FogOfWarHandler fogOfWarHandler;

    public void MoveCamera(Vector3 movement)
    {
        cameraHandler.MoveCamera(movement);
    }
}
