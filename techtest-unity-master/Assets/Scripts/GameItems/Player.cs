using UnityEngine;
using System.Collections;
using System;

public class Player
{
	private int _userId;
	private string _name;
	private int _coins;

	public event Action OnCoinsChanged;
	
	public Player(Hashtable playerData)
	{
		_userId = (int)playerData[HashConstants.PD_USER_ID];
		_name = playerData[HashConstants.PD_NAME].ToString();
		_coins = (int)playerData[HashConstants.PD_COINS];
	}

	/// <summary>
	/// Reusing the player object instead of destroying and instantiating a new one is more efficient
	/// </summary>
	/// <param name="playerData"></param>
	public void Update(Hashtable playerData)
	{
		_userId = (int)playerData[HashConstants.PD_USER_ID];
		_name = playerData[HashConstants.PD_NAME].ToString();
		_coins = (int)playerData[HashConstants.PD_COINS];
	}
	
	public int GetUserId()
	{
		return _userId;
	}
	
	public string GetName()
	{
		return _name;
	}

	public int GetCoins()
	{
		return _coins;
	}

	public void ChangeCoinAmount(int amount)
	{
		_coins += amount;
		OnCoinsChanged?.Invoke();
	}
}