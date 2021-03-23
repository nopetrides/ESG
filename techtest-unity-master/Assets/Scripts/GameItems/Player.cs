using UnityEngine;
using System.Collections;
using System;

public class Player
{
	private int _userId;
	public int UserId => _userId;
	private string _name;
	public string Name => _name;
	private int _coins;
	public int Coins =>_coins;
	private int _mostCoins;
	public int MostCoins => _mostCoins;
	private int _streak;
	public int Streak => _streak;
	private int _bestStreak;
	public int BestStreak => _bestStreak;

	public event Action OnCoinsChanged;
	
	public Player(Hashtable playerData)
	{
		SetValues(playerData);
	}

	/// <summary>
	/// Reusing the player object instead of destroying and instantiating a new one is more efficient
	/// </summary>
	/// <param name="playerData"></param>
	public void Update(Hashtable playerData)
	{
		SetValues(playerData);
	}

	private void SetValues(Hashtable playerData)
	{
		_userId = (int)playerData[HashConstants.PD_USER_ID];
		_name = playerData[HashConstants.PD_NAME].ToString();
		_coins = (int)playerData[HashConstants.PD_COINS];
		
		_mostCoins = (int)playerData[HashConstants.PD_MAX_MONEY];
		_streak = (int)playerData[HashConstants.PD_STREAK];
		_bestStreak = (int)playerData[HashConstants.PD_BEST_STREAK];
	}
	
	public void ChangeCoinAmount(int amount)
	{
		_coins += amount;
		_mostCoins = _coins > _mostCoins ? _coins : _mostCoins;
		OnCoinsChanged?.Invoke();
	}

	public void SetStreak(int streak)
	{
		_streak = streak;
		_bestStreak = _streak > _bestStreak ? _streak: _bestStreak;
	}
}