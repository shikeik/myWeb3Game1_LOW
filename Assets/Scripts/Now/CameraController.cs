using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace com.goldsprite.LegendOfWarriors3_StateMachineLearn
{

    /// <summary>
    /// V3:  <para/>
    ///     1. 在玩家不动时, 相机平移到玩家前上方 <para/>
    ///     2. 玩家移动时, 相机偏移到玩家朝向前方一段距离 <para/>
    ///     3. 玩家下落时, 相机跟随玩家本身位置. <para/>
    /// </summary>
    public class CameraController: MonoBehaviour
    {
        public Camera followCamera;
        private Rigidbody2D rb;

        //跟随位置偏移量
        [SerializeField] private Vector2 offset;
        //移动方向偏移量
        [SerializeField] private Vector2 moveOffset;
        //相机移动速率
        [SerializeField] private float moveVelRate = 3;

        //实时跟随位置
        [SerializeField] private Vector3 targetPos;


        private void OnEnable()
        {
            followCamera = Camera.main;
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            UpdateTargetPos();
            TranslateCamera();
        }

        private void OnDrawGizmosSelected()
        {
            UpdateTargetPos();
            //在Scene绘制Gizmo图标
            Gizmos.DrawWireSphere(targetPos, 0.3f);
        }

        //计算跟随点位置
        private void UpdateTargetPos()
        {
            var velo = Vector3.zero;
            if (rb != null) velo = rb.velocity;
            var offPos = transform.position + new Vector3(offset.x * transform.localScale.x, offset.y * (ToGroundDistance()>1f&&velo.y<0?0f:1));
            targetPos = offPos;
        }

        //移动相机
        private void TranslateCamera()
        {
            Vector3 moveVel = (Vector2)(targetPos - followCamera.transform.position) * 1/60f * moveVelRate;
            followCamera.transform.position += moveVel;
        }


        /// <summary>
        /// 距离地面一定距离才算跌落, 防止斜坡跌落动画
        /// </summary>
        /// <returns></returns>
        private float ToGroundDistance()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            if (hit)
            {
                return hit.distance;
            }
            return -1;

        }
    }
}
