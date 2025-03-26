using UnityEngine;

namespace Assets.Scripts.Runtime.Scripts
{
    public class SceneCollector : MonoBehaviour
    {
        [SerializeField] private SceneType _sceneType;
        [field: SerializeField] internal GameObject p1Spawn { get; private set; }
        [field: SerializeField] internal GameObject p2Spawn { get; private set; }

        private void Awake()
        {
            switch (_sceneType)
            {
                case SceneType.Logical:
                    SceneData.Instance.logical.player1Spawner = p1Spawn;
                    SceneData.Instance.logical.player2Spawner = p2Spawn;
                    break;
                case SceneType.Graphic:
                    SceneData.Instance.graphical.player1Spawner = p1Spawn;
                    SceneData.Instance.graphical.player2Spawner = p2Spawn;
                    break;
            }
        }
    }

    public enum SceneType
    {
        Logical = 0,
        Graphic
    }
}