using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is main game manager
/// </summary>
public class GameLogicController : MonoBehaviour
{
    [SerializeField] private GameModeScriptable gameModeData = null;
    public GameModeScriptable GameMode => gameModeData;
    public event Action<Hashtable> OnPlayerInfoLoaded;
    public event Action OnPlayerInfoUpdated;
    public event Action<Dictionary<string,object>> OnGameUpdated;
    private Player _player;
    public Player Player => _player;
    private OpponentDecider _opponent;
    
    void Start()
    {
        // we don't need to create an instance of an object
        PlayerInfoLoader.Load(PlayerInfoLoaded);
        _opponent = new OpponentDecider();
    }

    void PlayerInfoLoaded(Hashtable info)
    {		
        _player = new Player(info);
        _player.OnCoinsChanged += PlayerInfoChanged;
        OnPlayerInfoLoaded?.Invoke(info);
    }

    void PlayerInfoChanged()
    {
        OnPlayerInfoUpdated?.Invoke();
    }

    /// <summary>
    /// We receive the input from the UI, and we handle all the game logic here
    /// </summary>
    public void OnInputHandled(ThrowableScriptable playerChoice)
    {
        ThrowableScriptable opponentChoice = _opponent.GetOpponentHand(gameModeData.LastSelectedGameMode, playerChoice);
        GameUpdateLogic.Load(playerChoice, opponentChoice, GameUpdated);
    }

    private void GameUpdated(Dictionary<string,object> gameUpdateData)
    {
        _player.ChangeCoinAmount((int)gameUpdateData[HashConstants.GUD_MONEY_CHANGE]);
        OnGameUpdated?.Invoke(gameUpdateData);
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnCoinsChanged -= PlayerInfoChanged;
        }
    }
}
