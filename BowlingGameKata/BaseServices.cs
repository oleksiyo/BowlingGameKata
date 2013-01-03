using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameKata
{
    public abstract class BaseServices : IServices
    {
        public int frame = 0;
        protected List<int> listRolls { get; private set; }

        protected BaseServices(List<int> listRolls, int frame)
        {
            this.listRolls = listRolls;
            this.frame = frame;
        }

        public abstract bool IsValidated();

        public abstract int GetBonus();

        public abstract ScoreDetails GetScoreDetails();
    }
}
