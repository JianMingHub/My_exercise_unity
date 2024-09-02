using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace COHENLI.DefenseBasic
{
    public class ShopDialog : Dialog, IComponentChecking
    {
        public Transform gridRoot;
        public ShopItemUI itemUIPrefab;
        private ShopManager m_shopMng;
        private GameManager m_gm;

        public override void Show(bool isShow)
        {
            // Pref.coins = 1000;
            base.Show(isShow);
            m_shopMng = FindObjectOfType<ShopManager>();
            m_gm = FindObjectOfType<GameManager>();
            UpdateUI();
        }

        public bool IsComponentsNull()
        {
            return m_shopMng == null || m_gm == null || gridRoot == null;
        }
        private void UpdateUI()
        {
            if (IsComponentsNull()) return;

            ClearChild();

            var items = m_shopMng.items;
            if (items == null || items.Length <= 0) return;

            for (int i = 0; i < items.Length; i++)
            {
                int idx = i;
                var item = items[idx];
                // create hero
                var itemUIClone = Instantiate(itemUIPrefab, Vector3.zero, Quaternion.identity);
                itemUIClone.transform.SetParent(gridRoot);      // set in gridRoot
                itemUIClone.transform.localScale = Vector3.one; // set Scale (1,1,1)
                itemUIClone.transform.localPosition = Vector3.zero; // reset Position (0,0,0)

                // upate random hero
                itemUIClone.UpdateUI(item, idx);
                if (itemUIClone.btn)
                {
                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => ItemEvent(item, idx));
                }
            }
        }
        // mua hero
        private void ItemEvent(ShopItem item, int itemIdx)
        {
            if (item == null) return;
            bool isUnlocked = Pref.GetBool(Const.PLAYER_PREFIX_PREF + itemIdx);
            if (isUnlocked)
            {
                if (itemIdx == Pref.curPlayerId) return;
                Pref.curPlayerId = itemIdx;
                
                UpdateUI();
            } 
            else if (Pref.coins >= item.price)
            {
                Pref.coins -= item.price;
                Pref.SetBool(Const.PLAYER_PREFIX_PREF + itemIdx, true);
                Pref.curPlayerId = itemIdx;

                UpdateUI();

                GUIManager.Ins.UpdateMainCoins();
            }
            else
            {
                Debug.LogWarning("Not enough money");
            }
        }
        // xóa hero khi người dùng nhấn close
        public void ClearChild() 
        {
            if (gridRoot == null || gridRoot.childCount <= 0) return;

            for (int i = 0; i < gridRoot.childCount; i++)
            {
                var child = gridRoot.GetChild(i);
                if (child)
                    Destroy(child.gameObject);
            }
        }
    }
}


