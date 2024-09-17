using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : Singleton<GameStats>
{
    [SerializeField]HeightScore heightScore;
    public float HeightScore => heightScore.HighestScore;
}
