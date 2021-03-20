using UnityEngine;
using System.Collections;
using System;

public class Player
{
	private int _userId;
	private string _name;
	private int _coins;

	public Player(Hashtable playerData)
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
	}
}