using DG.Tweening;
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

        private GameObject _spawedCubePrefabWithColliderForPlayer1;
        private GameObject _spawedCubePrefabRenderForPlayer1;
        private GameObject _spawedCubePrefabWithColliderForPlayer2;
        private GameObject _spawedCubePrefabRenderForPlayer2;

        private void Awake()
        {
            SceneData.InitializeTheSingleton();
            SceneData.Instance.Initialize();
            LoadSceneAsync();
        }

        //TODO: new input system
        /*public void OnLMB()
        {
            float move = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;
            _cube.transform.Translate(new Vector3(move, 0, 0));
        }*/

        private void Update()
        {
            float move = Input.GetAxis("Horizontal") * Time.deltaTime * 5f;

            if (move > 0)
            {
                _spawedCubePrefabWithColliderForPlayer1.transform.Translate(new Vector3(move, 0, 0));
                _spawedCubePrefabRenderForPlayer1.transform.Translate(new Vector3(move, 0, 0));
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

            Transform endOfSpawn = SceneData.Instance.logical.p1SpawnEndOf;
            int speedOf = 5;
            _spawedCubePrefabWithColliderForPlayer1.transform.DOMove(endOfSpawn.position, speedOf);
            _spawedCubePrefabRenderForPlayer1.transform.DOMove(endOfSpawn.position, speedOf);

            endOfSpawn = SceneData.Instance.logical.p2SpawnEndOf;
            _spawedCubePrefabWithColliderForPlayer2.transform.DOMove(endOfSpawn.position, speedOf);
            _spawedCubePrefabRenderForPlayer2.transform.DOMove(endOfSpawn.position, speedOf);
        }
    }
}