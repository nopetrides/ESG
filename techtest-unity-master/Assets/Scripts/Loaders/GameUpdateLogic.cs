using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// renamed
public class GameUpdateLogic
{
	public static void UpdateGame(ThrowableScriptable playerChoice, ThrowableScriptable opponentHand, Action<Dictionary<string,object>, Result> onLoaded) // capitalized
	{
		Result drawResult = ResultAnalyzer.GetResultState(playerChoice, opponentHand);
		Dictionary<string, object> mockGameUpdate = new Dictionary<string,object>();
		mockGameUpdate[HashConstants.GUD_PLAYER_RESULT] = playerChoice;
		mockGameUpdate[HashConstants.GUD_OPPONENT_RESULT] = opponentHand;
		mockGameUpdate[HashConstants.GUD_MONEY_CHANGE] = GetCoinsAmount(drawResult);
		
		onLoaded?.Invoke(mockGameUpdate,drawResult); // ? to avoid null refs
	}

	// Its weird that GetCoinsAmount is in the UpdateGameLoader
	private static int GetCoinsAmount (Result result)
	{
		if (result.Equals (Result.Won))
		{
			return 10;
		}
		else if (result.Equals (Result.Lost))
		{
			return -10;
		}

		return 0; // removed unnecessary else
	}
}