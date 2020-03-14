using System;

namespace LuckyNumbers.API.Service
{

    public enum LottoExperiencePoints
    {
        ONEGOAL = 1,
        TWOGOALS = 3,
        THREEGOALS = 57,
        FOURGOALS = 1032,
        FIVEGOALS = 54021,
        SIXGOALS = 2797362
    }

    public class Experience
    {
        public int currentLevel(int experience)
        {

            return experience == 0 ? 1 : (int)(2 * Math.Pow(experience, 0.333333));
        }
    }
}