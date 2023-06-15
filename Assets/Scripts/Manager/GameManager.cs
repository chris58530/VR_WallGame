using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState gameState = GameState.Begin;


    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        Actions.GameInitialize?.Invoke();
        gameState = GameState.Begin;
    }

    private void Update()
    {
        Actions.GameUpdate?.Invoke();

        switch (gameState)
        {
            case GameState.Begin:
                Actions.GameBegin?.Invoke();
                Debug.Log("begin");
                break;
            case GameState.Start:
                Actions.BeforePlayerChoose?.Invoke();
                gameState = GameState.Choosing;

                break;
            case GameState.Choosing:
                Actions.OnPlayerChoose?.Invoke();

                break;
            case GameState.NextRound:
                Actions.AfterPlayerChoose?.Invoke();
                gameState = GameState.Start;
                break;
            case GameState.End:
                Actions.GameEnd?.Invoke();
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
