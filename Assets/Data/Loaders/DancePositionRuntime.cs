﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DanceFlow
{
    /// <summary>
    /// Represents one dance position (a single orientation for the two people without any moving)
    /// </summary>
    public class DancePositionRuntime
    {
        public enum PositionDifficulty
        {
            Easy,
            Med,
            Hard
        }
        

        public DancePositionRuntime(string positionName, PositionDifficulty difficulty = PositionDifficulty.Easy, bool isLift = false, bool isDip = false, float xSpot = 0, float ySpot = 0 , string iconUrl = null)
        {
            this.PositionName = positionName;
            this.Difficulty = difficulty;
            this.IsALift = isLift;
            this.IsADip = isDip;
            this.XSpot = xSpot;
            this.YSpot = ySpot;
            this.IconUrl = IconUrl;
        }


        public string PositionName
        {
            get;
            set;
        }

        public PositionHolder MyHolder
        {
            get;
            set;
        }

        public PositionDifficulty Difficulty
        {
            get;
            set;
        }

        public bool IsALift
        {
            get;
            set;
        }

        public bool IsADip
        {
            get;
            set;
        }

        public float XSpot
        {
            get;
            set;
        }

        public float YSpot
        {
            get;
            set;
        }

        public string IconUrl
        {
            get;
            set;
        }

        public List<DanceMoveRuntime> moves = new List<DanceMoveRuntime>();

        public string DifficultyToString()
        {
            switch (this.Difficulty)
            {
                case PositionDifficulty.Easy:
                    return "Easy";
                case PositionDifficulty.Med:
                    return "Med";
                case PositionDifficulty.Hard:
                    return "Hard";
                default:
                    return "Easy";

            }
        }
    }
}
