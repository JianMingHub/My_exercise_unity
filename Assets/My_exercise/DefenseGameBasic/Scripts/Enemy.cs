using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public float atkDistance;
        private Animator m_amin;
        private Rigidbody2D m_rb;
        private Player m_player;

        private void Awake() 
        {
            m_amin = GetComponent<Animator>();
            m_rb = GetComponent<Rigidbody2D>();
            m_player = FindAnyObjectByType<Player>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }
        public bool IsComponentsNull()
        {
            return m_rb == null || m_amin == null;
        }

        // Update is called once per frame
        void Update()
        {
            if(IsComponentsNull()) return;

            // tính khoảng cách giữa player và con quái
            float distToPlayer = Vector2.Distance(m_player.transform.position, transform.position);

            if(distToPlayer <= atkDistance)
            {
                m_rb.velocity = Vector2.zero;               // (0,0) dừng di chuyển con enemy lại
                m_amin.SetBool(Const.ATTACK_ANIM, true);    // chuyển sang trạng thái tấn công
            }
            else
            {
                m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);   // di chuyển con enemy
            }
        }
    }
}

