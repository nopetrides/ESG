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

	[SerializeField] private GameModeScriptable gameModeData = null;
		
	private Player _player;

	private ThrowablesListScriptables gameMode = null;
	
	// removed Awake()
	
	void Start()
	{
		PlayerInfoLoader playerInfoLoader = new PlayerInfoLoader();
		playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
		playerInfoLoader.Load();

		// get data about game mode selection from previous scene
		//gameMode = ;
	}

	void Update()
	{
		UpdateHud(); // doing this every frame is unnecessary
	}

	public void OnPlayerInfoLoaded(Hashtable playerData)
	{
		_player = new Player(playerData);
		_player.OnCoinsChanged += UpdateHud;
	}

	private void InitializeHud()
	{
		nameLabel.text = "Name: " + _player.GetName();
		UpdateHud();
	}
	public void UpdateHud()
	{
		moneyLabel.text = "Money: $" + _player.GetCoins(); // ToString is uncessary
	}

	/// <summary>
	/// receives the Throwable from the dynamically setup buttons
	/// </summary>
	/// <param name="hand"></param>
	public void HandlePlayerInput(ThrowableScriptable hand)
	{
		UpdateGame(hand);
	}

	private void UpdateGame(ThrowableScriptable playerChoice)
	{
		// creating a new object then calling to do some logic, then having garbage collecting clean this up is not that efficient
		GameUpdateLogic.Load(playerChoice, OnGameUpdated);
	}

	public void OnGameUpdated(Dictionary<string,object> gameUpdateData)
	{
		playerHand.text = GetNameOfThrowable((ThrowableScriptable)gameUpdateData[HashConstants.GUD_PLAYER_RESULT]);
		enemyHand.text = GetNameOfThrowable((ThrowableScriptable)gameUpdateData[HashConstants.GUD_OPPONENT_RESULT]);

		_player.ChangeCoinAmount((int)gameUpdateData[HashConstants.GUD_MONEY_CHANGE]);
	}

	private string GetNameOfThrowable(ThrowableScriptable throwable)
	{
		return throwable == null ? "Nothing" : throwable.Name;
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.OnCoinsChanged -= UpdateHud;
		}
	}
}