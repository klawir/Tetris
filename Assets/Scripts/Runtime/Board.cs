using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class Board
    {
        internal RectTransform SpawnRectTransformForLogicalPart { get; private set; }
        internal RectTransform SpawnRectTransformForGraphicsPart { get; private set; }
        internal RectTransform RootRectTransform { get; private set; }
        internal RectTransform NextBlockRectTransform { get; private set; }
        internal GameObject SpawnedNextBlock { get; private set; }
        internal bool HasSpawnedNextBlock { get; private set; }

        public float GetLeftBound { get; private set; }
        public float GetRightBound { get; private set; }

        public Board(RectTransform rootRectTransform,
            RectTransform spawnRectTransformForLogicalPart,
            RectTransform spawnRectTransformForGraphicsPart,
            RectTransform nextBlockRectTransform)
        {
            SpawnRectTransformForLogicalPart = spawnRectTransformForLogicalPart;
            SpawnRectTransformForGraphicsPart = spawnRectTransformForGraphicsPart;
            RootRectTransform = rootRectTransform;
            GetLeftBound = RootRectTransform.position.x - (RootRectTransform.rect.width / 2);
            GetRightBound= RootRectTransform.position.x + (RootRectTransform.rect.width / 2);
            NextBlockRectTransform = nextBlockRectTransform;
        }

        internal void CreateBlockAtTheQueue(GameObject prefabRender)
        {
            if (HasSpawnedNextBlock)
            {
                UnityEngine.Object.Destroy(SpawnedNextBlock);
            }

            SpawnedNextBlock = UnityEngine.Object.Instantiate(prefabRender, NextBlockRectTransform);
            HasSpawnedNextBlock = true;
        }
    }
}
