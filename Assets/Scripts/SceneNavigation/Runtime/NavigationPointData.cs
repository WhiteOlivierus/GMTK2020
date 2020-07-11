using UnityEngine;

public class NavigationPointData : MonoBehaviour
{
    public bool canTurn = false;

    [ShowIf(nameof(canTurn), true, ShowIfComparisonType.Equals)]
    public bool fullCircle = true;

    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
    public int maxAngleLeft = 0;
    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
    public int maxAngleRight = 0;
}
