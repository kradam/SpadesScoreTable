using System;
public enum GameState { Planning, Scoring }
public class PlayerTrick
{
    public int? Plan { get; set; }
    public int? Gain { get; set; }
}
public class Game
{
    //public Game()
    //{
    //    CalculateScore();
    //}
    public int Number { get; set; }
    public int Dealer { get; set; }
   
    public PlayerTrick[] PlayersTricks { get; set; }
    public PairScore[] PairScores { get; set; }
    public GameState State { get; set; }
    public bool PlanReadOnly { get { return true; } }
    public bool PlanningDisabled { get { return (State == GameState.Scoring); } set {; } }
    public string PlanningDisabledString = "disabled";
    public bool ScoringDisabled { get { return (State == GameState.Planning); } set {; } }
    //public bool ScoringDisabled = true; // { get { return true; } set { } }
    public int SumPlan() => 
        (PlayersTricks[0].Plan ?? 0) + (PlayersTricks[1].Plan ?? 0) + (PlayersTricks[2].Plan ?? 0) + (PlayersTricks[3].Plan ?? 0);
    public int SumGain() =>
        (PlayersTricks[0].Gain ?? 0) + (PlayersTricks[1].Gain ?? 0) + (PlayersTricks[2].Gain ?? 0) + (PlayersTricks[3].Gain ?? 0);
    public int PairScore(int pair) => PairScores[pair].score;
    public int PairOvertrick(int pair) => PairScores[pair].overtrick;
    public void CalculateScore(Game previousGame)
    {
        CalculatePairScore(pair: 0, previousGame);
        CalculatePairScore(pair: 1, previousGame);
    }
    public bool CalculatePairScore(int pair, Game previousGame)
    {
        if (previousGame == null)
        {
            PairScores[pair].score = PairScores[pair].overtrick = 0;
        } else
        {
            PairScores[pair].score = previousGame.PairScores[pair].score;
            PairScores[pair].overtrick = previousGame.PairScores[pair].overtrick;
        }

        int player1Idx = pair, player2Idx = pair + 2;
        if (PlayersTricks[player1Idx].Plan == null || PlayersTricks[player1Idx].Gain == null ||
            PlayersTricks[player2Idx].Plan == null || PlayersTricks[player2Idx].Gain == null)
        {
            return false;
        }
        //if (PlayersTricks[player1Idx].Gain + )
            
        foreach (int playerIdx in new int[] { player1Idx, player2Idx } )
            if (PlayersTricks[playerIdx].Plan == 0)
            {
                PairScores[pair].score += (PlayersTricks[playerIdx].Gain == 0) ? +100 : -100;
                //Console.WriteLine(playerIdx);
            }
        int pairPlan = (PlayersTricks[player1Idx].Plan ?? 0) + (PlayersTricks[player2Idx].Plan ?? 0);
        int pairGain = (PlayersTricks[player1Idx].Gain ?? 0) + (PlayersTricks[player2Idx].Gain ?? 0);
        if (pairGain >= pairPlan)
        {
            PairScores[pair].score += pairPlan * 10;
            PairScores[pair].overtrick += pairGain - pairPlan;
            if (PairScores[pair].overtrick > 9)
            {
                PairScores[pair].score -= 100;
                PairScores[pair].overtrick -= 10;
            }
        } else
        {
            PairScores[pair].score -= pairPlan * 10;
        }
        return true;
    }
    public void ToggleState ()
    {
        if (State == GameState.Planning)
            State = GameState.Scoring;
        else
            State = GameState.Planning;
    }
}
public class PairScore
{
    public int score;
    public int overtrick;
}

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
}
