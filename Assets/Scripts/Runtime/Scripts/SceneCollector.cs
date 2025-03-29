using UnityEngine;

namespace Assets.Scripts.Runtime.Scripts
{
    public class SceneCollector : MonoBehaviour
    {
        [SerializeField] private SceneType _sceneType;
        [field: SerializeField] internal RectTransform p1Spawn { get; private set; }
        [field: SerializeField] internal Transform p1SpawnEndOf { get; private set; }
        [field: SerializeField] internal RectTransform p2Spawn { get; private set; }
        [field: SerializeField] internal Transform p2SpawnEndOf { get; private set; }
        [field: SerializeField] internal GameObject player1GameOver { get; private set; }
        [field: SerializeField] internal GameObject player2GameOver { get; private set; }

        private void Awake()
        {
            switch (_sceneType)
            {
                case SceneType.Logical:
                    SceneData.Instance.logical.player1Spawner = p1Spawn;
                    SceneData.Instance.logical.p1SpawnEndOf = p1SpawnEndOf;
                    SceneData.Instance.logical.player2Spawner = p2Spawn;
                    SceneData.Instance.logical.p2SpawnEndOf = p2SpawnEndOf;
                    break;
                case SceneType.Graphic:
                    SceneData.Instance.graphical.player1Spawner = p1Spawn;
                    SceneData.Instance.graphical.player2Spawner = p2Spawn;
                    SceneData.Instance.graphical.player1GameOver = player1GameOver;
                    SceneData.Instance.graphical.player2GameOver = player2GameOver;
                    break;
            }
        }
    }

    public enum SceneType
    {
        Logical = 0,
        Graphic,
        MainMenu,
        Score
    }
}