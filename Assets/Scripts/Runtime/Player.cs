using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class Player
    {
        private string _name;
        private int _score;
        public System.Action<int> OnAddScore;

        public Block CurrentBlock { get; private set; }
        public Board Board {  get; private set; }

        public Player(string name, Board board)
        {
            _name = name;
            _score = 0;
            CurrentBlock = new Block();
            Board = board;
        }

        internal void OnCollision()
        {
            CurrentBlock.DeleteRigidbody2D();
        }

        internal void AddScore(int value)
        {
            _score += value;
            OnAddScore.Invoke(_score);
        }

        internal void SpawnNewBlock(UnityEngine.GameObject prefab, SceneType sceneType, RectTransform spawnPlace)
        {
            switch (sceneType)
            {
                case SceneType.Logical:
                    CurrentBlock.InstantiateLogicalPart(UnityEngine.Object.Instantiate(prefab, spawnPlace));
                    break;
                case SceneType.Graphic:
                    CurrentBlock.InstantiateGraphicsPart(UnityEngine.Object.Instantiate(prefab, spawnPlace));
                    break;
            }
        }

        internal void MoveHorizontalBlock(Vector2 playerMove, int horizontalSpeed, int blockFallingSpeed)
        {
            CurrentBlock.LogicalPart.transform.DOMove(
                new Vector3(
                    playerMove.x * horizontalSpeed,
                    blockFallingSpeed, 0), 0.1f).SetRelative();

            CurrentBlock.GraphicsPart.transform.DOMove(
                new Vector3(
                    playerMove.x * horizontalSpeed,
                    blockFallingSpeed, 0), 0.1f).SetRelative();
        }

        internal void MoveVerticalBlock(int blockFallingSpeed)
        {
            CurrentBlock.LogicalPart.transform.DOMove(
                new Vector3(0, blockFallingSpeed, 0),
                0.1f).SetRelative();

            CurrentBlock.GraphicsPart.transform.DOMove(
                new Vector3(0, blockFallingSpeed, 0),
                0.1f).SetRelative();
        }

        internal float GetLeftBound()
        {
            return Board.GetLeftBound;
        }

        internal float GetRightBound()
        {
            return Board.GetRightBound;
        }

        internal bool CanMoveLeft(float offSet)
        {
            return CurrentBlock.LogicalPart.transform.position.x > offSet;
        }

        internal bool CanMoveRight(float offSet)
        {
            return CurrentBlock.LogicalPart.transform.position.x < offSet;
        }

        internal void CreateBlockAtTheQueue(GameObject prefabRender)
        {
            Board.CreateBlockAtTheQueue(prefabRender);
        }
    }
}