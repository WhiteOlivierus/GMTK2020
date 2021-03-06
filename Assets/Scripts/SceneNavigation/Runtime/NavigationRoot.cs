﻿using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class NavigationRoot : MonoBehaviour
{
    public bool halfTurn = false;

    public bool backButton = false;

    public bool canTurn = false;

#if UNITY_EDITOR
    [ShowIf(nameof(canTurn), true, ShowIfComparisonType.Equals)]
#endif
    public bool fullCircle = true;

#if UNITY_EDITOR
    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
#endif
    [Range(0, -180)]
    public int maxAngleLeft = 0;
#if UNITY_EDITOR
    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
#endif
    [Range(0, 180)]
    public int maxAngleRight = 0;

    public UnityEvent onArrival = default;

    public UnityEvent onExit = default;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(transform.position, direction);

        Handles.color = Color.blue;
        Handles.Label(transform.position, gameObject.name);
    }
#endif
}
