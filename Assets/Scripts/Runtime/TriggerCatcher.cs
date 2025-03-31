using UnityEngine;

public class TriggerCatcher : MonoBehaviour
{
    public System.Action<Collider2D> OnOnTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnOnTriggerEnter2D.Invoke(collision);
    }
}
