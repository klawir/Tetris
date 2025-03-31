using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public struct Scene
    {
        internal RectTransform Player1Spawner;
        internal TMPro.TMP_Text Player1Score;
        internal RectTransform Player2Spawner;
        internal TMPro.TMP_Text Player2Score;
        internal GameObject Player1GameOver;
        internal GameObject Player2GameOver;
        internal RectTransform Player1NextBlock;
        internal RectTransform Player2NextBlock;

        internal void Player1RunGameOver()
        {
            Player1GameOver.SetActive(true);
        }

        internal void Player2RunGameOver()
        {
            Player2GameOver.SetActive(true);
        }
    }
}
