using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Summary description for Class1
/// </summary>
public class Match
{
	public List<Game> games = new List<Game>();
	//public Match(int dealer = 1)
	//{
	//	games.Add(new Game(1, dealer));	
	//}

    public void NewGame (int dealer = 0) 
	{		
		if (games.Count > 0)
        {
			games.Last().State = GameState.Done;
        }

		games.Add(new Game(games.Count + 1, dealer: games.Count == 0 ? dealer : games.Last().Dealer+1 % 4));
		bool test = false; // TODO read test  from URL params
		if (test)
        {
			games.Last().PlayersTricks[0].Plan = 1;
			games.Last().PlayersTricks[0].Gain = 3;
			games.Last().PlayersTricks[1].Plan = 4;
			games.Last().PlayersTricks[1].Gain = 5;
			games.Last().PlayersTricks[2].Plan = 3;
			games.Last().PlayersTricks[2].Gain = 3;
			games.Last().PlayersTricks[3].Plan = 1;
			games.Last().PlayersTricks[3].Gain = 2;
			games.Last().CalculateScore();
		}
        games.Last().CalculateScore();
    }
	public void RemoveLastGame()
    {
		games.Remove(games.Last());
	}

}
