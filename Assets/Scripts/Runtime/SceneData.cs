
namespace Assets.Scripts.Runtime
{
    public class SceneData
    {
        public static SceneData Instance { get; private set; }

        public Scene LogicalScene;
        public Scene GraphicalScene;

        public static void InitializeTheSingleton()
        {
            Instance = new SceneData();
        }

        public void Initialize()
        {
            LogicalScene = new Scene();
            GraphicalScene = new Scene();
        }
    }
}