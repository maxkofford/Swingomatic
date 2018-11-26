using System;
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

                List<DancePositionRuntime> runtimePositions = new List<DancePositionRuntime>();
                List<DanceMoveRuntime> runtimeMoves = new List<DanceMoveRuntime>();

                Dictionary<string, DancePositionRuntime> nameToPosition = new Dictionary<string, DancePositionRuntime>();

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


                        string[] splitLine = line.Split(new string[] { "->" }, System.StringSplitOptions.RemoveEmptyEntries);

                        DanceMoveRuntime currentPosition = new DanceMoveRuntime();

                        currentPosition.LeftPosition = nameToPosition[splitLine[0]];
                        currentPosition.RightPosition = nameToPosition[splitLine[1]];

                        nameToPosition[splitLine[0]].moves.Add(currentPosition);
                        nameToPosition[splitLine[1]].moves.Add(currentPosition);

                        runtimeMoves.Add(currentPosition);

                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error Move: " + line);
                        Debug.Log(e);
                    }

                }

                runtimeAllMoves = new AllMovesRuntime(runtimePositions, runtimeMoves);
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
