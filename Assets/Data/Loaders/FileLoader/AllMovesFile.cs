﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace DanceFlow
{

    public class AllMovesFile
    {
        private static AllMovesRuntime runtimeAllMoves;

        public static AllMovesRuntime GetAllMovesRuntime()
        {
            if (runtimeAllMoves == null)
            {
                string[] positions = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "PositionData.txt"));
                string[] moves = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "MoveData.txt"));
                string[] moveVariations = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "VariationData.txt"));
                string[] peopleStyles = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "PeopleStyleData.txt"));

                List<DancePositionRuntime> runtimePositions = new List<DancePositionRuntime>();
                List<DanceMoveRuntime> runtimeMoves = new List<DanceMoveRuntime>();
                Dictionary<string, DancePersonStyleRuntime> runtimePeopleStyles = new Dictionary<string, DancePersonStyleRuntime>();

                Dictionary<string, DancePositionRuntime> nameToPosition = new Dictionary<string, DancePositionRuntime>();
                Dictionary<string, DanceMoveRuntime> nameToMove = new Dictionary<string, DanceMoveRuntime>();

                foreach (string line in positions)
                {
                    try
                    {

                        if (line.Length < 2)
                            continue;
                        if (line[0] == '/' && line[1] == '/')
                            continue;

                        string[] splitLine = line.Split(',');

                        DancePositionRuntime currentPosition = new DancePositionRuntime(splitLine[0]);

                        switch (splitLine[1])
                        {
                            case "Easy":
                                currentPosition.Difficulty = DancePositionRuntime.PositionDifficulty.Easy;
                                break;
                            case "Med":
                                currentPosition.Difficulty = DancePositionRuntime.PositionDifficulty.Med;
                                break;
                            case "Hard":
                                currentPosition.Difficulty = DancePositionRuntime.PositionDifficulty.Hard;
                                break;
                            default:
                                currentPosition.Difficulty = DancePositionRuntime.PositionDifficulty.Easy;
                                break;
                        }

                        switch (splitLine[2])
                        {
                            case "0":
                                currentPosition.IsADip = false;
                                break;
                            case "1":
                                currentPosition.IsADip = true;
                                break;
                            default:
                                currentPosition.IsADip = false;
                                break;
                        }

                        switch (splitLine[3])
                        {
                            case "0":
                                currentPosition.IsALift = false;
                                break;
                            case "1":
                                currentPosition.IsALift = true;
                                break;
                            default:
                                currentPosition.IsALift = false;
                                break;
                        }

                        currentPosition.XSpot = float.Parse(splitLine[4]);
                        currentPosition.YSpot = float.Parse(splitLine[5]);

                        if (splitLine.Length > 5)
                            currentPosition.IconUrl = splitLine[6];

                        nameToPosition.Add(currentPosition.PositionName, currentPosition);
                        runtimePositions.Add(currentPosition);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error Position: " + line);
                        Debug.Log(e);
                    }

                }

                foreach (string line in moves)
                {
                    try
                    {
                        if (line.Length < 2)
                            continue;
                        if (line[0] == '/' && line[1] == '/')
                            continue;

                        string[] splitLine = line.Split(',');
                        string[] splitPositions = splitLine[0].Split(new string[] { "->" }, System.StringSplitOptions.RemoveEmptyEntries);

                        DanceMoveRuntime currentMove = new DanceMoveRuntime();

                        currentMove.LeftPosition = nameToPosition[splitPositions[0]];
                        currentMove.RightPosition = nameToPosition[splitPositions[1]];

                        nameToPosition[splitPositions[0]].moves.Add(currentMove);
                        nameToPosition[splitPositions[1]].moves.Add(currentMove);

                        currentMove.DanceMoveName = splitLine[0];

                        runtimeMoves.Add(currentMove);

                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error Move: " + line);
                        Debug.Log(e);
                    }

                }

                foreach (string line in moveVariations)
                {

                    try
                    {
                        if (line.Length < 2)
                            continue;
                        if (line[0] == '/' && line[1] == '/')
                            continue;

                        string[] splitLine = line.Split(',');

                        nameToMove[splitLine[0]].Variations.Add(new DanceVariationRuntime() { BaseMove = nameToMove[splitLine[0]], VariationName = splitLine[1] });
                    }

                    catch (Exception e)
                    {
                        Debug.Log("Error Variation: " + line);
                        Debug.Log(e);
                    }
                }

                foreach (string line in peopleStyles)
                {

                    try
                    {
                        if (line.Length < 2)
                            continue;
                        if (line[0] == '/' && line[1] == '/')
                            continue;

                        string[] splitLine = line.Split(',');

                        string personName = splitLine[0];
                        string dancePartName = splitLine[1];
                        string weight = splitLine[2];
                       
                        DancePersonStyleRuntime targetStyle;
                        if (!runtimePeopleStyles.ContainsKey(personName))
                        {
                            runtimePeopleStyles.Add(personName, new DancePersonStyleRuntime() { PersonName = personName });
                        }
                        targetStyle = runtimePeopleStyles[personName];

                        //if its a dance move....
                        if (dancePartName.Contains("->"))
                        {
                            targetStyle.myMoveWeights.Add(new DancePersonStyleRuntime.DanceMoveWeights() { TargetMove = nameToMove[dancePartName], Weight = float.Parse(weight) });
                        }
                        else
                        {
                            targetStyle.myPositionWeights.Add(new DancePersonStyleRuntime.DancePositionWeights() { TargetPosition = nameToPosition[dancePartName], Weight = float.Parse(weight) });
                        }

                    }

                    catch (Exception e)
                    {
                        Debug.Log("Error Person Style: " + line);
                        Debug.Log(e);
                    }
                }

                runtimeAllMoves = new AllMovesRuntime(runtimePositions,runtimeMoves);
            }

            return runtimeAllMoves;
        }

        public static void SaveAllMovesRuntime()
        {
            AllMovesRuntime allData = LoadMoves.theMoves;

            Comparison<DancePositionRuntime> comparisonPositions = new Comparison<DancePositionRuntime>((DancePositionRuntime first,DancePositionRuntime second)=>
            {
                return first.PositionName.CompareTo(  second.PositionName);
            });

            Comparison<DanceMoveRuntime> comparisonMoves = new Comparison<DanceMoveRuntime>((DanceMoveRuntime first, DanceMoveRuntime second) =>
            {
                return (first.LeftPosition.PositionName+first.RightPosition.PositionName).CompareTo(second.LeftPosition.PositionName + second.RightPosition.PositionName);
            });

            allData.Positions.Sort(comparisonPositions);
            allData.Moves.Sort(comparisonMoves);

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Path.Combine(Application.streamingAssetsPath, "PositionData.txt")))
            {
                foreach (var position in allData.Positions)
                {
                    file.WriteLine(position.PositionName + "," + position.DifficultyToString() +  "," + (position.IsADip ? 1 : 0) + "," +  (position.IsALift ? 1 : 0) + "," + position.MyHolder.transform.localPosition.x + "," + position.MyHolder.transform.localPosition.y);
                }
            }

            using (System.IO.StreamWriter file =
             new System.IO.StreamWriter(Path.Combine(Application.streamingAssetsPath, "MoveData.txt")))
            {
                foreach (var move in allData.Moves)
                {
                    file.WriteLine(move.LeftPosition.PositionName + "->" + move.RightPosition.PositionName);
                }
            }
        }
    }
}
