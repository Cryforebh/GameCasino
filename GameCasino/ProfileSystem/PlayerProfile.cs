using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCasino.ProfileSystem
{
    [Serializable]
    public class PlayerProfile
    {
        public string Name { get; set; }
        public int Balance { get; set; } = 1000;

        [NonSerialized]
        public DateTime LastLogin;
    }
}
