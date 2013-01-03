using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace BowlingGameKata
{
    public class BowlingGame
    {
        private const int framesNumber = 10;
        private const int strikeNumber = 10;
        private const int spareNumber = 10;
        readonly List<int> listRolls = new List<int>();

        public void Roll(int pins)
        {
            listRolls.Add(pins);
        }

        public int Score()
        {
            var score = 0;
            var frame = 0;
            for (var i = 0; i < framesNumber; i++)
            {
                if (IsSpare(frame))
                {
                    score += spareNumber + SpareBonus(frame);
                    frame += 2;
                }
                else if (IsStrike(frame))
                {
                    score += StrikeBonus(frame);
                    frame++;
                }
                else
                {
                    score += SumOfBallsInFrame(frame);
                    frame += 2;
                }
            }
            return score;
        }

        public int Score2()
        {
            var score = 0;
            var frame = 0;
            ScoreDetails scoreDetails = new ScoreDetails(0, 0);

            for (var i = 0; i < framesNumber; i++)
            {
                List<IServices> listService = new List<IServices>
                                                  {
                                                      new SpareServices(listRolls, scoreDetails.frame),
                                                      new StrikeServices(listRolls, scoreDetails.frame),
                                                      new CommonServices(listRolls, scoreDetails.frame)
                                                  };
                listService.ForEach(x =>
                                        {
                                            if (x.IsValidated())
                                            {
                                                var scoreD = x.GetScoreDetails();
                                                scoreDetails.score += scoreD.score;
                                                scoreDetails.frame += scoreD.frame;
                                            }
                                        });
            }
            return scoreDetails.score;
        }

        bool IsStrike(int frame)
        {
            return listRolls[frame] == strikeNumber;
        }

        bool IsSpare(int frame)
        {
            return listRolls[frame] + listRolls[frame + 1] == spareNumber;
        }

        int SumOfBallsInFrame(int frame)
        {
            return listRolls[frame + 1] + listRolls[frame];
        }
        int SpareBonus(int frame)
        {
            return listRolls[frame + 2];
        }

        int StrikeBonus(int frame)
        {
            return strikeNumber + listRolls[frame + 1] + listRolls[frame + 2];
        }
    }

    public class BowlingGameTest
    {
        private readonly BowlingGame bowlingGame;

        public BowlingGameTest()
        {
            bowlingGame = new BowlingGame();
        }


        [Fact]
        public void should_be_score_7_if_pins_1_and_6()
        {
            bowlingGame.Roll(1);
            bowlingGame.Roll(6);
            EmptyHits(18);
            var score = bowlingGame.Score();
            score.Should().Be(7);

            var score2 = bowlingGame.Score2();
            score2.Should().Be(7);
        }

        [Fact]
        public void should_be_score_17_when_frame_2_and_pins_4_5_6_2()
        {
            bowlingGame.Roll(4);
            bowlingGame.Roll(5);
            bowlingGame.Roll(6);
            bowlingGame.Roll(2);
            EmptyHits(16);
            var score = bowlingGame.Score();
            score.Should().Be(17);


            var score2 = bowlingGame.Score2();
            score2.Should().Be(17);
        }

        [Fact]
        public void should_be_score_29_when_frame_3_with_spare()
        {
            bowlingGame.Roll(1);
            bowlingGame.Roll(4);
            bowlingGame.Roll(4);
            bowlingGame.Roll(5);

            SpareHite();

            bowlingGame.Roll(5);
            EmptyHits(13);
            var score = bowlingGame.Score();
            score.Should().Be(34);

            var score2 = bowlingGame.Score2();
            score2.Should().Be(34);
        }

        [Fact]
        public void should_be_18_when_spare()
        {
            SpareHite();

            bowlingGame.Roll(4);

            EmptyHits(17);

            var score = bowlingGame.Score();
            score.Should().Be(18);

            var score2 = bowlingGame.Score2();
            score2.Should().Be(18);
        }

        [Fact]
        public void should_be_26_when_strake()
        {
            StrakeHite();

            bowlingGame.Roll(4);
            bowlingGame.Roll(4);

            EmptyHits(16);

            var score = bowlingGame.Score();
            score.Should().Be(26);

            var score2 = bowlingGame.Score2();
            score2.Should().Be(26);
        }


        private void EmptyHits(int count)
        {
            for (var i = 0; i < count; i++)
            {
                bowlingGame.Roll(0);
            }
        }

        private void SpareHite()
        {
            bowlingGame.Roll(6);
            bowlingGame.Roll(4);
        }

        private void StrakeHite()
        {
            bowlingGame.Roll(10);
        }

    }
}
