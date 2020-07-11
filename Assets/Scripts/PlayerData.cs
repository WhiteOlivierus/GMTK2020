﻿using DutchSkull.Singleton;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [HideInInspector] public CanvasGroup fadeGroup = default;
    [HideInInspector] public CameraController cameraController = default;
    public NavigationPointData currentNavigationPoint = default;

    private void Start()
    {
        cameraController = new CameraController(transform);
        fadeGroup = GetComponentInChildren<CanvasGroup>();
    }
}