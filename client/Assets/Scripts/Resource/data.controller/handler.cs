using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;

namespace game.resource
{
    class DataController
    {
        private UnityWebRequest updateDownloadWebRequest;
        private int updateDownloadElementCount = 0;
        private int updateDownloadElementIndex = 0;
        private bool updateIsCompleted = false;

        private string ValidPathSeparator(string _originPath)
        {
            return _originPath.Replace("\\", "/");
        }

        private Dictionary<string, long> GetAllFilesInRootDirectory(string _rootDirectoryPath)
        {
            Dictionary<string, long> result = new();
            DirectoryInfo rootDirectoryInfo = new(_rootDirectoryPath);

            if (!rootDirectoryInfo.Exists)
            {
                return result;
            }

            foreach (FileInfo file in rootDirectoryInfo.GetFiles())
            {
                result[ValidPathSeparator(Path.Combine(_rootDirectoryPath, file.FullName))] = file.Length;
            }

            foreach (DirectoryInfo directory in rootDirectoryInfo.GetDirectories())
            {
                foreach (var newElement in GetAllFilesInRootDirectory(Path.Combine(_rootDirectoryPath, directory.FullName)))
                {
                    result[newElement.Key] = newElement.Value;
                }
            }

            return result;
        }

        public IEnumerator Fetch()
        {
            string dataHostingController = dataController.Config.GetHostingControlationAddress();
            string localStoragePath = dataController.Config.GetLocalStogareFullPath();

            UnityWebRequest controllerModel = new(dataHostingController);
            controllerModel.downloadHandler = new DownloadHandlerBuffer();
            yield return controllerModel.SendWebRequest();

            if (controllerModel.result != UnityWebRequest.Result.Success)
            {
                yield break;
            }

            dataController.Model dataControllerModel = dataController.Model.FromJson(controllerModel.downloadHandler.text);
            if (dataControllerModel.FileList.Count <= 0)
            {
                yield break;
            }

            Dictionary<string, long> localFiles = GetAllFilesInRootDirectory(localStoragePath);
            List<string> updateDownloadList = new();

            foreach (var dataControllerElement in dataControllerModel.FileList)
            {
                string elementPath = ValidPathSeparator(Path.Combine(localStoragePath, dataControllerElement.path));

                if (localFiles.ContainsKey(elementPath))
                {
                    if (localFiles[elementPath] != dataControllerElement.size)
                    {
                        updateDownloadList.Add(dataControllerElement.path);
                    }

                    localFiles.Remove(elementPath);
                }
                else
                {
                    updateDownloadList.Add(dataControllerElement.path);
                }
            }

            foreach (KeyValuePair<string, long> localFile in localFiles)
            {
                (new FileInfo(localFile.Key)).Delete();

                string removeFolderPath = Path.GetDirectoryName(localFile.Key);
                DirectoryInfo removeFolderInfo = new(removeFolderPath);

                while (removeFolderInfo.GetFiles().Length <= 0)
                {
                    removeFolderInfo.Delete();
                    removeFolderPath = Path.GetDirectoryName(removeFolderPath);
                    removeFolderInfo = new(removeFolderPath);
                }
            }

            this.updateDownloadElementIndex = 0;
            this.updateDownloadElementCount = updateDownloadList.Count;

            foreach (string updateDownload in updateDownloadList)
            {
                //UnityEngine.Debug.Log("biln.data.controller >> downloading: " + updateDownload);
                this.updateDownloadWebRequest = new(ValidPathSeparator(Path.Combine(dataHostingController, updateDownload)), UnityWebRequest.kHttpVerbGET);

                string saveToPath = ValidPathSeparator(Path.Combine(localStoragePath, updateDownload));
                this.updateDownloadWebRequest.downloadHandler = new DownloadHandlerFile(saveToPath);

                yield return this.updateDownloadWebRequest.SendWebRequest();
                this.updateDownloadElementIndex++;
            }

            this.updateIsCompleted = true;
        }

        public float GetCurentProgress()
        {
            if (this.updateDownloadWebRequest == null)
            {
                return 0.0f;
            }

            return this.updateDownloadWebRequest.downloadProgress;
        }

        public float GetTotalProgress()
        {
            if (this.updateDownloadElementIndex <= 0
                || this.updateDownloadElementCount <= 0)
            {
                return 0;
            }

            return ((float)this.updateDownloadElementIndex / (float)this.updateDownloadElementCount);
        }

        public bool IsCompleted()
        {
            return this.updateIsCompleted;
        }
    }
}
