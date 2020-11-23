using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform cameraHolder;

    [Header("Settings")]
    [SerializeField] private float movementSpeed;

    public void MoveCamera(Vector3 movement)
    {
        float speed = movementSpeed * Time.deltaTime;
        Vector3 translation = movement;
        translation *= speed;
        cameraHolder.Translate(translation, Space.World);
        //TODO: limit movement to map bounds
    }
}
