using UnityEngine;
using System.Collections;

namespace ifup.input
{
    public class SwipeData
    {
        public SwipeAxis swipeAxis;
        public Vector2 swipeDeltaPosition;
        public Vector2 swipeStartPosition;
        public Vector2 swipeEndPosition;
        public float swipeStartTme;
        public float swipeStopTime;
        public float swipeDeltaTime;
        public Vector2 swipeDelta {
            get {
                Vector2 normalized = swipeDeltaPosition.normalized;
                switch (swipeAxis) {
                    case SwipeAxis.Diagonal: return normalized;
                    case SwipeAxis.Horizontal: return new Vector2(Mathf.Sign(normalized.x) * 1, 0);
                    case SwipeAxis.Vertical: return new Vector2(0, Mathf.Sign(normalized.y) * 1);
                    case SwipeAxis.Disabled: return Vector2.zero;                    
                }
                return Vector2.zero;
            }
        }
    }
}