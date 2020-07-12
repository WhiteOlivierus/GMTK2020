﻿using DutchSkull.Singleton;
using System;
using UnityEngine;

public class InputManager : SingleSceneSingleton<InputManager>
{
    public Action playerMoved;

    protected override void Awake() => SetInstance(this);

    private void Start()
    {
        PlayerData.transform.position = PlayerData.currentNavigationPoint.transform.position;
        playerMoved.Invoke();
    }

    [SerializeField] private MouseEmotionState testState = default;

    private void Update()
    {
        TurnCamera();

        Interact();

        ChangeMouseState();
    }

    private void TurnCamera()
    {
        PlayerData.cameraController.Update();

        if (Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.LeftArrow))
            PlayerData.cameraController.SetDirection(-1);

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
            PlayerData.cameraController.SetDirection(1);
    }

    private void Interact()
    {
        GameObject navigationTrigger = GetRaycasted();

        if (navigationTrigger == null ||
            !navigationTrigger.TryGetComponent(out Raycastable raycastable))
            return;

        raycastable.OnHover();

        if (!Input.GetMouseButtonDown(0))
            return;

        raycastable.Interact();
    }

    private void ChangeMouseState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            MouseController.SetMouseState(MouseEmotion.Calm);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            MouseController.SetMouseState(MouseEmotion.Grumpy);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            MouseController.SetMouseState(MouseEmotion.Murdery);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            MouseController.SetMouseState(testState);
    }

    private GameObject GetRaycasted()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit))
            return null;

        return hit.transform.gameObject;
    }

    private PlayerData PlayerData => PlayerData.Instance;

    private MouseController MouseController => MouseController.Instance;
}
