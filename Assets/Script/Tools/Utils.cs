using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CustomNamespace
{
    public static class Utils
    {
        public const string VersionInfo = "versionInfo.txt";
        public const string Manifest = "manifest.txt";
        public const string HotfixFilesList = "hotfixFilesList.txt";

        public const string Resources = "Resources";
        public const string Hotfix = "hotfix";
        public const string Lua = "lua";
        public const string Prefab = "prefab";
        public const string Texture = "ui";
        public const string Protos = "protos";
        /// <summary>
        /// ab����׺��
        /// </summary>
        public const string abEnd = "asset";

        /// <summary>
        /// ��ȡmanifest�ļ�·��
        /// </summary>
        /// <returns></returns>
        public static string GetResourcesManifestPath()
        {
            return Path.Combine(GetReleasePath(), Manifest);
        }

        /// <summary>
        /// ��ȡ�ȸ��ļ��б��ļ�·��
        /// </summary>
        /// <returns></returns>
        public static string GetHotfixFilesListPath()
        {
            return Path.Combine(GetReleasePath(), HotfixFilesList);
        }

        /// <summary>
        /// ��ȡ���ذ汾��Ϣ�ļ�·��
        /// </summary>
        /// <returns></returns>
        public static string GetVersionInfoPath()
        {
            return Path.Combine(GetReleasePath(), VersionInfo);
        }

        /// <summary>
        /// ��ȡ���ذ汾��Ϣ
        /// </summary>
        /// <returns></returns>
        //public static VersionInfo GetCurrentVersion()
        //{
        //    //��ȡ���ذ汾��Ϣ
        //    string localVersionInfoPath = Utils.GetVersionInfoPath();
        //    if (File.Exists(localVersionInfoPath))
        //    {
        //        string localVersionInfo = File.ReadAllText(localVersionInfoPath);
        //        VersionInfo versionInfo = new VersionInfo();
        //        versionInfo.Analysis(localVersionInfo);
        //        return versionInfo;
        //    }
        //    else
        //    {
        //        Debug.LogError($"���ذ汾��Ϣ�ļ�������,·��{localVersionInfoPath}");
        //        return null;
        //    }
        //}

        /// <summary>
        /// ��ȡOutputABs/ReleaseĿ¼
        /// </summary>
        public static string GetReleasePath()
        {
            return Path.Combine(GetOutputABsPath(), "Release");
        }

        /// <summary>
        /// ��ȡOutputABsĿ¼
        /// </summary>
        /// <returns></returns>
        public static string GetOutputABsPath()
        {
            return Path.Combine(Path.GetDirectoryName(Application.dataPath), "OutputABs");
        }

        /// <summary>
        /// ��ȡRelease lua�����ļ�Ŀ¼
        /// </summary>
        /// <returns></returns>
        public static string GetReleaseLuaPath()
        {
            return Path.Combine(GetReleasePath(), Utils.Lua);
        }

        /// <summary>
        /// ��ȡָ��Release lua�����ļ�
        /// </summary>
        /// <param name="filePathInLua">luaĿ¼�µ��ļ�·��</param>
        /// <returns></returns>
        public static string GetReleaseLuaPath(string filePathInLua)
        {
            return Path.Combine(GetReleaseLuaPath(), filePathInLua);
        }

        /// <summary>
        /// ��ȡEditor lua�����ļ�Ŀ¼
        /// </summary>
        /// <returns></returns>
        public static string GetProjectLuaPath()
        {
            return Path.Combine(Application.dataPath, Utils.Resources, Utils.Hotfix, Utils.Lua);
        }

        /// <summary>
        /// ��ȡ���������µ�ַ
        /// </summary>
        /// <returns></returns>
        public static string GetUpdateAddress()
        {
            return "http://119.91.49.126/Release";
        }

        /// <summary>
        /// д��
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        public static void WriteFile(string content, string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            CreateDirectory(directoryPath);
            if (!File.Exists(path)) 
            {
                var file = File.Create(path);
                file.Dispose();
            }

            File.WriteAllText(path, content);
        }

        /// <summary>
        /// д��
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        public static void WriteFile(byte[] content, string path)
        {
            string directoryPath = Path.GetDirectoryName(path);
            CreateDirectory(directoryPath);
            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Dispose();
            }

            File.WriteAllBytes(path, content);
        }


        private static void CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath) || Directory.Exists(directoryPath))
            {
                return;
            }

            string parentDirectoryPath = Directory.GetParent(directoryPath).FullName;
            CreateDirectory(parentDirectoryPath);
            Directory.CreateDirectory(directoryPath);
        }

        public static string GetSize(long size)
        {
            if (size < 1024)
            {
                return $"{size.ToString()}B";
            }

            float result = size / 1024.0f;
            if (result < 1024)
            {
                return $"{result.ToString("F2")}KB";
            }

            result /= 1024.0f;
            if (result < 1024)
            {
                return $"{result.ToString("F2")}MB";
            }

            result /= 1024.0f;
            return $"{result.ToString("F2")}GB";
        }

        public static void CopyDirectory(string sourceDir, string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                CreateDirectory(targetDir);
            }

            //�����ļ�
            string[] files = Directory.GetFiles(sourceDir);
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileName(files[i]);
                string sourceFile = files[i];
                string targetFile = Path.Combine(targetDir, fileName);

                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                File.Copy(sourceFile, targetFile);
            }

            //�����ļ���
            string[] dirs = Directory.GetDirectories(sourceDir);
            for (int i = 0; i < dirs.Length; i++)
            {
                string sourceDir2 = dirs[i];
                string targetDir2 = Path.Combine(targetDir, Path.GetFileName(dirs[i]));
                CopyDirectory(sourceDir2, targetDir2);
            }
        }

        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                if (files == null || files.Length == 0) return;

                for (int i = 0; i < files.Length; i++)
                {
                    File.Delete(files[i]);
                }

                string[] dirs = Directory.GetDirectories(path);
                for (int i = 0; i < dirs.Length; i++)
                {
                    DeleteDirectory(dirs[i]);
                }
                Directory.Delete(path);
            }
        }

        public static void CopyFile(string sourceFile, string targetFile)
        {
            if (!File.Exists(targetFile))
            {
                string targetDir = Path.GetDirectoryName(targetFile);
                if (!Directory.Exists(targetDir))
                {
                    CreateDirectory(targetDir);
                }
            }
            File.Copy(sourceFile, targetFile);
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
