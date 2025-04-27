using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.SaveLoad
{
    public interface ISaveLoadService<T>
    {
        void SaveData<T>(T data, string id);
        T LoadData<T>(string id);
    }
}
