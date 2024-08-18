using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Player : MonoBehaviour
    {
        private Animator m_amin;
        public float atkRate;
        private float m_curAtkRate;
        private bool m_isAttacked;

        private void Awake() {
            m_amin = GetComponent<Animator>();
            m_curAtkRate = atkRate;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }
        public bool IsComponentsNull()
        {
            return m_amin == null;
        }
        // Update is called once per frame
        void Update()
        {
            if(IsComponentsNull()) return;

            if(Input.GetMouseButtonDown(0) && !m_isAttacked)
            {
                Debug.Log("Player clicked mouse button");
                m_amin.SetBool(Const.ATTACK_ANIM, true);
                m_isAttacked = true;    // sau khi tấn công xong thì chuyển sang true
            }

            if(m_isAttacked)
            {
                m_curAtkRate -= Time.deltaTime;
                if(m_curAtkRate <= 0)
                {
                    m_isAttacked = false;
                    m_curAtkRate = atkRate;
                }
            }
        }
        public void ResetAtkAnim()
        {
            if(IsComponentsNull()) return;
            m_amin.SetBool(Const.ATTACK_ANIM, false);
        }
    }
}

