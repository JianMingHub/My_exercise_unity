using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace COHENLI.DefenseBasic
{
    public class GameOverDialog : Dialog
    {
        public Text bestScoreTxt;

        public override void Show(bool isShow)
        {
            base.Show(isShow);

            if(bestScoreTxt)
                bestScoreTxt.text = Pref.bestScore.ToString("0000");
        }
        public void Replay()
        {
            Close();
            SceneManager.LoadScene(Const.GAMEPLAY_SCENE);
        }
        public void QuitGame()
        {
            if (Application.isEditor)
            {
                // Exit Play Mode in Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                // Exit the application during the build
                Application.Quit();
            }
        }
    }
}