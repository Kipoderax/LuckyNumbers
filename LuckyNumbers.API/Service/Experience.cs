using System;

namespace LuckyNumbers.API.Service
{

    public enum ExperiencePoints
    {
        ONEGOAL = 3,
        TWOGOALS = 8,
        THREEGOALS = 57,
        FOURGOALS = 986,
        FIVEGOALS = 21542,
        SIXGOALS = 398381
    }

    public class Experience
    {
        public int currentLevel(int experience)
        {

            return experience == 0 ? 1 : (int)(2 * Math.Pow(experience, 0.4));
        }
    }
}