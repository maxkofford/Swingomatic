using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceFlow
{
    public class MoveHolder : MonoBehaviour
    {
        [SerializeField]
        private LineConnector connector;

        [SerializeField]
        private ArrowOrienter arrow;

        public DanceMoveRuntime move;
        public void Initialize(DanceMoveRuntime move, Settings settings)
        {
            this.gameObject.name = move.LeftPosition.PositionName + " to " + move.RightPosition.PositionName;
            move.MyHolder = this;
            this.move = move;

            connector.SetStartPoint(move.LeftPosition.MyHolder.transform);
            connector.SetEndPoint(move.RightPosition.MyHolder.transform);
            arrow.SetStartPoint(move.LeftPosition.MyHolder.transform);
            arrow.SetEndPoint(move.RightPosition.MyHolder.transform);
            connector.SetColor(settings.MoveLineColor);
            connector.lineWidth = settings.MoveLineThickness;
            RefreshLine();
        }

        public void SetLineUpdate(bool newValue)
        {
            connector.SetShouldUpdate(newValue);
            arrow.SetShouldUpdate(newValue);
        }

        public void RefreshLine()
        {
            connector.RefreshLine();
            arrow.RefreshArrow();
        }
    }
}