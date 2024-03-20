using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace hungw
{
    public class WindowVottingController : MonoBehaviour
    {
        [Serializable]
        public class MyEvent : UnityEvent { }

        [Header("Run when start votting"), Space(5f)]
        [FormerlySerializedAs("CustomEvent")]
        [SerializeField] private MyEvent _customEvent = new MyEvent();
        /// <summary>
        /// Run when end game
        /// </summary>
        /// <value></value>
        public MyEvent CustomEvent
        {
            get => _customEvent;
            set { _customEvent = value; }
        }
        [SerializeField] List<GameObject> stars;
        private Database firbase;

        public void EnventVoteGame()
        {
            UISystemProfilerApi.AddMarker("MyEvent.CustomEvent", this);
            _customEvent.Invoke();
        }

        void OnEnable()
        {
            EnventVoteGame();
        }
        void Awake()
        {
            firbase = GameObject.FindWithTag("firebase").GetComponent<Database>();
        }
        // Update is called once per frame

        public void Vote(int value)
        {
            PlayerPrefs.SetInt("starCount", value);
            firbase.UpdateProperties("starCount", value);

            for (int i = 0; i < value; i++)
            {
                stars[i].GetComponent<Animator>().Play("full");
            }
            for (int i = value; i < stars.Count; i++)
            {
                stars[i].GetComponent<Animator>().Play("idle");
            }
        }
    }
}
