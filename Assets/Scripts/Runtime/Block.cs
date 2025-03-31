using UnityEngine;

public class Block
{
    public GameObject LogicalPart {  get; private set; }
    public GameObject GraphicsPart { get; private set; }

    public void DeleteRigidbody2D()
    {
        Object.Destroy(LogicalPart.GetComponent<Rigidbody2D>());
    }

    internal void InstantiateGraphicsPart(GameObject gameObject)
    {
        GraphicsPart = gameObject;
    }

    internal void InstantiateLogicalPart(GameObject gameObject)
    {
        LogicalPart = gameObject;
    }
}
