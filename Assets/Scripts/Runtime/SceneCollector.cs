using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class SceneCollector : MonoBehaviour
    {
        [SerializeField] private SceneType _sceneType;
        [SerializeField] private RectTransform _player1Spawn;
        [SerializeField] private TMPro.TMP_Text _player1ScoreText;
        [SerializeField] private RectTransform _player2Spawn;
        [SerializeField] private TMPro.TMP_Text _player2ScoreText;
        [SerializeField] private GameObject _player1GameOver;
        [SerializeField] private GameObject _player2GameOver;
        [SerializeField] private RectTransform _player1NextBlock;
        [SerializeField] private RectTransform _player2NextBlock;

        private void Awake()
        {
            switch (_sceneType)
            {
                case SceneType.Logical:
                    SceneData.Instance.LogicalScene.Player1Spawner = _player1Spawn;
                    SceneData.Instance.LogicalScene.Player2Spawner = _player2Spawn;
                    break;
                case SceneType.Graphic:
                    SceneData.Instance.GraphicalScene.Player1Spawner = _player1Spawn;
                    SceneData.Instance.GraphicalScene.Player1Score = _player1ScoreText;
                    SceneData.Instance.GraphicalScene.Player2Score = _player2ScoreText;
                    SceneData.Instance.GraphicalScene.Player2Spawner = _player2Spawn;
                    SceneData.Instance.GraphicalScene.Player1GameOver = _player1GameOver;
                    SceneData.Instance.GraphicalScene.Player2GameOver = _player2GameOver;
                    SceneData.Instance.GraphicalScene.Player1NextBlock = _player1NextBlock;
                    SceneData.Instance.GraphicalScene.Player2NextBlock = _player2NextBlock;
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