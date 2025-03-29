using UnityEngine;

namespace Assets.Scripts.Runtime.Scripts
{
    public class Scene
    {
        internal RectTransform player1Spawner;
        internal Transform p1SpawnEndOf;
        internal RectTransform player2Spawner;
        internal Transform p2SpawnEndOf;
        internal GameObject player1GameOver;
        internal GameObject player2GameOver;

        internal void Player1RunGameOver()
        {
            player1GameOver.SetActive(true);
        }
    }
}
