using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState = GameState.Start;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        Actions.GameInitialize?.Invoke();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                Actions.BeforePlayerChoose?.Invoke();
                gameState = GameState.Choosing;

                break;
            case GameState.Choosing:
                Player.canDetect = true;
                Actions.OnPlayerChoose?.Invoke();

                break;
            case GameState.NextRound:
                Actions.AfterPlayerChoose?.Invoke();
                gameState = GameState.Start;
                break;
        }
    }
    private void FixedUpdate()
    {
        Actions.SlaveMove?.Invoke();

        Actions.SlaveLeave?.Invoke();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

}
