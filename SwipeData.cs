using UnityEngine;
using System.Collections;

namespace ifup.input
{
    public class SwipeData
    {
        public SwipeAxis swipeAxis;
        public Vector2 swipeDelta { get { return swipeDeltaPosition.normalized; } }
        public Vector2 swipeDeltaPosition;
        public Vector2 swipeStartPosition;
        public Vector2 swipeEndPosition;
        public float swipeStartTme;
        public float swipeStopTime;
        public float swipeDeltaTime;
    }
}