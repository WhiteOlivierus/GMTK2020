using UnityEngine;

public class NavigationPointData : MonoBehaviour
{
    public bool canTurn = false;

    [ShowIf(nameof(canTurn), true, ShowIfComparisonType.Equals)]
    public int turnsLeft = 0;
    [ShowIf(nameof(canTurn), true, ShowIfComparisonType.Equals)]
    public int turnsRight = 0;
}
