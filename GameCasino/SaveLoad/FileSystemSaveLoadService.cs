using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.SaveLoad
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private readonly string _savePath;

        public FileSystemSaveLoadService(string path)
        {
            _savePath = path;
            Directory.CreateDirectory(path);
        }

        public T LoadData<T>(string id)
        {
            var fullPath = Path.Combine(_savePath, $"{id}.txt");
            if (!File.Exists(fullPath)) return default(T);

            using (var fs = File.OpenRead(fullPath))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
        }

        public void SaveData<T>(T data, string id)
        {
            var fullPath = Path.Combine(_savePath, $"{id}.txt");
            using (var fs = File.Create(fullPath))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }
        }
    }
}
