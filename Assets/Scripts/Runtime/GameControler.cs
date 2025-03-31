using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Runtime
{
    public class GameControler : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private int _sceneIndex;
        [SerializeField] private RectTransform _rectTransformBoardPlayer1;
        [SerializeField] private RectTransform _rectTransformBoardPlayer2;
        [SerializeField] private GameObject _cubePrefabWithCollider;
        [SerializeField] private GameObject _cubePrefabRender;
        [SerializeField] private GameObject _cube2PrefabWithCollider;
        [SerializeField] private GameObject _cube2PrefabRender;
        [SerializeField] private GameObject _player1BlockSpawn;
        [SerializeField] private int _horizontalSpeed;
        [SerializeField] private int _blockFallingSpeed = 4;
        [SerializeField] private float _leftBorderOffSet;
        [SerializeField] private float _rightBorderOffSet;

        [SerializeField] private Player _player1;
        [SerializeField] private Player _player2;
        [SerializeField] private TriggerCatcher _player1TriggerCatcher;
        [SerializeField] private TriggerCatcher _player2TriggerCatcher;

        private int _randomIndexForPlayer1;
        private int _randomIndexForPlayer2;
        private PlayerControl _playerControl;
        private int _defaultPoint;
        private UnityEngine.InputSystem.InputAction _player1InputAction;
        private UnityEngine.InputSystem.InputAction _player2InputAction;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            Board Player1Board = new Board(_rectTransformBoardPlayer1,
                SceneData.Instance.logical.player1Spawner,
                SceneData.Instance.graphical.player1Spawner, 
                SceneData.Instance.graphical.p1NextBlock);

            Board Player2Board = new Board(_rectTransformBoardPlayer2,
                SceneData.Instance.logical.player2Spawner,
                SceneData.Instance.graphical.player2Spawner,
                SceneData.Instance.graphical.p2NextBlock);

            _player1 = new Player("player1", Player1Board);
            _player2 = new Player("player2", Player2Board);

            var p1Score = SceneData.Instance.graphical.player1Score;
            _player1.OnAddScore += (value) =>
            {
                p1Score.SetText(value.ToString());
            };

            var p2Score = SceneData.Instance.graphical.player2Score;
            _player2.OnAddScore += (value) =>
            {
                p2Score.SetText(value.ToString());
            };

            _randomIndexForPlayer1 = Random.Range(1, 3);
            SpawnNewBlock(_player1, _randomIndexForPlayer1);
            _randomIndexForPlayer1 = Random.Range(1, 3);
            CreateBlockAtTheQueue(_player1, _randomIndexForPlayer1);

            _randomIndexForPlayer2 = Random.Range(1, 3);
            SpawnNewBlock(_player2, _randomIndexForPlayer2);
            _randomIndexForPlayer2 = Random.Range(1, 3);
            CreateBlockAtTheQueue(_player2, _randomIndexForPlayer2);
        }

        private void FixedUpdate()
        {
            Vector2 player1Move = _player1InputAction.ReadValue<Vector2>();
            Vector2 player2Move = _player2InputAction.ReadValue<Vector2>();

            if (HasPlayerPressedAnyMoveKey(player1Move) &&
                player1Move.x > 0 &&//d
                CanMoveRight(_player1, _rectTransformBoardPlayer1) ||

                player1Move.x < 0 &&
                CanMoveLeft(_player1, _rectTransformBoardPlayer1))
            {
                _player1.MoveHorizontalBlock(player1Move, _horizontalSpeed, _blockFallingSpeed);
            }

            else
            {
                _player1.MoveVerticalBlock(_blockFallingSpeed);
            }

            if (HasPlayerPressedAnyMoveKey(player2Move)&&
                player2Move.x > 0 && //->
                CanMoveRight(_player2, _rectTransformBoardPlayer2) ||

                player2Move.x < 0 &&
                CanMoveLeft(_player2, _rectTransformBoardPlayer2))
            {
                _player2.MoveHorizontalBlock(player2Move, _horizontalSpeed, _blockFallingSpeed);
            }

            else
            {
                _player2.MoveVerticalBlock(_blockFallingSpeed);
            }
        }

        private void Initialize()
        {
            SceneData.InitializeTheSingleton();
            SceneData.Instance.Initialize();
            SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);
            _playerControl = new PlayerControl();
            _playerControl.Enable();

            _player1TriggerCatcher.OnOnTriggerEnter2D += RecognizeForPlayer1;
            _player2TriggerCatcher.OnOnTriggerEnter2D += RecognizeForPlayer2;
            _defaultPoint = _score.DefaultValue;

            _player1InputAction = _playerControl.Player1.Move;
            _player2InputAction = _playerControl.Player2.Move;
        }

        private void CreateBlockAtTheQueue(Player player, int randomIndex)
        {
            switch (randomIndex)
            {
                case 1:
                    player.CreateBlockAtTheQueue(_cubePrefabRender);
                    break;

                case 2:
                    player.CreateBlockAtTheQueue(_cube2PrefabRender);
                    break;
            }
        }

        private void SpawnNewBlock(Player player, int optionNumber)
        {
            switch (optionNumber)
            {
                case 1:
                    player.SpawnNewBlock(_cubePrefabWithCollider,
                        SceneType.Logical,
                        player.Board.SpawnRectTransformForLogicalPart);

                    player.SpawnNewBlock(_cubePrefabRender,
                        SceneType.Graphic, 
                        player.Board.SpawnRectTransformForGraphicsPart);
                    break;

                case 2:
                    player.SpawnNewBlock(_cube2PrefabWithCollider,
                        SceneType.Logical,
                        player.Board.SpawnRectTransformForLogicalPart);

                    player.SpawnNewBlock(_cube2PrefabRender,
                        SceneType.Graphic,
                        player.Board.SpawnRectTransformForGraphicsPart);
                    break;
            }
        }

        private void RecognizeForPlayer1(Collider2D collider2D)
        {
            _player1.OnCollision();

            SpawnNewBlock(_player1, _randomIndexForPlayer1);

            bool isGameOver = collider2D.GetComponent<RectTransform>().position.y >=
                SceneData.Instance.logical.player1Spawner.position.y;

            _player1.AddScore(_defaultPoint);
            if (isGameOver)
            {
                _player1TriggerCatcher.OnOnTriggerEnter2D -= RecognizeForPlayer1;
                RunGameOver((int)SceneType.Score);
                SceneData.Instance.graphical.Player1RunGameOver();
            }

            _randomIndexForPlayer1 = Random.Range(1, 3);
            CreateBlockAtTheQueue(_player1, _randomIndexForPlayer1);
        }

        private void RecognizeForPlayer2(Collider2D collider2D)
        {
            _player2.OnCollision();
            SpawnNewBlock(_player2, _randomIndexForPlayer2);
            bool isGameOver = collider2D.GetComponent<RectTransform>().position.y >=
                SceneData.Instance.logical.player2Spawner.position.y;

            _player2.AddScore(_defaultPoint);
            if (isGameOver)
            {
                _player2TriggerCatcher.OnOnTriggerEnter2D -= RecognizeForPlayer2;
                RunGameOver((int)SceneType.Score);
                SceneData.Instance.graphical.Player2RunGameOver();
            }

            _randomIndexForPlayer2 = Random.Range(1, 3);
            CreateBlockAtTheQueue(_player2, _randomIndexForPlayer2);
        }

        private void RunGameOver(int sceneIndex, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIndex, sceneMode));
            enabled = false;
        }

        private bool HasPlayerPressedAnyMoveKey(Vector2 player1Move)
        {
            return player1Move != Vector2.zero;
        }

        private bool CanMoveLeft(Player player, RectTransform rectTransformBoardPlayer)
        {
            float playerLeftBorder = player.GetLeftBound() + _leftBorderOffSet;
            return player.CanMoveLeft(playerLeftBorder);
        }

        private bool CanMoveRight(Player player, RectTransform rectTransformBoardPlayer)
        {
            float playerRightBorder = player.GetRightBound() - _rightBorderOffSet;
            return player.CanMoveRight(playerRightBorder);
        }

        private System.Collections.IEnumerator LoadSceneCoroutine(int sceneIndex, LoadSceneMode sceneMode)
        {
            yield return new WaitForSeconds(3);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, sceneMode);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}