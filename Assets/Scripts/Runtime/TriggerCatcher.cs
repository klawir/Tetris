using UnityEngine;

public class TriggerCatcher : MonoBehaviour
{
    public System.Action<Collider2D> OnOnTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name+" "+collision.name);
        OnOnTriggerEnter2D.Invoke(collision);
    }
}
