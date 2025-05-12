using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomNamespace
{
    [Serializable]
    public class EventPackage
    {
        /// <summary>
        /// �¼�����
        /// </summary>
        [SerializeField]
        public string Name;

        /// <summary>
        /// �¼��ص�
        /// </summary>
        private List<Action<System.Object>> mCallBack = new List<Action<object>>();

        /// <summary>
        /// ��ע����¼��б�
        /// </summary>
        [SerializeField]
        private List<string> mCallBackNameList = new List<string>();

        public EventPackage(string name)
        {
            Name = name;
        }

        public void AddEvent(Action<System.Object> action, string actionName)
        {
            mCallBack.Add(action);
            mCallBackNameList.Add(actionName);
        }

        public void RemoveEvent(Action<System.Object> action)
        {
            for (int i = 0; i < mCallBack.Count; i++)
            {
                if (mCallBack[i] == action)
                {
                    mCallBack.RemoveAt(i);
                    mCallBackNameList.RemoveAt(i);
                }
            }
        }

        public void RemoveEvent(string actionName)
        {
            for (int i = 0; i < mCallBackNameList.Count; i++)
            {
                if (mCallBackNameList[i] == actionName)
                {
                    mCallBack.RemoveAt(i);
                    mCallBackNameList.RemoveAt(i);
                }
            }
        }

        public void TriggerEvent(object arg)
        {
            Action<object>[] action = mCallBack.ToArray();
            for (int i = 0; i < action.Length; i++)
            {
                action[i].Invoke(arg);
            }
        }

        public int GetCallBackCount()
        {
            return mCallBack.Count;
        }
    }
}
