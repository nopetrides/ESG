using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is main game manager
/// </summary>
public class GameLogicController : MonoBehaviour
{
    [SerializeField] private GameModeScriptable gameModeData = null;

    [SerializeField] private AudioClip gameMusic;
    public GameModeScriptable GameMode => gameModeData;
    public event Action<Hashtable> OnPlayerInfoLoaded;
    public event Action OnPlayerInfoUpdated;
    public event Action<Dictionary<string,object>, Result> OnGameUpdated;
    private Player _player;
    public Player Player => _player;
    private OpponentDecider _opponent;

    void Start()
    {
        if (AudioManager.Instance == null)
        {
            // we know that the audio manager is only in the game mode scene, so they must have started the game from the game scene
            SceneManager.LoadScene("GameModeSelectScene");
            return;
        }
        // we don't need to create an instance of an object
        PlayerInfoLoader.LoadPlayer(PlayerInfoLoaded);
        _opponent = new OpponentDecider();
        AudioManager.Instance.PlayLoopingMusic(gameMusic);
    }

    public void ResetPlayer()
    {
        PlayerInfoLoader.CreatePlayer(PlayerInfoLoaded);
    }
    
    void PlayerInfoLoaded(Hashtable info)
    {
        if (_player == null)
        {
            _player = new Player(info);
            _player.OnCoinsChanged += PlayerInfoChanged;
        }
        else
        {
            _player.Update(info);
        }
        OnPlayerInfoLoaded?.Invoke(info);
    }

    void PlayerInfoChanged()
    {
        // we could move this into the Player class, but we may have more than one value to change in the player
        // this function should be called once per set of changes
        PlayerInfoLoader.SavePlayer(_player.Coins, _player.MostCoins,_player.Streak, _player.BestStreak); 
        OnPlayerInfoUpdated?.Invoke();
    }

    /// <summary>
    /// We receive the input from the UI, and we handle all the game logic here
    /// </summary>
    public void OnInputHandled(ThrowableScriptable playerChoice)
    {
        ThrowableScriptable opponentChoice = _opponent.GetOpponentHand(gameModeData.LastSelectedGameMode, playerChoice);
        GameUpdateLogic.UpdateGame(playerChoice, opponentChoice, GameUpdated);
    }

    private void GameUpdated(Dictionary<string,object> gameUpdateData, Result outcome)
    {
        _player.SetStreak(outcome == Result.Won ? _player.Streak+1 : 0);
        _player.ChangeCoinAmount((int) gameUpdateData[HashConstants.GUD_MONEY_CHANGE] * (_player.Streak == 0 ? 1 : _player.Streak));
        OnGameUpdated?.Invoke(gameUpdateData, outcome);
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnCoinsChanged -= PlayerInfoChanged;
        }
    }
}
