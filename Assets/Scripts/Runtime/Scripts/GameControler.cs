using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Runtime.Scripts
{
    public class GameControler : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefabWithCollider;
        [SerializeField] private GameObject cubePrefabRender;
        [SerializeField] private int _sceneIndex;
        [SerializeField] private GameObject _player1BlockSpawn;

        private GameObject _cubePrefabWithCollider;
        private GameObject _cubePrefabRender;

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
                _cubePrefabWithCollider.transform.Translate(new Vector3(move, 0, 0));
                _cubePrefabRender.transform.Translate(new Vector3(move, 0, 0));
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

            _cubePrefabWithCollider = Instantiate(cubePrefabWithCollider.gameObject, SceneData.Instance.logical.player1Spawner.transform);
            _cubePrefabRender = Instantiate(cubePrefabRender.gameObject, SceneData.Instance.graphical.player1Spawner.transform);
        }
    }
}