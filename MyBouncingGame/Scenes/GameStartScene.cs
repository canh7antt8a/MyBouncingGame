﻿using System;
using CocosSharp;
using MyBouncingGame.Views;

namespace MyBouncingGame.Scenes
{
	public class GameStartScene : CCScene
	{
		CCLayer gameStartLayer;

		CCLabel gameStartTittle;
		CCSprite gameStartBackground;

		Button goToLevelSceneBtn;
		Button goToSettingBtn;

		public GameStartScene (CCWindow mainWindow) : base(mainWindow)
		{
			CreateLayer ();

			addBackgroundLabel ();

			CreateButton ();
		}

		private void CreateLayer()
		{
			gameStartLayer = new CCLayer ();
			this.AddChild (gameStartLayer);
		}

		private void addBackgroundLabel()
		{
			gameStartBackground = new CCSprite ("images/GameStartSceneBackground.png");
			gameStartBackground.Position = ContentSize.Center;
			gameStartBackground.IsAntialiased = false;
			gameStartLayer.AddChild (gameStartBackground);

			gameStartTittle = new CCLabel ("Bouncing King", "fonts/go3v2.ttf", 75, CCLabelFormat.SystemFont);
			gameStartTittle.Color = new CCColor3B (0, 0, 0);
			gameStartTittle.PositionX = gameStartLayer.VisibleBoundsWorldspace.Center.X;
			gameStartTittle.PositionY = gameStartLayer.VisibleBoundsWorldspace.Center.Y + 120.0f;
			gameStartLayer.AddChild (gameStartTittle);
		}

		private void CreateButton()
		{
			goToLevelSceneBtn = new Button (gameStartLayer);
			goToLevelSceneBtn.ButtonStyle = ButtonStyle.confirmBtn1;
			goToLevelSceneBtn.btnText = "Start";
			goToLevelSceneBtn.PositionX = gameStartLayer.VisibleBoundsWorldspace.UpperRight.X - 150.0f;
			goToLevelSceneBtn.PositionY = gameStartLayer.VisibleBoundsWorldspace.LowerLeft.Y + 230.0f;
			goToLevelSceneBtn.Clicked += goToLevelSelectScene;
			gameStartLayer.AddChild(goToLevelSceneBtn);

			goToSettingBtn = new Button (gameStartLayer);
			goToSettingBtn.ButtonStyle = ButtonStyle.confirmBtn2;
			goToSettingBtn.btnText="Setting";
			goToSettingBtn.PositionX = gameStartLayer.VisibleBoundsWorldspace.UpperRight.X - 150.0f;
			goToSettingBtn.PositionY = gameStartLayer.VisibleBoundsWorldspace.LowerLeft.Y + 100.0f;
			goToSettingBtn.Clicked += goToSettingScene;
			gameStartLayer.AddChild(goToSettingBtn);
		}

		private void goToLevelSelectScene(object sender, EventArgs args)
		{
			gameStartLayer.RemoveAllChildren (true);

			goToLevelSceneBtn.Dispose ();
			goToSettingBtn.Dispose ();

			GameAppDelegate.GoToGameScene ();
		}

		private void goToSettingScene(object sender, EventArgs args)
		{
			gameStartLayer.RemoveAllChildren (true);

			goToLevelSceneBtn.Dispose ();
			goToSettingBtn.Dispose ();

			GameAppDelegate.GoToGameScene ();
		}
			
	}
}
