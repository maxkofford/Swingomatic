using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DanceFlow
{

    public class PositionHolder : MonoBehaviour
    {
        public DancePositionRuntime position;

        [SerializeField]
        private Text positionName;

        [SerializeField]
        private DragMe dragger;

        [SerializeField]
        private Image background;

        [SerializeField]
        private Image border;

        [SerializeField]
        private BoxCollider2D collider;

        public void Initialize(DancePositionRuntime position, Settings settings)
        {
            this.gameObject.name = position.PositionName;
            position.MyHolder = this;
            this.position = position;
            positionName.text = position.PositionName;
            RectTransform trans = this.transform as RectTransform;

            //TextGenerator textGen = new TextGenerator();
            //TextGenerationSettings generationSettings = positionName.GetGenerationSettings(positionName.rectTransform.rect.size);
            //float width = textGen.GetPreferredWidth(position.PositionName, generationSettings);

            float width = position.PositionName.Length*12;
            trans.sizeDelta = new Vector2(trans.sizeDelta.x + width, trans.sizeDelta.y);

            collider.size = trans.sizeDelta;

            dragger.DragStartHandler += () =>ToggleMoveUpdates(true);
            dragger.DragEndHandler += ()=>ToggleMoveUpdates(false);

            this.transform.localPosition = new Vector3(position.XSpot, position.YSpot, 0);
            switch (position.Difficulty)
            {
                case DancePositionRuntime.PositionDifficulty.Easy:
                    background.color = settings.EasyColor;
                    break;
                case DancePositionRuntime.PositionDifficulty.Med:
                    background.color = settings.MedColor;
                    break;
                case DancePositionRuntime.PositionDifficulty.Hard:
                    background.color = settings.HardColor;
                    break;

                default:
                    background.color = settings.MedColor;
                    break;

            }
            

                
        }

        public void ToggleMoveUpdates(bool state)
        {
            foreach (var move in position.moves)
            {
                move.MyHolder.SetLineUpdate(state);
            }
        }
    }
}