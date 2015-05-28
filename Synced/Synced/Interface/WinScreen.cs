using Microsoft.Xna.Framework;
using Synced.Actors;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synced.Interface
{
    class WinScreen : SplashScreen
    {
        public enum TypeOfWin { SuddenDeath, TeamVictory}
        public TypeOfWin WinType
        {
            get;
            private set;
        }
        public WinScreen(Game game) : base(Library.WinScreens.Background,game)
        {
            WinType = TypeOfWin.SuddenDeath;
            SplashTime = new TimeSpan(10);
            GameComponents.Add(new Sprite(Library.WinScreens.SuddenTeam,new Vector2(ResolutionManager.GetCenterPointWidth,ResolutionManager.GetCenterPointHeight), Static_Classes.DrawingHelper.DrawingLevel.Top, Game));
        }
        public WinScreen(Game game, Player player) : base(Library.WinScreens.Background,game)
        {
            WinType = TypeOfWin.TeamVictory;
            SplashTime = new TimeSpan(10);
            Sprite teamColor = null;
            switch (player.TeamColor)
            {
                case Synced.Content.Library.Colors.ColorName.Blue:
                    teamColor = new Sprite(Library.WinScreens.BlueTeam, new Vector2(ResolutionManager.GetCenterPointWidth,ResolutionManager.GetCenterPointHeight), Static_Classes.DrawingHelper.DrawingLevel.Top, Game);
                    break;
                case Synced.Content.Library.Colors.ColorName.Green:
                    teamColor = new Sprite(Library.WinScreens.GreenTeam, new Vector2(ResolutionManager.GetCenterPointWidth, ResolutionManager.GetCenterPointHeight), Static_Classes.DrawingHelper.DrawingLevel.Top, Game);
                    break;
                case Synced.Content.Library.Colors.ColorName.Red:
                    teamColor = new Sprite(Library.WinScreens.RedTeam, new Vector2(ResolutionManager.GetCenterPointWidth, ResolutionManager.GetCenterPointHeight), Static_Classes.DrawingHelper.DrawingLevel.Top, Game);
                    break;
                case Synced.Content.Library.Colors.ColorName.Yellow:
                    teamColor = new Sprite(Library.WinScreens.YellowTeam, new Vector2(ResolutionManager.GetCenterPointWidth, ResolutionManager.GetCenterPointHeight), Static_Classes.DrawingHelper.DrawingLevel.Top, Game);
                    break;
                default:
                    break;
            }
            GameComponents.Add(teamColor);
        }
        
    }
}
