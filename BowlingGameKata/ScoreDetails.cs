using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameKata
{
    public class ScoreDetails
    {
        public int score { get; set; }
        public int frame { get; set; }

        public ScoreDetails(int score, int frame)
        {
            this.score = score;
            this.frame = frame;
        }
    }
}
