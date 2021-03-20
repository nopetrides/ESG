using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	[SerializeField] private Text playerHand; // converted from public to SerializedField
	[SerializeField] private Text enemyHand;

	[SerializeField] private Text nameLabel; // these can also be serialized fields instead of being assigned on Awake
	[SerializeField] private Text moneyLabel;

	private Player _player;
	
	// removed Awake()
	
	void Start()
	{
		PlayerInfoLoader playerInfoLoader = new PlayerInfoLoader();
		playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
		playerInfoLoader.Load();
	}

	void Update()
	{
		UpdateHud();
	}

	public void OnPlayerInfoLoaded(Hashtable playerData)
	{
		_player = new Player(playerData);
	}

	public void UpdateHud()
	{
		nameLabel.text = "Name: " + _player.GetName();
		moneyLabel.text = "Money: $" + _player.GetCoins(); // ToString is uncessary
	}

	public void HandlePlayerInput(int item)
	{
		UseableItem playerChoice = UseableItem.None;

		switch (item)
		{
			case 1:
				playerChoice = UseableItem.Rock;
				break;
			case 2:
				playerChoice = UseableItem.Paper;
				break;
			case 3:
				playerChoice = UseableItem.Scissors;
				break;
		}

		UpdateGame(playerChoice);
	}

	private void UpdateGame(UseableItem playerChoice)
	{
		UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice);
		updateGameLoader.OnLoaded += OnGameUpdated;
		updateGameLoader.Load();
	}

	public void OnGameUpdated(Hashtable gameUpdateData)
	{
		playerHand.text = DisplayResultAsText((UseableItem)gameUpdateData[HashConstants.GUD_PLAYER_RESULT]);
		enemyHand.text = DisplayResultAsText((UseableItem)gameUpdateData[HashConstants.GUD_OPPONENT_RESULT]);

		_player.ChangeCoinAmount((int)gameUpdateData[HashConstants.GUD_MONEY_CHANGE]);
	}

	private string DisplayResultAsText (UseableItem result)
	{
		switch (result)
		{
			case UseableItem.Rock:
				return "Rock";
			case UseableItem.Paper:
				return "Paper";
			case UseableItem.Scissors:
				return "Scissors";
		}

		return "Nothing";
	}
}