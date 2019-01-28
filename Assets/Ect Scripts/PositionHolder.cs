using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace DanceFlow
{

    public class PositionHolder : MonoBehaviour
    {
        public DancePositionRuntime position;

        public enum DisplayMode
        {
            String,
            Icon
        }

        [SerializeField]
        private DisplayMode displayMode = DisplayMode.String;

        [SerializeField]
        private Text positionName;

        [SerializeField]
        private DragMe dragger;



        [SerializeField]
        private Image border;

        [SerializeField]
        private Image background;

        [SerializeField]
        private Image icon;



        [SerializeField]
        private float borderWidth = 6;

        [SerializeField]
        private float backgroundWidth = 6;

        [SerializeField]
        private float imageSize = 128;

        
        [SerializeField]
        private Shader iconShader;



        [SerializeField]
        private BoxCollider2D arrowOrienterCollider;

        //whether or not to automatically set it to icon mode if a icon is loaded for it
        [SerializeField]
        private bool ShouldAutoSetIcon = false;

        //some test buttons for updating images midgame
#if UNITY_EDITOR
        public float NewBorderWidth = 0;

        [AutomaticEditorButton]
        public string editorButtonBorderWidthString = "editorButtonBorderWidth";

        public void editorButtonBorderWidth()
        {
            UpdateBorderWidth(NewBorderWidth);
        }


        public float NewBackgroundWidth = 0;

        [AutomaticEditorButton]
        public string editorButtonBackgroundWidthString = "editorButtonBackgroundWidth";

        public void editorButtonBackgroundWidth()
        {
            UpdateBackgroundWidth(NewBackgroundWidth);
        }

        public float NewImageWidth = 0;

        /*
        [AutomaticEditorButton]
        public string editorButtonImageWidthString = "editorButtonImageWidth";

        public void editorButtonImageWidth()
        {
            //UpdateBackgroundWidth(NewBorderWidth);
        }
        */

        public DisplayMode newMode = DisplayMode.String;

        [AutomaticEditorButton]
        public string editorButtonChangeModeString = "editorButtonChangeMode";

        public void editorButtonChangeMode()
        {
            ChangeDisplayMode(newMode);
        }

#endif

        //update the thing on top of it to update its size
        //image/text on background on border
        private void UpdateBorderWidth(float newWidth)
        {
            borderWidth = newWidth;
            background.GetComponent<RectTransform>().offsetMin = new Vector2(newWidth, newWidth);
            background.GetComponent<RectTransform>().offsetMax = new Vector2(-newWidth, -newWidth);
        }

        //update the thing on top of it to update its size
        //image/text on background on border
        private void UpdateBackgroundWidth(float newWidth)
        {
            backgroundWidth = newWidth;
            icon.GetComponent<RectTransform>().offsetMin = new Vector2(newWidth, newWidth);
            icon.GetComponent<RectTransform>().offsetMax = new Vector2(-newWidth, -newWidth);
        }

        private void ChangeDisplayMode(DisplayMode newMode)
        {
            RectTransform trans = this.transform as RectTransform;
            RefreshMoves();
            switch (newMode)
                {
                case DisplayMode.Icon:
                    //the mode for displaying a icon of the move

                    trans.sizeDelta = new Vector2(imageSize + borderWidth + backgroundWidth, imageSize + borderWidth + backgroundWidth);
                    arrowOrienterCollider.size = trans.sizeDelta;
                    positionName.gameObject.SetActive(false);
                    icon.gameObject.SetActive(true);

                    break;
                case DisplayMode.String:

                    //the mode for displaying the name of the move


                    //TextGenerator textGen = new TextGenerator();
                    //TextGenerationSettings generationSettings = positionName.GetGenerationSettings(positionName.rectTransform.rect.size);
                    //float width = textGen.GetPreferredWidth(position.PositionName, generationSettings);


                   //the automatic estimate wasnt working so this just goes with a default 12 sizes per character
                    float width = position.PositionName.Length * 12;
                    trans.sizeDelta = new Vector2(40 + width, 40);

                    arrowOrienterCollider.size = trans.sizeDelta;
                    positionName.gameObject.SetActive(true);
                    icon.gameObject.SetActive(false);
                    break;
                default:
                    throw new System.Exception("Oieh not a valid display mode setup");
            }
        }

        private void SetIcon(Texture newIcon)
        {
            icon.material = new Material(iconShader);
            icon.material.mainTexture = newIcon;
            if (ShouldAutoSetIcon)
            {
                ChangeDisplayMode(DisplayMode.Icon);
            }
        }

        public void Initialize(DancePositionRuntime position, Settings settings)
        {
            this.gameObject.name = position.PositionName;
            position.MyHolder = this;
            this.position = position;
            positionName.text = position.PositionName;
           


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

            UpdateBackgroundWidth(backgroundWidth);
            UpdateBorderWidth(borderWidth);
            ChangeDisplayMode(this.displayMode);
            WebFileGrabber.instance.AddImageCallback(position.IconUrl, SetIcon);
        }

        public void ToggleMoveUpdates(bool state)
        {
            foreach (var move in position.moves)
            {
                move.MyHolder.SetLineUpdate(state);
            }
        }

        public void RefreshMoves()
        {
            foreach (var move in position.moves)
            {
                if(move.MyHolder!=null)
                move.MyHolder.RefreshLine();
            }
        }
    }
}