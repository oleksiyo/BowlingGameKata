using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameKata
{
    public interface IServices
    {
        bool IsValidated();
        int GetBonus();
        ScoreDetails GetScoreDetails();
    }
}
