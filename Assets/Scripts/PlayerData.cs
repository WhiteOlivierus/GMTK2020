﻿using DutchSkull.Singleton;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [HideInInspector] public CanvasGroup fadeGroup = default;
    [HideInInspector] public CameraController cameraController = default;
    [HideInInspector] public List<InventoryItem> inventory = new List<InventoryItem>();
    [HideInInspector] public NavigationRoot currentNavigationPoint = default;

    private void Awake()
    {
        cameraController = new CameraController(transform);
        fadeGroup = GetComponentInChildren<CanvasGroup>();
        SceneNavigation.playerMoved += cameraController.Calibrate;
    }

    private void OnDisable() => SceneNavigation.playerMoved -= cameraController.Calibrate;

    public void AddToInventory(InventoryItem item) => inventory.Add(item);

    private SceneNavigation SceneNavigation => SceneNavigation.Instance;
}
