using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiskGeniusBypass
{
    public class LicenseFileHandler
    {
        private string[] filePaths;
        private StorageMethod storageMethod;
        private List<MemStoreItem> memstore;
        private List<FileStoreItem> filestore;
        private string tempFolder;
        public enum StorageMethod
        {
            Memory = 0,
            MoveFiles = 1,
        }

        public LicenseFileHandler(StorageMethod storageMethod, string[] inputFilePaths)
        {
            Console.WriteLine(String.Join(Environment.NewLine, inputFilePaths));

            this.storageMethod = storageMethod;
            this.filePaths = inputFilePaths;
            if (storageMethod == StorageMethod.Memory)
            {
                memstore = new List<MemStoreItem>();
                for (int i = 0; i < inputFilePaths.Length; i++)
                {
                    memstore.Add(new MemStoreItem(Path.GetFileName(inputFilePaths[i]), inputFilePaths[i]));
                }
            } else if (storageMethod == StorageMethod.MoveFiles)
            {
                filestore = new List<FileStoreItem>();
                tempFolder = "temp" + Guid.NewGuid().ToString().Split(char.Parse("-"))[0];
                for (int i = 0; i < inputFilePaths.Length; i++)
                {
                    filestore.Add(new FileStoreItem(Path.GetFileName(inputFilePaths[i]), inputFilePaths[i]));
                }
            }
        }

        public void Store()
        {
            if (storageMethod == StorageMethod.Memory)
            {
                for (int i = 0; i < memstore.Count; i++)
                {
                    memstore[i].Data = File.ReadAllBytes(memstore[i].Filepath);
                    File.Delete(memstore[i].Filepath);
                }
            } else if (storageMethod == StorageMethod.MoveFiles)
            {
                Console.WriteLine("move files method");
                if (!File.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                Console.WriteLine(filePaths.Length);
                for (int i = 0; i < filePaths.Length; i++)
                {
                    Console.WriteLine("Storing file {0}", filestore[i].Filename);
                    string newPath = Path.Combine(tempFolder, filestore[i].Filename);
                    File.Move(filestore[i].OriginalFilepath, newPath);
                    filestore[i].CurrentFilepath = newPath;
                }
            }
        }

        public void Restore()
        {
            if (storageMethod == StorageMethod.Memory)
            {
                for (int i = 0; i < memstore.Count; i++)
                {
                    File.WriteAllBytes(memstore[i].Filepath, memstore[i].Data);
                }
            }
            else if (storageMethod == StorageMethod.MoveFiles)
            {
                for (int i = 0; i < filestore.Count; i++)
                {
                    Console.WriteLine("Restoring file {0}", filestore[i].Filename);
                    Console.WriteLine(filestore[i].CurrentFilepath);
                    File.Move(filestore[i].CurrentFilepath, filestore[i].OriginalFilepath);
                    filestore[i].CurrentFilepath = filestore[i].OriginalFilepath;
                }
                Directory.Delete(tempFolder, true);
            }
        }

        private class MemStoreItem
        {
            public string Filename;
            public string Filepath;
            public byte[] Data;

            public MemStoreItem(string name, string path)
            {
                Filename = name;
                Filepath = path;
                Data = File.ReadAllBytes(Filepath);
            }
        }

        private class FileStoreItem
        {
            public string Filename;
            public string OriginalFilepath;
            public string CurrentFilepath;

            public FileStoreItem(string name, string path)
            {
                Filename = name;
                OriginalFilepath = CurrentFilepath = path;
            }

        }
    }

}
