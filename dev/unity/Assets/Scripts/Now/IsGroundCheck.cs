using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Now
{
    public class IsGroundCheck : MonoBehaviour
    {
        [SerializeField]
        private Vector2 footSize, footoffset;
        public LayerMask groundLayer;

        public Collider2D IsGround { get; private set; }

        private void Update()
        {
            IsGround = Physics2D.OverlapBox((Vector2)transform.position + footoffset * (Vector2)transform.localScale, footSize, 0, groundLayer);
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube((Vector2)transform.position + footoffset * (Vector2)transform.localScale, footSize);
        }
    }
}