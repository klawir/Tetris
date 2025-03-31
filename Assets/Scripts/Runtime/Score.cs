using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "Score", menuName = "Setup/Score")]
    public class Score : ScriptableObject
    {
        [field: SerializeField] internal int DefaultValue { get; private set; }
    }
}