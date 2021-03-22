using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
	public static void Load(Action<Hashtable> OnLoaded) // capitalized
	{
		Hashtable mockPlayerData = new Hashtable();
		mockPlayerData[HashConstants.PD_USER_ID] = 1;
		mockPlayerData[HashConstants.PD_NAME] = "Player 1";
		mockPlayerData[HashConstants.PD_COINS] = 50;

		OnLoaded?.Invoke(mockPlayerData); // added ? to ensure event is not firing null
	}
}