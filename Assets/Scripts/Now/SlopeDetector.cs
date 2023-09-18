using UnityEngine;

public class SlopeDetector : MonoBehaviour
{
    public float slopeSpeedMultiplier = 0.5f; // 斜坡速度比例
    public float SlopeSpeedMultiplier2;

    public float slopeAngle; // 斜坡角度
    public bool isOnSlope; // 是否在斜坡上
    public Vector2 xAxisVec;
    public Vector2 yAxisVec;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ground"))) return;
        if (collision.contacts.Length > 0) // 如果有碰撞点
        {
            ContactPoint2D contact = collision.contacts[0];
            var slopeAn = Vector3.Angle(contact.normal, Vector3.up);
            var slopeDir = Mathf.Sign(Vector3.Dot(contact.normal, Vector3.right));
            slopeAngle = slopeAn * slopeDir;
        }
    }

    private void Update()
    {
        SlopeSpeedMultiplier2 = GetSlopeSpeedMultiplier()*2;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnSlope = false;
    }

    public float GetSlopeAngle()
    {
        return slopeAngle;
    }

    public bool IsOnSlope()
    {
        return isOnSlope;
    }

    public float GetSlopeSpeedMultiplier()
    {
        return Mathf.Lerp(1f, slopeSpeedMultiplier, slopeAngle / 45f); // 根据斜坡角度计算速度比例
    }
}
