using UnityEngine;

namespace Assets.Scripts.Runtime.Scripts
{
    [System.Serializable]
    public class Player
    {
        [SerializeField] private string _name;
        [SerializeField] private int _score;
    }
}