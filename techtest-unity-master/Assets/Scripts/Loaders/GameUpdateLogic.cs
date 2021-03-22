using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// renamed
public class GameUpdateLogic
{
	public static void Load(ThrowableScriptable playerChoice, Action<Dictionary<string,object>> onLoaded) // capitalized
	{
		// replace with a basic AI
		//UseableItem opponentHand = (UseableItem)Enum.GetValues(typeof(UseableItem)).GetValue(UnityEngine.Random.Range(1, 4)); // bugfix: change range 0,4 to 1,4 
		ThrowableScriptable opponentHand = null;
		
		Dictionary<string, object> mockGameUpdate = new Dictionary<string,object>();
		mockGameUpdate[HashConstants.GUD_PLAYER_RESULT] = playerChoice;
		mockGameUpdate[HashConstants.GUD_OPPONENT_RESULT] = opponentHand;
		mockGameUpdate[HashConstants.GUD_MONEY_CHANGE] = GetCoinsAmount(playerChoice, opponentHand);
		
		onLoaded?.Invoke(mockGameUpdate); // ? to avoid null refs
	}

	// Its weird that GetCoinsAmount is in the UpdateGameLoader
	private static int GetCoinsAmount (ThrowableScriptable playerHand, ThrowableScriptable opponentHand)
	{
		Result drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);

		if (drawResult.Equals (Result.Won))
		{
			return 10;
		}
		else if (drawResult.Equals (Result.Lost))
		{
			return -10;
		}

		return 0; // removed unnecessary else
	}
}