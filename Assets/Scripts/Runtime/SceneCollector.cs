using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class SceneCollector : MonoBehaviour
    {
        [SerializeField] private SceneType _sceneType;
        [field: SerializeField] internal RectTransform p1Spawn { get; private set; }
        [field: SerializeField] internal TMPro.TMP_Text p1ScoreText { get; private set; }
        [field: SerializeField] internal RectTransform p2Spawn { get; private set; }
        [field: SerializeField] internal RectTransform p2Score { get; private set; }
        [field: SerializeField] internal TMPro.TMP_Text p2ScoreText { get; private set; }
        [field: SerializeField] internal GameObject player1GameOver { get; private set; }
        [field: SerializeField] internal GameObject player2GameOver { get; private set; }
        [field: SerializeField] internal RectTransform p1NextBlock { get; private set; }
        [field: SerializeField] internal RectTransform p2NextBlock { get; private set; }

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
                    SceneData.Instance.graphical.player1Score = p1ScoreText;
                    SceneData.Instance.graphical.player2Score = p2ScoreText;
                    SceneData.Instance.graphical.player2Spawner = p2Spawn;
                    SceneData.Instance.graphical.player1GameOver = player1GameOver;
                    SceneData.Instance.graphical.player2GameOver = player2GameOver;
                    SceneData.Instance.graphical.p1NextBlock = p1NextBlock;
                    SceneData.Instance.graphical.p2NextBlock = p2NextBlock;
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