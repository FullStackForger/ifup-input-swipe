using UnityEngine;
using System.Collections;

namespace ifup.input
{
    public class SwipeManager : Singleton<SwipeManager>
    {
        public delegate void OnSwipeStartedEvent(SwipeData swipeData);
        public delegate void OnSwipeInProgressEvent(SwipeData swipeData);
        public delegate void OnSwipeEndedEvent(SwipeData swipeData);

        public OnSwipeStartedEvent OnSwipeStarted;
        public OnSwipeInProgressEvent OnSwipeInProgress;
        public OnSwipeEndedEvent OnSwipeEnded;

        public SwipeAxis swipeAxis;

        public bool isSwiping { get { return m_contact; } }            

        private bool m_contact = false;        
        private SwipeData m_swipeData;

        void Update()
        {
            bool contact = false;
            Vector2 contactPosition;

            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                contact = Input.GetMouseButton(0);
                contactPosition = Input.mousePosition;
            } else {
                contact = (Input.touchCount > 0);
                contactPosition = Input.touches[0].position;
            }
            
            if (contact) {               
                if (!m_contact) {
                    m_swipeData = new SwipeData() {
                        swipeAxis = swipeAxis,
                        swipeStartTme = Time.time,
                        swipeStartPosition = contactPosition
                    };
                    if (OnSwipeStarted != null) OnSwipeStarted(m_swipeData);
                }
                float contactTime = Time.time;
                m_swipeData.swipeDeltaTime = contactTime - m_swipeData.swipeStartTme;
                m_swipeData.swipeDeltaPosition = contactPosition - m_swipeData.swipeStartPosition;

                if (OnSwipeInProgress != null) OnSwipeInProgress(m_swipeData);
            } else if (m_contact) {
                m_swipeData.swipeEndPosition = contactPosition;
                m_swipeData.swipeStopTime = Time.time;
                if (OnSwipeEnded != null) OnSwipeEnded(m_swipeData);
            }

            m_contact = contact;
        }
    }
}