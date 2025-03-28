using DG.Tweening;
using Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Runtime.Scripts
{
    public class GameControler : MonoBehaviour
    {  
        [SerializeField] private GameObject _cubePrefabWithCollider;
        [SerializeField] private GameObject _cubePrefabRender;
        [SerializeField] private int _sceneIndex;
        [SerializeField] private GameObject _player1BlockSpawn;
        [SerializeField] private int _horizontalSpeed;

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
        }

        private void FixedUpdate()
        {
            Vector2 player1Move = _playerControl.Player1.Move.ReadValue<Vector2>();
            Vector2 player2Move = _playerControl.Player2.Move.ReadValue<Vector2>();

            if (player1Move != Vector2.zero)
            {
                _spawedCubePrefabWithColliderForPlayer1.transform.DOMove(new Vector3(player1Move.x * _horizontalSpeed, -4, 0), 0.1f).SetRelative();
                _spawedCubePrefabRenderForPlayer1.transform.DOMove(new Vector3(player1Move.x * _horizontalSpeed, -4, 0), 0.1f).SetRelative();
            }

            else
            {
                _spawedCubePrefabWithColliderForPlayer1.transform.DOMove(
                    new Vector3(0, -4, 0),
                    0.1f).SetRelative();

                _spawedCubePrefabRenderForPlayer1.transform.DOMove(
                    new Vector3(0, -4, 0),
                    0.1f).SetRelative();
            }

            if (player2Move != Vector2.zero)
            {
                _spawedCubePrefabWithColliderForPlayer2.transform.DOMove(new Vector3(player2Move.x * _horizontalSpeed, -4, 0), 0.1f).SetRelative();
                _spawedCubePrefabRenderForPlayer2.transform.DOMove(new Vector3(player2Move.x * _horizontalSpeed, -4, 0), 0.1f).SetRelative();
            }

            else
            {
                _spawedCubePrefabWithColliderForPlayer2.transform.DOMove(
                    new Vector3(0, -4, 0),
                    0.1f).SetRelative();

                _spawedCubePrefabRenderForPlayer2.transform.DOMove(
                    new Vector3(0, -4, 0),
                    0.1f).SetRelative();
            }
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

            _spawedCubePrefabWithColliderForPlayer2 = Instantiate(_cubePrefabWithCollider.gameObject, SceneData.Instance.logical.player2Spawner.transform);
            _spawedCubePrefabRenderForPlayer2 = Instantiate(_cubePrefabRender.gameObject, SceneData.Instance.graphical.player2Spawner.transform);
        }
    }
}