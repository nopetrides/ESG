using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
	/// <summary>
	/// This used to "Load" the player, but really it just created a new player
	/// </summary>
	/// <param name="onLoaded"></param>
	public static void CreatePlayer(Action<Hashtable> onLoaded) // capitalized
	{
		Hashtable mockPlayerData = new Hashtable();
		mockPlayerData[HashConstants.PD_USER_ID] = PlayerPrefs.GetInt(HashConstants.PD_USER_ID, 0) +1;
		PlayerPrefs.SetInt(HashConstants.PD_USER_ID, (int)mockPlayerData[HashConstants.PD_USER_ID]);
		// we probably don't need this in the hash since we can always assign in if we know the user ID, but lets keep it in case we want to have name entry come form somewhere else
		mockPlayerData[HashConstants.PD_NAME] = "Player " + PlayerPrefs.GetInt(HashConstants.PD_USER_ID, 1); 
		// start player with more money so they get game over slower, we should probably expose this as data for a designer to be able to tweak
		mockPlayerData[HashConstants.PD_COINS] = 100;
		PlayerPrefs.SetInt(HashConstants.PD_COINS, 100);
		
		mockPlayerData[HashConstants.PD_MAX_MONEY] = 100;
		PlayerPrefs.SetInt(HashConstants.PD_MAX_MONEY, 100);
		mockPlayerData[HashConstants.PD_STREAK] = 0;
		PlayerPrefs.SetInt(HashConstants.PD_STREAK, 0);
		mockPlayerData[HashConstants.PD_BEST_STREAK] = 0;
		PlayerPrefs.SetInt(HashConstants.PD_BEST_STREAK, 0);
		
		onLoaded?.Invoke(mockPlayerData); // added ? to ensure event is not firing null
	}

	/// <summary>
	/// Loading the player state from previous sessions, right now it leverages the Unity PlayerPrefs, but we can replace these with anything.
	/// </summary>
	public static void LoadPlayer(Action<Hashtable> onLoaded)
	{
		Hashtable savedPlayerData = new Hashtable();
		savedPlayerData[HashConstants.PD_USER_ID] = PlayerPrefs.GetInt(HashConstants.PD_USER_ID, 1);
		savedPlayerData[HashConstants.PD_NAME] = "Player " + PlayerPrefs.GetInt(HashConstants.PD_USER_ID, 1); 
		savedPlayerData[HashConstants.PD_COINS] = PlayerPrefs.GetInt(HashConstants.PD_COINS, 100);
		savedPlayerData[HashConstants.PD_MAX_MONEY] = PlayerPrefs.GetInt(HashConstants.PD_MAX_MONEY, 100);
		savedPlayerData[HashConstants.PD_STREAK] = PlayerPrefs.GetInt(HashConstants.PD_STREAK, 0);
		savedPlayerData[HashConstants.PD_BEST_STREAK] = PlayerPrefs.GetInt(HashConstants.PD_BEST_STREAK, 0);
		onLoaded?.Invoke(savedPlayerData);
	}

	/// <summary>
	/// We only need to update the gameplay changing values for the player
	/// </summary>
	public static void SavePlayer(int money, int mostMoney, int streak, int bestStreak)
	{
		PlayerPrefs.SetInt(HashConstants.PD_COINS, money);
		PlayerPrefs.SetInt(HashConstants.PD_MAX_MONEY, mostMoney);
		PlayerPrefs.SetInt(HashConstants.PD_STREAK, streak);
		PlayerPrefs.SetInt(HashConstants.PD_BEST_STREAK, bestStreak);
	}
}