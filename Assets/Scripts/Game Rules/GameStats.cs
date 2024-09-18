using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : Singleton<GameStats>
{
    [SerializeField]HeightScore heightScore;
    public float HeightScore => heightScore.HighestScore;
    protected override void Awake()
      {
          base.Awake();
          if (heightScore == null) 
          {
              Debug.LogError("HeightScore not assigned in GameStats! Make sure to assign heightScore refrence in Unity GUI!");
          }
      }
}
