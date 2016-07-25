using UnityEngine;

namespace ifup.input.example
{
    public class Ex1SphereSpring : MonoBehaviour
    {        
        public float moveDistance = 2;
        public float moveSpeed = 10;
        public float returnSpeed = 0.75f;  

        private SwipeManager SwipeManager { get { return SwipeManager.Instance; } }
        private Vector2 startingPosition0;
        private Vector3 startingPosition;
        private Vector3 targetPosition;
        private float moveStartTime;
        private SwipeData swipeData;
        private bool targetReached = false;

        void Start()
        {
            SwipeManager.OnSwipeEnded += OnSwipeEndedHandler;
            startingPosition0 = startingPosition = targetPosition = transform.position;            
        }

        void Update()
        {
            float speed = targetReached ? returnSpeed : moveSpeed;
            float distCovered = (Time.time - moveStartTime) * speed;
            float fracJourney = distCovered / moveDistance;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, fracJourney);
            if (!targetReached && Vector3.Distance(transform.position, targetPosition) == 0) {
                targetReached = true;
                targetPosition = startingPosition0;
                startingPosition = transform.position;
                moveStartTime = Time.time;
            }
        }

        void OnSwipeEndedHandler(SwipeData swipeData)
        {
            Vector3 swipeDelta3 = new Vector3(swipeData.swipeDelta.x, swipeData.swipeDelta.y, 0);            
            Vector3 moveDelta = swipeDelta3 * moveDistance;
            moveStartTime = Time.time;
            startingPosition = transform.position;
            targetPosition =  transform.position + moveDelta;
            this.swipeData = swipeData;
            targetReached = false;
        }
    }
}
