using Assets.Scripts.Now;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(IsGroundCheck))]
    public class EntityProperties : MonoBehaviour, IEntityProperties
    {
        //引用变量
        protected Rigidbody2D rb;
        public IsGroundCheck footCheck;
        public Rigidbody2D Rb { get => rb; }
        public event Action OnHurtEvent;
        public event Action OnDeathEvent;
        [SerializeField] private PhysicsMaterial2D SmoothMaterial;
        [SerializeField] private PhysicsMaterial2D RoughMaterial;

        [Header("预设变量")]
        [SerializeField] protected float speed = 3f;
        [SerializeField] protected float jumpForce = 5f;
        [SerializeField] protected float maxHealth = 20;
        [SerializeField] protected float damage = 2;
        [SerializeField] protected ControlMode controlMode;
        [SerializeField] protected int defaultFacing = 1;
        public float Speed { get => speed; }
        public float JumpForce { get => jumpForce; }
        public enum ControlMode { Input, AI }
        public int DefaultFacing { get => defaultFacing; }

        //"实时变量"
        public bool IsGround { get => footCheck.IsGround; }
        public float Health { get; protected set; }
        public float Damage { get => damage; }
        public Vector2 Velocity { get=>rb.velocity; }

        protected void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            footCheck = GetComponent<IsGroundCheck>();

            Health = maxHealth;
        }

        protected void Update()
        {
            JumpSmooth();
        }


        //防止卡墙, 跳跃时变为光滑材质
        protected void JumpSmooth()
        {
            rb.sharedMaterial = IsGround ? RoughMaterial:SmoothMaterial;
        }


        ////////////////属性事件
        public void TakeDamage(ILivingBeing target)
        {
            target.OnHurt(Damage);
        }

        public virtual void OnHurt(float damage)
        {
            if (Health-damage <= 0)
            {
                Health = 0;
                OnDeathEvent?.Invoke();
            }
            else
            {
                Health -= damage;
                OnHurtEvent?.Invoke();
            }
        }

        /// <summary>
        /// 实体移动事件:  <para/>
        /// 传入指定方向 <paramref name="dir"/> 进行移动, 并自动面向移动方向
        /// </summary>
        /// <param name="dir">移动方向</param>
        public void Move(int dir)
        {
            Rigidbody2D rb = this.rb;
            rb.velocity = new Vector2(dir * Speed, rb.velocity.y);

            //调整朝向
            if (dir != 0) transform.localScale = new Vector3(DefaultFacing * dir, 1, 1);

            //Debug.Log("TestMove(): "+"dir: " + dir + ", vel: " +rb.velocity+", facing: "+transform.localScale.x);//单元测试
        }

        /// <summary>
        /// 实体跳跃事件: <para/>
        /// 添加<c>JumpForce</c>力度起跳
        /// </summary>
        public void Jump()
        {
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
        ////////////////////////////////////////////////
    }
}