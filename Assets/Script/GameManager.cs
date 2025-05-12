using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomNamespace
{
    public class GameManager : BaseManager
    {
        public enum Status
        {
            /// <summary>
            /// 运行中
            /// </summary>
            Run = 1,

            /// <summary>
            /// 正在注册中
            /// </summary>
            Registering = 2,

            /// <summary>
            /// 正在注销中
            /// </summary>
            Unregistering = 3,
        }

        public bool IsEditorMode = true;
        public bool IsOpenDebug = true;

        private Status mStatus;

        private static GameManager mInstance;
        public static GameManager Instance
        {
            get
            {
                return mInstance;
            }

        }

        [SerializeField]
        private List<BaseManager> mManagerList = new List<BaseManager>();

        private void Awake()
        {
            mInstance = this;
            mStatus = Status.Run;

            StartCoroutine(Register());
        }

        private void Update()
        {
            
        }
        
        public override IEnumerator Register()
        {
            if (mStatus != Status.Run) 
                yield break;

            mStatus = Status.Registering;

            yield return Register<UIEventManager>();
            yield return Register<EventManager>();

            if (!IsEditorMode)
            {
                //yield return Register<HotfixManager>();
            }
            yield return Register<ResourcesManager>();
            yield return Register<UIManager>();
            if (IsOpenDebug)
            {
                yield return Register<DebugManager>();
            }


            AddListener();
            mStatus = Status.Run;
        }

        public override IEnumerator Unregister()
        {
            mStatus = Status.Unregistering;
            RemoveListener();

            if (!IsEditorMode)
            {
                //yield return Unregister<HotfixManager>();
            }
            yield return Unregister<UIEventManager>();
            yield return Unregister<UIManager>();
            yield return Unregister<EventManager>();
            if (IsOpenDebug)
            {
                yield return Unregister<DebugManager>();
            }

            yield return Unregister<ResourcesManager>();

            mStatus = Status.Run;
        }

        private IEnumerator Register<T>() where T : BaseManager
        {
            var component = transform.GetComponentInChildren<T>();
            mManagerList.Add(component);
            yield return component.Register();
        }

        private IEnumerator Unregister<T>() where T : BaseManager
        {
            var component = transform.GetComponentInChildren<T>();
            mManagerList.Remove(component);
            yield return component.Unregister();
        }

        public BaseManager GetManager<T>() where T : BaseManager
        {
            for (int i = 0; i < mManagerList.Count; i++)
            {
                if (mManagerList[i] is T)
                {
                    return mManagerList[i];
                }
            }
            return null;
        }

        private void AddListener()
        {
            EventManager.Instance.AddListener(EventManager.Event_Exit, OnExit, "CSharp.GameManager.OnExit");
            EventManager.Instance.AddListener(EventManager.Event_Restart, OnRestart, "CSharp.GameManager.OnRestart");
        }

        private void RemoveListener()
        {
            EventManager.Instance.RemoveListener(EventManager.Event_Exit, OnExit);
            EventManager.Instance.RemoveListener(EventManager.Event_Restart, OnRestart);
        }

        private void OnDestroy()
        {
            //Unregister();
        }

        private void OnExit(object obj)
        {
            if (mStatus == Status.Run)
                StartCoroutine(Exit());
        }

        private void OnRestart(object obj)
        {
            if (mStatus == Status.Run)
                StartCoroutine(Restart());
        }

        private IEnumerator Restart()
        {
            yield return Unregister();

            StartCoroutine(Register());
        }

        private IEnumerator Exit()
        {
            yield return Unregister();

            Application.Quit();
        }


    }
}
