using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTwoObjects : MonoBehaviour
{
    [SerializeField] private GameObject objectOne = default;
    [SerializeField] private GameObject objectTwo = default;

    public void Toggle()
    {
        objectOne.SetActive(!objectOne.activeSelf);
        objectTwo.SetActive(!objectTwo.activeSelf);
    }
}
