using UnityEngine;

[System.Serializable]
public class Block
{
    public GameObject LogicalPart;
    public GameObject GraphicsPart;

    public void DeleteRigidbody2D()
    {
        Object.Destroy(LogicalPart.GetComponent<Rigidbody2D>());
    }
}
