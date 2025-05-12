using System;
using UnityEngine;

namespace CustomNamespace
{
    [Serializable]
    public class UICache
    {
        public UICache(string path, float startTime, GameObject gameObject)
        {
            mPath = path;
            mStartTime = startTime;
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }

        /// <summary>
        /// ��Դ·��
        /// </summary>
        [SerializeField]
        private string mPath;
        public string Path { get { return mPath; } }

        /// <summary>
        /// ��ʼ���ʱ��
        /// </summary>
        [SerializeField]
        private float mStartTime;

        /// <summary>
        /// �������ʱ��
        /// </summary>
        private float mEndTime { get { return mStartTime + 60; } }

        public GameObject gameObject { get; set; }

        public Transform transform { get; set; }

        /// <summary>
        /// ˢ�´��ʱ��
        /// </summary>
        public void FlashTime(float startTime)
        {
            mStartTime = startTime;
        }

        /// <summary>
        /// ���ʱ���Ƿ����
        /// </summary>
        /// <returns></returns>
        public bool IsOverTime()
        {
            return Time.time > mEndTime;
        }

        public void Destory()
        {
            if (gameObject != null)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
