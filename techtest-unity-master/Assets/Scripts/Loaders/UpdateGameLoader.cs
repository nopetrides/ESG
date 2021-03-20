using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(Hashtable gameUpdateData);
	public event OnLoadedAction OnLoaded;

	private UseableItem _choice;

	public UpdateGameLoader(UseableItem playerChoice)
	{
		_choice = playerChoice;
	}

	public void Load() // capitalized
	{
		UseableItem opponentHand = (UseableItem)Enum.GetValues(typeof(UseableItem)).GetValue(UnityEngine.Random.Range(1, 4)); // bugfix: change range 0,4 to 1,4 

		Hashtable mockGameUpdate = new Hashtable();
		mockGameUpdate[HashConstants.GUD_PLAYER_RESULT] = _choice;
		mockGameUpdate[HashConstants.GUD_OPPONENT_RESULT] = opponentHand;
		mockGameUpdate[HashConstants.GUD_MONEY_CHANGE] = GetCoinsAmount(_choice, opponentHand);
		
		OnLoaded(mockGameUpdate);
	}

	// Its weird that GetCoinsAmount is in the UpdateGameLoader
	private int GetCoinsAmount (UseableItem playerHand, UseableItem opponentHand)
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