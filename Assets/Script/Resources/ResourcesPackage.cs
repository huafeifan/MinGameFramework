using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomNamespace
{
    [Serializable]
    public class ResourcesPackage
    {
        /// <summary>
        /// AssetĿ¼�µ�·��
        /// </summary>
        [SerializeField]
        public string ResourcesPath;

        /// <summary>
        /// ����AB����
        /// </summary>
        [SerializeField]
        public string AssetBundleName;

        /// <summary>
        /// ��Դ��
        /// </summary>
        [SerializeField]
        public string ResourecesName;

        /// <summary>
        /// AB������
        /// </summary>
        [SerializeField]
        public AssetBundle AssetBundle;
    }
}
