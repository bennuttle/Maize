using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

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
		
		[NonSerialized]
		private CurrentMaze myMaze;
		
		
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
		
		//Application.PersistentDataPath + "fileName.xml"
		public void savePlayer(String saveFilePath)
		{
			SaveLoad.Serialize<Player>(this, saveFilePath);
		}
		
		//Application.PersistentDataPath + "fileName.xml"
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
		
		private static double convertTimetoMS(int minutes, int seconds, int milliseconds)
		{
			return milliseconds + seconds * 1000 + minutes * 60000;
		}
		
		private static int convertMStoMinutes(double milliseconds)
		{
			return (int)(milliseconds / 60000);
		}
		
		private static int convertMStoRemainingSeconds(double milliseconds)
		{
			return (int)((milliseconds / 1000) % 60);
		}
		
		private static int convertMStoRemainingMS(double milliseconds)
		{
			return(int)(milliseconds % 1000);
		}
		
		public int getPlayTimeMinutes()
		{
			return Player.convertMStoMinutes(totalMazeTime);
		}
		
		public int getPlayTimeSeconds()
		{
			return Player.convertMStoRemainingSeconds(totalMazeTime);
		}
		
		public int getPlayTimeMilliseconds()
		{
			return Player.convertMStoRemainingMS(totalMazeTime);
		}
		
		public void finishMaze(bool reachedGoal, int minutes, int seconds, int milliseconds)
		{
			stepsTaken += myMaze.currentStepsTaken;
			mazesAttempted++;
			mazesAttemptedByDifficulty[myMaze.currentDifficulty]++;
			totalMazeTime += convertTimetoMS(minutes, seconds, milliseconds);
			
			
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
		
		public void startMaze(MazeGraph theMaze)
		{
			myMaze = new CurrentMaze(theMaze);
		}
		
		public MazeGraph freePlayMaze()
		{
			double[] completionPercentByDifficulty = new double[mazesAttemptedByDifficulty.Length];
			int completions, attempts;
			Random rand = new Random();
			
			for (int index = 0; index < completionPercentByDifficulty.Length; index++)
			{
				completions = Math.Max(1, mazesCompletedByDifficulty[index]);
				attempts = Math.Max(1, mazesAttemptedByDifficulty[index]);
				
				completionPercentByDifficulty[index] = ((1.0)*completions) / attempts;
			}
			
			int difficulty, harderIndex, easierIndex;
			harderIndex = completionPercentByDifficulty.Length - 1;
			easierIndex = 0;
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
				
				chance = rand.NextDouble();
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
