using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameKata
{
    public class CommonServices : BaseServices
    {
        public CommonServices(List<int> listRolls, int frame)
            : base(listRolls, frame) { }

        public override bool IsValidated()
        {
            return listRolls[frame] != 10 && listRolls[frame] + listRolls[frame + 1] != 10;
        }

        public override int GetBonus()
        {
            return 0;
        }

        public override ScoreDetails GetScoreDetails()
        {
            var score = listRolls[frame + 1] + listRolls[frame];
            frame += 2;
            return new ScoreDetails(score, frame);
        }
    }
}