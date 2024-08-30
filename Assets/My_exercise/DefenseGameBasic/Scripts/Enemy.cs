using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class Enemy : MonoBehaviour, IComponentChecking
    {
        public float speed;
        public float atkDistance;
        private Rigidbody2D m_rb;
        private Animator m_amin;
        private Player m_player;
        private  bool m_isDead;             // check if enemy is dead
        private GameManager m_gm;

        private void Awake() 
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_amin = GetComponent<Animator>();
            m_player = FindAnyObjectByType<Player>();
            m_gm = FindObjectOfType<GameManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }
        public bool IsComponentsNull()
        {
            return m_rb == null || m_amin == null || m_player == null;
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
        // bắt va chạm giữa enemy với vũ khí, nếu vũ khí va chạm thì enemy sẽ chết, nhớ check IsTrigger của Collider và bỏ Looptime của enemy
        // gắn file Weapon vào weapon player
        // set enemy vào layer và tag enemy và hero vào layer và tag hero, 
        public void Die()
        {
            // Debug.Log("Die");
            if (IsComponentsNull() || m_isDead) return;

            m_isDead = true;
            m_amin.SetTrigger(Const.DEAD_ANIM);
            m_rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer(Const.DEAD_ANIM);
            if (m_gm)
                m_gm.Score++;
            Destroy(gameObject,2f);
        }
    }
}

