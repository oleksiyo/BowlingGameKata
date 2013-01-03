using System.Collections.Generic;

namespace BowlingGameKata
{
    public class SpareServices : BaseServices
    {
        public SpareServices(List<int> listRolls, int frame)
            : base(listRolls, frame) { }

        public override int GetBonus()
        {
            return listRolls[frame + 2];
        }

        public override bool IsValidated()
        {
            return listRolls[frame] + listRolls[frame + 1] == 10;
        }

        public override ScoreDetails GetScoreDetails()
        {
            int sum = 10 + GetBonus();
            frame += 2;
            return new ScoreDetails(sum, frame);
        }
    }
}
