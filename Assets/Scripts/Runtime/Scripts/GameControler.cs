using DG.Tweening;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Runtime.Scripts
{
    public class GameControler : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;
        [SerializeField] private RectTransform _rectTransformBoardPlayer1;
        [SerializeField] private RectTransform _rectTransformBoardPlayer2;
        [SerializeField] private GameObject _cubePrefabWithCollider;
        [SerializeField] private GameObject _cubePrefabRender;
        [SerializeField] private GameObject _player1BlockSpawn;
        [SerializeField] private int _horizontalSpeed;
        [SerializeField] private int _blockFallingSpeed = 4;
        [SerializeField] private float _leftBorderOffSet;
        [SerializeField] private float _rightBorderOffSet;

        [SerializeField] private Player _player1;
        [SerializeField] private Player _player2;
        [SerializeField] private TriggerCatcher _player1TriggerCatcher;
        [SerializeField] private TriggerCatcher _player2TriggerCatcher;

        private GameObject _spawedCubePrefabWithColliderForPlayer1;
        private GameObject _spawedCubePrefabRenderForPlayer1;
        private GameObject _spawedCubePrefabWithColliderForPlayer2;
        private GameObject _spawedCubePrefabRenderForPlayer2;
        private PlayerControl _playerControl;

        private void Awake()
        {
            SceneData.InitializeTheSingleton();
            SceneData.Instance.Initialize();
            LoadSceneAsync();
            _playerControl = new PlayerControl();
            _playerControl.Enable();

            _player1TriggerCatcher.OnOnTriggerEnter2D += RecognizeForPlayer1;
            //_player2TriggerCatcher.OnOnTriggerEnter2D += RecognizeForPlayer2;
        }

        private void FixedUpdate()
        {
            Vector2 player1Move = _playerControl.Player1.Move.ReadValue<Vector2>();
            Vector2 player2Move = _playerControl.Player2.Move.ReadValue<Vector2>();

            if (player1Move != Vector2.zero &&
                player1Move.x > 0 &&//d
                _spawedCubePrefabWithColliderForPlayer1.transform.position.x < _rectTransformBoardPlayer1.position.x + (_rectTransformBoardPlayer1.rect.width / 2) - _rightBorderOffSet ||
                player1Move.x < 0 &&
                _spawedCubePrefabWithColliderForPlayer1.transform.position.x > _rectTransformBoardPlayer1.position.x - (_rectTransformBoardPlayer1.rect.width / 2) + _leftBorderOffSet)
            {
                _spawedCubePrefabWithColliderForPlayer1.transform.DOMove(new Vector3(player1Move.x * _horizontalSpeed, _blockFallingSpeed, 0), 0.1f).SetRelative();
                _spawedCubePrefabRenderForPlayer1.transform.DOMove(new Vector3(player1Move.x * _horizontalSpeed, _blockFallingSpeed, 0), 0.1f).SetRelative();
            }

            else
            {
                _spawedCubePrefabWithColliderForPlayer1.transform.DOMove(
                    new Vector3(0, _blockFallingSpeed, 0),
                    0.1f).SetRelative();

                _spawedCubePrefabRenderForPlayer1.transform.DOMove(
                    new Vector3(0, _blockFallingSpeed, 0),
                    0.1f).SetRelative();
            }

            if (player2Move != Vector2.zero &&
                player2Move.x > 0 &&// ->
                _spawedCubePrefabWithColliderForPlayer2.transform.position.x < _rectTransformBoardPlayer2.position.x + (_rectTransformBoardPlayer2.rect.width / 2) - _rightBorderOffSet ||
                player2Move.x < 0 &&
                _spawedCubePrefabWithColliderForPlayer2.transform.position.x > _rectTransformBoardPlayer2.position.x - (_rectTransformBoardPlayer2.rect.width / 2) + _leftBorderOffSet)
            {
                _spawedCubePrefabWithColliderForPlayer2.transform.DOMove(new Vector3(player2Move.x * _horizontalSpeed, _blockFallingSpeed, 0), 0.1f).SetRelative();
                _spawedCubePrefabRenderForPlayer2.transform.DOMove(new Vector3(player2Move.x * _horizontalSpeed, _blockFallingSpeed, 0), 0.1f).SetRelative();
            }

            else
            {
                _spawedCubePrefabWithColliderForPlayer2.transform.DOMove(
                    new Vector3(0, _blockFallingSpeed, 0),
                    0.1f).SetRelative();

                _spawedCubePrefabRenderForPlayer2.transform.DOMove(
                    new Vector3(0, _blockFallingSpeed, 0),
                    0.1f).SetRelative();
            }
        }

        private void RecognizeForPlayer1(Collider2D collider2D)
        {
            _player1.OnCollision();

            _spawedCubePrefabWithColliderForPlayer1 = Instantiate(_cubePrefabWithCollider.gameObject, SceneData.Instance.logical.player1Spawner);
            _spawedCubePrefabRenderForPlayer1 = Instantiate(_cubePrefabRender.gameObject, SceneData.Instance.graphical.player1Spawner);

            bool numOverlaps = collider2D.GetComponent<RectTransform>().position.y>=
                SceneData.Instance.logical.player1Spawner.position.y;

            if (numOverlaps)
            {
                _player1TriggerCatcher.OnOnTriggerEnter2D -= RecognizeForPlayer1;
            }

            _player1.block.LogicalPart = _spawedCubePrefabWithColliderForPlayer1; 
            _player1.block.GraphicsPart = _spawedCubePrefabRenderForPlayer1;
        }

        private void RecognizeForPlayer2(Collider2D collision)
        {

        }

        private void LoadSceneAsync()
        {
            StartCoroutine(LoadSceneCoroutine());
        }

        private System.Collections.IEnumerator LoadSceneCoroutine()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneIndex, LoadSceneMode.Additive);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            _spawedCubePrefabWithColliderForPlayer1 = Instantiate(_cubePrefabWithCollider.gameObject, SceneData.Instance.logical.player1Spawner.transform);
            _spawedCubePrefabRenderForPlayer1 = Instantiate(_cubePrefabRender.gameObject, SceneData.Instance.graphical.player1Spawner.transform);

            _player1.block.LogicalPart = _spawedCubePrefabWithColliderForPlayer1;
            _player1.block.GraphicsPart = _spawedCubePrefabRenderForPlayer1;

            _spawedCubePrefabWithColliderForPlayer2 = Instantiate(_cubePrefabWithCollider.gameObject, SceneData.Instance.logical.player2Spawner.transform);
            _spawedCubePrefabRenderForPlayer2 = Instantiate(_cubePrefabRender.gameObject, SceneData.Instance.graphical.player2Spawner.transform);
        }
    }
}