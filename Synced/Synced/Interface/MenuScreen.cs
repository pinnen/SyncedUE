// MenuScreen.cs
// Introduced: 2015-04-16
// Last edited: 2015-04-29
// Edited by:
// Pontus Magnusson
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Synced.Actors;
using Synced.Content;
using Synced.Static_Classes;
using System;
using System.Collections.Generic;

namespace Synced.Interface
{
    class MenuScreen : Screen
    {
        #region Delegates & Event & Raisers

        public event StartNewGame NewGame;

        #endregion

        #region Variables & Properties
        const int _minimumPlayersConstant = 1;

        //SpriteBatch _spriteBatch
        //{
        //    get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); }
        //}


        #endregion


        public MenuScreen(Game game)
            : base(game)
        {
            //Menu background 
            DrawOrder = (int)DrawingHelper.DrawingLevel.Back;
            // Temporary screen variables (Half of screen)
            int w = ResolutionManager.GetCenterPointWidth;
            int h = ResolutionManager.GetCenterPointHeight;

            // Add character selectors
            GameComponents.Add(new CharacterSelector(PlayerIndex.One, new Rectangle(0, 0, w, h), Color.Blue, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Two, new Rectangle(w, 0, w, h), Color.Green, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Three, new Rectangle(0, h, w, h), Color.Red, Game));
            GameComponents.Add(new CharacterSelector(PlayerIndex.Four, new Rectangle(w, h, w, h), Color.Yellow, Game));
            // Background
            GameComponents.Add(new Sprite(Library.Interface.MenuBackground, Vector2.Zero, DrawingHelper.DrawingLevel.Back, game));

            Library.Audio.PlaySong(Library.Audio.Songs.MenuSong1);
        }

        public bool IsEveryoneReady()
        {
            int count = 0;
            int total = 0;
            foreach (var item in GameComponents)
            {
                if (item is CharacterSelector)
                {
                    if ((item as CharacterSelector).IsReady())
                        count++;
                    if ((item as CharacterSelector).IsConnected())
                        total++;
                }
            }
            return (count >= total && count >= _minimumPlayersConstant);
        }
        public List<Library.Character.Name> SelectedCharacter = new List<Library.Character.Name>();

        public override void Update(GameTime gameTime)
        {
            if (IsEveryoneReady() && NewGame != null
                && InputManager.IsButtonPressed(PlayerIndex.One, Buttons.Start))
            {
                foreach (var item in GameComponents)
                {
                    if (item is CharacterSelector)
                    {
                        if ((item as CharacterSelector).IsReady())
                        {
                            SelectedCharacter.Add((item as CharacterSelector).SelectedCharacter);
                        }
                    }
                }
                NewGame(this, new EventArgs());
            }
            base.Update(gameTime);
        }
    }
}
