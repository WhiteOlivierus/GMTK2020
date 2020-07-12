using UnityEngine;

[CreateAssetMenu(fileName = "NewInventorySystem", menuName = "GMTK2020/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject
{
    public string itemName = string.Empty;
    public Texture2D itemTexture = default;
}
