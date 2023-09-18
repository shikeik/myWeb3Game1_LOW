
using UnityEngine;

namespace Assets.TestScripts
{
    [ExecuteInEditMode]
    internal class Test6: MonoBehaviour
    {
        [Range(0, 360)]
        public float slope;
        public float r;
        public float x, y;
        public Vector2 pos;

        private void Update()
        {
            x = r * Mathf.Cos(Mathf.Deg2Rad * slope);
            y = r * Mathf.Sin(Mathf.Deg2Rad * slope);
            transform.position = new Vector2(pos.x+x, pos.y+y);
        }
    }
}
