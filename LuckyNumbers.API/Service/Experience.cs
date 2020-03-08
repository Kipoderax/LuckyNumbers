using System;

namespace LuckyNumbers.API.Service
{

    public enum ExperiencePoints
    {
        ONEGOAL = 1,
        TWOGOALS = 3,
        THREEGOALS = 21,
        FOURGOALS = 186,
        FIVEGOALS = 1985,
        SIXGOALS = 15134
    }

    public class Experience
    {
        public int currentLevel(int experience)
        {

            return experience == 0 ? 1 : (int)(2 * Math.Pow(experience, 0.4));
        }

        public int needExpToNextLevel(int experience)
        {
            if (currentLevel(experience) == 1)
            {
                return 1;
            }
            return (int)(
                    (0.176777 * Math.Pow(currentLevel(experience) + 1, 2.5) + 1)
                            - (experience)
            );
        }

        public int needExpForAllLevel(int experience)
        {

            return (int)(
                    (0.176777 * Math.Pow(currentLevel(experience) + 1, 2.5)) -
                    (0.176777 * Math.Pow(currentLevel(experience), 2.5))
            );
        }
    }
}