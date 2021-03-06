﻿using System;
using System.Collections.Generic;
using System.Reflection;
using eDriven.Gui.Data;
using eDriven.Gui.GUIStyles;
using eDriven.Gui.Layout;
using eDriven.Gui.Reflection;
using eDriven.Gui.Shapes;
using eDriven.Gui.States;
using eDriven.Gui.Styles;
using UnityEngine;

namespace eDriven.Gui.Components
{
    [HostComponent(typeof(DropDownList))]

    [Style(Name = "showBackground", Type = typeof(bool), Default = false)]
    [Style(Name = "backgroundColor", Type = typeof(Color), Default = 0xdadada)]
    [Style(Name = "backgroundTexture", Type = typeof(Texture))]

    [Style(Name = "borderStyle", Type = typeof(GUIStyle), ProxyType = typeof(OnePxBorderStyle))]
    [Style(Name = "borderColor", Type = typeof(Color), Default = 0x222222)]

    [Style(Name = "scrollerSkin", Type = typeof(Type), Default = typeof(ScrollerSkin))]

    public class DropDownListSkin : Skin
    {
        public DropDownListSkin()
        {
            States = new List<State>(new[]
            {
                new State("normal")
                {
                    Overrides = new List<IOverride>
                    {
                        new SetProperty("_anchor", "DisplayPopup", false)
                    }
                }, 
                new State("open")
                {
                    Overrides = new List<IOverride>
                    {
                        new SetProperty("_anchor", "DisplayPopup", true)
                    }
                }, 
                new State("disabled")
            });
        }

        /*public override void SetCurrentState(string stateName)
        {
            base.SetCurrentState(stateName);

            Debug.Log("DDLS State: " + stateName);
        }*/

        #region Members

        [Obfuscation(Exclude = true)]
        private PopUpAnchor _anchor;
        [Obfuscation(Exclude = true)]
        private RectShape _background;
        [Obfuscation(Exclude = true)]
        private Scroller _scroller;
        [Obfuscation(Exclude = true)]
        private RectShape _border;
        
        #endregion

        #region Skin parts

        /* skin part */
        ///<summary>Label display
        ///</summary>
        // ReSharper disable MemberCanBePrivate.Global
        public Group DropDown;
        // ReSharper restore MemberCanBePrivate.Global

        /* skin part */
        ///<summary>Label display
        ///</summary>
        // ReSharper disable MemberCanBePrivate.Global
        public Button OpenButton;
        // ReSharper restore MemberCanBePrivate.Global

        /* skin part */
        ///<summary>Label display
        ///</summary>
        // ReSharper disable MemberCanBePrivate.Global
        public Label LabelDisplay;
        // ReSharper restore MemberCanBePrivate.Global

        // ReSharper disable MemberCanBePrivate.Global
        /* skin part */
        ///<summary>Content group
        ///</summary>
        public DataGroup DataGroup;

        #endregion

        protected override void CreateChildren()
        {
            base.CreateChildren();

            #region Popup anchor

            _anchor = new PopUpAnchor
            {
                Id = "pop_up",
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0,
                PopupPosition = PopupPosition.Below,
                PopupWidthMatchesAnchorWidth = true
            };
            AddChild(_anchor);

            #endregion

            #region DropDown

            DropDown = new Group
            {
                Id = "drop_down",
                MaxHeight = 134, MinHeight = 22,
                Width = 150
            };
            //AddChild(DropDown);
            _anchor.Popup = DropDown;

            #endregion

            #region Background

            _background = new RectShape
            {
                Id = "background",
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0
            };
            DropDown.AddChild(_background);

            #endregion

            #region Scroller

            _scroller = new Scroller
            {
                Id = "scroller",
                SkinClass = EvaluateSkinClassFromStyle("scrollerSkin"),
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0,
                MinViewportInset = 1,
                HasFocusableChildren = false,
                Height = 100
            };
            DropDown.AddChild(_scroller);

            #endregion
            
            #region Data group

            DataGroup = new DataGroup
            {
                Id = "data_group",
                ItemRenderer = new ItemRendererFactory<DefaultItemRenderer>(),
                Layout = new VerticalLayout
                {
                    Gap = 0,
                    HorizontalAlign = HorizontalAlign.ContentJustify,
                    RequestedMinRowCount = 5
                }
            };
            _scroller.Viewport = DataGroup;

            #endregion

            #region Border

            _border = new RectShape
            {
                Id = "border",
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0,
                MouseEnabled = false
            };
            DropDown.AddChild(_border);

            #endregion

            #region OpenButton

            OpenButton = new Button
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0,
                FocusEnabled = false,
                SkinClass = typeof(DropDownListButtonSkin)
            };
            AddChild(OpenButton);

            #endregion
            
            #region Label

            LabelDisplay = new Label
            {
                VerticalCenter = 0,
                Left = 7,
                Right = 32,
                Top = 2,
                Bottom = 2,
                Width = 75,
                MouseEnabled = false,
                Color = Color.black
            };
            //LabelDisplay.SetStyle("color", GetStyle("textColor"));
            AddChild(LabelDisplay);

            #endregion
        }

        protected override void UpdateDisplayList(float width, float height)
        {
            base.UpdateDisplayList(width, height);

            _border.SetStyle("backgroundStyle", GetStyle("borderStyle"));
            _border.SetStyle("backgroundColor", GetStyle("borderColor"));
        }
    }
}