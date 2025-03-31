using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Runtime
{
    public class SceneControler : MonoBehaviour
    {
        [SerializeField] private SceneType _sceneType;

        public void LoadScene()
        {
            SceneManager.LoadScene((int)_sceneType, LoadSceneMode.Single);
        }
    }
}