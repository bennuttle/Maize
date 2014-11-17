using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maize
{
    [Serializable]
    public class Player
    {
        public int stepsTaken { get; set; }
        public int mazesAttempted { get; set; }
        public int mazesCompleted { get; set; }
        public int mostDifficultLevel { get; set; }
        public double totalMazeTime { get; set; }
        public int[] mazesCompletedByDifficulty { get; set; }

        public Player()
        {
            stepsTaken = 0;
            mazesAttempted = 0;
            mazesCompleted = 0;
            mostDifficultLevel = 1;
            totalMazeTime = 0;
            mazesCompletedByDifficulty = new int[20];
            for (int index = 0; index < mazesCompletedByDifficulty.Length; index++)
            {
                mazesCompletedByDifficulty[index] = 0;
            }
        }

        public void savePlayer(String saveFilePath)
        {
            SaveLoad.Serialize<Player>(this, saveFilePath);
        }

        public static Player loadPlayer(String savedFilePath)
        {
            try
            {
                return SaveLoad.Deserialize<Player>(savedFilePath);
            }
            catch
            {
                return new Player();
            }
        }
    }
}
