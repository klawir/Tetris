
namespace Assets.Scripts.Runtime
{
    public class SceneData
    {
        public static SceneData Instance { get; private set; }

        public Scene logical;
        public Scene graphical;

        public static void InitializeTheSingleton()
        {
            Instance = new SceneData();
        }

        public void Initialize()
        {
            logical = new Scene();
            graphical = new Scene();
        }
    }
}