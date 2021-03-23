using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// This script is just the view controller, and should not perform any logic or loading
/// Since player input is entirely UI based, the input and UI are combined in this script
/// otherwise, we could separate out the input handling as well
/// </summary>
public class GameViewController : MonoBehaviour
{
	[SerializeField] private GameLogicController gameManager = null;
	
	[SerializeField] private Text playerHand; // converted from public to SerializedField
	[SerializeField] private Text enemyHand;

	[SerializeField] private Text nameLabel; // these can also be serialized fields instead of being assigned on Awake
	[SerializeField] private Text moneyLabel;
	[SerializeField] private ThrowableButton playerChoiceButtonPrefab = null;
	[SerializeField] private Transform buttonsParent = null;
	[SerializeField] private GameObject gameOverPopup = null; // we could make this a prefab, but for simplicity its just a game object in the scene.
	[SerializeField] private Text resultText = null;
	[SerializeField] private Text streakText = null;
	[SerializeField] private Text bestMoneyText = null;
	[SerializeField] private Text bestStreakText = null;
	[SerializeField] private Image rulesImage = null;

	// removed Awake()

	private void OnEnable()
	{
		// we don't need to create an instance of an object
		gameManager.OnPlayerInfoLoaded += OnPlayerInfoLoaded;
		gameManager.OnPlayerInfoUpdated += UpdatePlayerMoney;
		gameManager.OnGameUpdated += OnGameUpdated;
		gameOverPopup.SetActive(false); // ensure the game over state is hidden by default
		resultText.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		if (gameManager == null)
			return;
		gameManager.OnPlayerInfoLoaded -= OnPlayerInfoLoaded;
		gameManager.OnPlayerInfoUpdated -= UpdatePlayerMoney;
		gameManager.OnGameUpdated -= OnGameUpdated;
	}

	public void OnPlayerInfoLoaded(Hashtable playerData)
	{
		// get data about game mode selection from previous scene
		Debug.Log("Playing GameMode: " + gameManager.GameMode.LastSelectedGameMode.Description);

		InitializeHud();
	}

	private void InitializeHud()
	{
		nameLabel.text = "Name: " + gameManager.Player.Name;
		SetupPlayerChoices();
		UpdatePlayerMoney();
		UpdatePlayerBest();
	}

	private void SetupPlayerChoices()
	{
		foreach (var choice in gameManager.GameMode.LastSelectedGameMode.Throwables)
		{
			ThrowableButton button = Instantiate(playerChoiceButtonPrefab, buttonsParent, false);
			button.Setup(choice);
			button.Clickable.onClick.AddListener(delegate
			{
				AudioManager.Instance.PlayButtonSelectedSFX();
				HandlePlayerInput(choice);
			});
		}

		rulesImage.sprite = gameManager.GameMode.LastSelectedGameMode.RulesSprite;
		playerChoiceButtonPrefab.gameObject.SetActive(false);
	}
	
	public void UpdatePlayerMoney()
	{
		moneyLabel.text = "Money: $" + gameManager.Player.Coins; // ToString is uncessary
		if (gameManager.Player.Coins <= 0)
		{
			gameOverPopup.SetActive(true);
		}
	}

	/// <summary>
	/// Game over screen creates new player
	/// </summary>
	public void RestPlayerButton()
	{
		gameOverPopup.SetActive(false);
		gameManager.ResetPlayer();
		AudioManager.Instance.PlayButtonPressedSFX();
	}

	public void ReturnToGameModeMenu()
	{
		SceneManager.LoadScene("GameModeSelectScene");
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
		gameManager.OnInputHandled(playerChoice);
	}

	public void OnGameUpdated(Dictionary<string,object> gameUpdateData, Result outcome)
	{
		playerHand.text = GetNameOfThrowable((ThrowableScriptable)gameUpdateData[HashConstants.GUD_PLAYER_RESULT]);
		enemyHand.text = GetNameOfThrowable((ThrowableScriptable)gameUpdateData[HashConstants.GUD_OPPONENT_RESULT]);
		ResultPseudoAnimation(outcome);
		if (gameManager.Player.Streak > 1)
		{
			streakText.gameObject.SetActive(true);
			streakText.text = $"x{gameManager.Player.Streak}";
		}
		else
		{
			streakText.gameObject.SetActive(false);
		}

		UpdatePlayerBest();
	}

	private void UpdatePlayerBest()
	{
		// We don't need to update these everytime,
		// optionally we could see if these values changed and then only update them
		// (setting text in Unity is always done even if the text is the same)
		bestMoneyText.text = $"Most Money At Once: {gameManager.Player.MostCoins}";
		bestStreakText.text = $"Highest Streak: {gameManager.Player.BestStreak}";
	}

	private void ResultPseudoAnimation(Result outcome)
	{
		resultText.gameObject.SetActive(true);
		resultText.text = Enum.GetName(typeof(Result), outcome) + "!";
		switch (outcome)
		{
			case Result.Draw:
				resultText.color = Color.white;
				break;
			case Result.Won:
				resultText.color = Color.green;
				break;
			case Result.Lost:
				resultText.color = Color.red;
				break;
		}
		// code-driven randomization of position for some "pop"
		// but maybe can start an animation here of the result text bouncing or something
		resultText.transform.position =  Random.insideUnitCircle;
	}

	private string GetNameOfThrowable(ThrowableScriptable throwable)
	{
		return throwable == null ? "Nothing" : throwable.Description;
	}
}