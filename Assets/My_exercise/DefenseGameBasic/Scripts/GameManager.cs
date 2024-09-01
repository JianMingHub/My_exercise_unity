using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class GameManager : MonoBehaviour, IComponentChecking
    {
        public float spawnTime;         // time to spawn
        public Enemy[] enemyPrefabs;    // list of enemy
        private bool m_isGameOver;      // check if game is over
        private int m_score;            // score of the player
        public int Score { get => m_score; set => m_score = value; }            // get set by player
        public GUIManager guiMng;
        
        // Start is called before the first frame update
        void Start()
        {
            if (IsComponentsNull()) return;
            guiMng.ShowGameGUI(false);
            guiMng.UpdateMainCoins();
        }
        public bool IsComponentsNull()
        {
            return guiMng == null;
        }
        public void PlayGame()
        {
            if(IsComponentsNull()) return;
            StartCoroutine(SpawnEnemy());
            guiMng.ShowGameGUI(true);
            guiMng.UpdateGameplayCoins();
        }
        public void GameOver()
        {
            if (m_isGameOver) return;
            m_isGameOver = true;
            Pref.bestScore = m_score;

            if(guiMng.gameoverDialog)
                guiMng.gameoverDialog.Show(true);
        }
        IEnumerator SpawnEnemy()
        {
            while(!m_isGameOver)
            {
                if(enemyPrefabs != null && enemyPrefabs.Length > 0)
                {
                    // Debug.Log(m_isGameOver);
                    int ranIdx = Random.Range(0, enemyPrefabs.Length);          // get random number of enemy, not max value. Ex: (0,3) 0, 1, 2
                    Enemy enemyRefab = enemyPrefabs[ranIdx];                    // get an element from the array enemyPrefabs
                    if(enemyRefab)
                    {
                        Instantiate(enemyRefab, new Vector3(8, 0, 0), Quaternion.identity);         // create a copy of it at the location
                    }
                }

                yield return new WaitForSeconds(spawnTime);                     // wait for enemy to spawn
            }
        }

    }
}

