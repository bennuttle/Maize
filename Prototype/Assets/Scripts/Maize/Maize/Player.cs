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
        public int[] mazesAttemptedByDifficulty { get; set; }
        public int[] mazesCompletedByDifficulty { get; set; }

        public double averageWastedSteps { get; set; }

        [NonSerialized()]
        public CurrentMaze myMaze { get; set; }

        public Player()
        {
            stepsTaken = 0;
            mazesAttempted = 0;
            mazesCompleted = 0;
            mostDifficultLevel = 1;
            totalMazeTime = 0;
            mazesCompletedByDifficulty = new int[20];
            mazesAttemptedByDifficulty = new int[20];

            averageWastedSteps = 0;

            for (int index = 0; index < mazesCompletedByDifficulty.Length; index++)
            {
                mazesCompletedByDifficulty[index] = 0;
                mazesAttemptedByDifficulty[index] = 0;
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

        public void finishMaze(boolean reachedGoal)
        {
            stepsTaken += myMaze.currentStepsTaken;
            mazesAttempted++;
            mazesAttemptedByDifficulty[myMaze.currentDifficulty]++;
            totalMazeTime += myMaze.currentMazeTime;

            if (reachedGoal)
            {
                mazesCompleted++;
                mazesCompletedByDifficulty[myMaze.currentDifficulty]++;
                //was the maze we just completed the hardest we've attempted? If so, update the value accordingly.
                mostDifficultLevel = myMaze.currentDifficulty > mostDifficultLevel ? myMaze.currentDifficulty : mostDifficultLevel;

                //Weight the average to account for prior mazes
                averageWastedSteps = (((mazesCompleted - 1) * averageWastedSteps) +

                    //for this maze, compute the wasted steps
                    (myMaze.currentStepsTaken - myMaze.currentOptimalPathLength)) /

                    //divide out by the number of mazes we've attempted.
                    mazesCompleted;
            }

        }

        public MazeGraph freePlayMaze()
        {
            double[] completionPercentByDifficulty = new double[mazesAttemptedByDifficulty.Length];
            int completions, attempts;

            for (int index = 0; index < completionPercentByDifficulty; index++)
            {
                completions = Math.Max(1, mazesCompletedByDifficulty[index]);
                attempts = Math.Max(1, mazesAttemptedByDifficulty[index]);

                completionPercentByDifficulty[index] = ((1.0)*completions) / attempts;
            }

            int difficulty, harderIndex, easierIndex;
            double chance;

            difficulty = myMaze != null ? myMaze.currentDifficulty : mostDifficultLevel;

            //loop until a maze is selected
            while (true)
            {
                if (harderIndex > completionPercentByDifficulty.Length - 1)
                {
                    harderIndex = difficulty;
                }

                if (easierIndex < 1)
                {
                    easierIndex = difficulty;
                }

                chance = Random.NextDouble();
                if (chance < completionPercentByDifficulty[difficulty])
                {
                    return new MazeGraph(difficulty);
                }

                if (chance < completionPercentByDifficulty[harderIndex++])
                {
                    return new MazeGraph(harderIndex);
                }

                if (chance < completionPercentByDifficulty[easierIndex--])
                {
                    return new MazeGraph(easierIndex);
                }

            }

        }

        public class CurrentMaze
        {
            public int currentStepsTaken { get; set; }
            public int currentOptimalPathLength { get; set; }
            public int currentDifficulty { get; set; }
            public double currentMazeTime { get; set; }

            public CurrentMaze(MazeGraph theMaze)
            {
                currentStepsTaken = 0;
                currentOptimalPathLength = theMaze.getOptimalSolutionLength();
                currentDifficulty = theMaze.sizeX + theMaze.sizeY + theMaze.sizeZ;
                currentMazeTime = 0;
            }
        }
    }
}
