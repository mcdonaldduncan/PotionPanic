using UnityEngine;

public class AmmoIndicator : MonoBehaviour
{
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetMaterial(Material material)
    {
        if (material == meshRenderer.material) return;

        meshRenderer.material = material;
    }

}
