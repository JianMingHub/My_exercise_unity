using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Pref : MonoBehaviour
    {
        // lưu điểm số cao nhất của người chơi
        public static int bestScore 
        {
            set
            {
                int oldBestScore = PlayerPrefs.GetInt(Const.BEST_SCORE_PREF, 0);         // get the best score old of the player
                if (oldBestScore < value)
                    PlayerPrefs.SetInt(Const.BEST_SCORE_PREF, value);                    // điểm số củ mà nhỏ hơn điểm số mới thì sẽ lưu đè lên điểm số củ
            }

            get => PlayerPrefs.GetInt(Const.BEST_SCORE_PREF);                           // lấy ra điểm số cao nhất dưới máy người dùng
        }
        // lấy ID hiện tại của người chơi
        public static int curPlayerId
        {
            set => PlayerPrefs.SetInt(Const.CUR_PLAYER_ID_PREF, value);
            get => PlayerPrefs.GetInt(Const.CUR_PLAYER_ID_PREF, 0);
        }
        // lấy ra coin từ máy người chơi
        public static int coins
        {
            set => PlayerPrefs.SetInt(Const.COIN_PREF, value);
            get => PlayerPrefs.GetInt(Const.COIN_PREF, 0);
        }
        // lưu volume của music xuống máy người dùng
        public static float musicVol
        {
            set => PlayerPrefs.SetFloat(Const.MUSIC_VOL_PREF, value);
            get => PlayerPrefs.GetFloat(Const.MUSIC_VOL_PREF, 0);
        }
        // lưu âm thanh xuống máy người dùng
        public static float soundVol
        {
            set => PlayerPrefs.SetFloat(Const.SOUND_VOL_PREF, value);
            get => PlayerPrefs.GetFloat(Const.SOUND_VOL_PREF, 0);
        }
        public static void SetBool(string key, bool value)
        {
            if(value)
                PlayerPrefs.SetInt(key, 1);
            else
                PlayerPrefs.SetInt(key, 0);
        }
        public static bool GetBool(string key)
        {
            int check = PlayerPrefs.GetInt(key, 0);
            return check == 1;
            // if(check == 0)
            //     return false;
            // else if(check == 1)
            //     return true;
            // return false;
        }
    }
}

