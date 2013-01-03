using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameKata
{
    public class StrikeServices : BaseServices
    {
        public StrikeServices(List<int> listRolls, int frame)
            : base(listRolls, frame) { }

        public override bool IsValidated()
        {
            return listRolls[frame] == 10;
        }

        public override int GetBonus()
        {
            return 10 + listRolls[frame + 1] + listRolls[frame + 2];
        }

        public override ScoreDetails GetScoreDetails()
        {
            int sum = GetBonus();
            frame++;
            return new ScoreDetails(sum, frame);
        }
    }
}
