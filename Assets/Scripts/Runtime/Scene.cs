using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public struct Scene
    {
        internal RectTransform player1Spawner;
        internal TMPro.TMP_Text player1Score;
        internal RectTransform player2Spawner;
        internal TMPro.TMP_Text player2Score;
        internal GameObject player1GameOver;
        internal GameObject player2GameOver;
        internal RectTransform p1NextBlock;
        internal RectTransform p2NextBlock;

        internal void Player1RunGameOver()
        {
            player1GameOver.SetActive(true);
        }

        internal void Player2RunGameOver()
        {
            player2GameOver.SetActive(true);
        }
    }
}
