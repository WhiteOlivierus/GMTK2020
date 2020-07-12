using System.Linq;
using UnityEngine;

public abstract class Raycastable : MonoBehaviour
{
    [SerializeField] private GameObject objectToHighlight = default;
    [SerializeField] private Color highlightColor = Color.red;

    private bool isHovering = false;

    private Material[] materials = new Material[0];

    private Color[] originalColor = new Color[0];

    private void Awake()
    {
        if (objectToHighlight == null)
            return;

        GetAllChildMaterials();

        originalColor = new Color[materials.Length];

        for (int i = 0; i < materials.Length; i++)
            originalColor[i] = materials[i].color;
    }

    private void Update()
    {
        Highlight();

        if (!isHovering)
        {
            UnHighlight();
            return;
        }
    }

    private void LateUpdate() => isHovering = false;

    private void Highlight()
    {
        for (int i = 0; i < materials.Length; i++)
            materials[i].color = highlightColor;
    }

    private void UnHighlight()
    {
        for (int i = 0; i < materials.Length; i++)
            materials[i].color = originalColor[i];
    }

    private void GetAllChildMaterials()
    {
        Renderer[] renderers = objectToHighlight.GetComponentsInChildren<Renderer>();
        materials = renderers.Select(renderer => renderer.material).ToArray();
    }

    public virtual void OnHover()
    {
        isHovering = true;
        Highlight();
    }

    public abstract void Interact();
}
