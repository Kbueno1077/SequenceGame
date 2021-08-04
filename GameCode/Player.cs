using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCode
{
    [Serializable()]
    public class Player
    {

        public string Name { get; set; }
        public string Password { get; set; }
        public int LucesMaxScore { get; set; }


        public Player(string name, string password, int maxScore)
        {
            Name = name;
            Password = password;
            LucesMaxScore = maxScore;
        }

    }
}
