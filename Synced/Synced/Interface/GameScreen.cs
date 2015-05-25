// CharacterSelector.cs
// Introduced: 2015-04-29
// Last edited: 2015-05-18
// Edited by:
// Pontus Magnusson
// Dennis Stockhaus
// Robin Calmegård
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Synced.Actors;
using Synced.Content;
using Synced.MapNamespace;
using Synced.Static_Classes;
using System.Collections.Generic;

namespace Synced.Interface
{
    class GameScreen : Screen
    {
        Map _map;
        List<Player> _players;

        public GameScreen(Game game, World world) // TODO: tmp added world to parameters, might solve in a different way later. 
            : base (game)
        {
            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game);
            ScreenManager.Instance.AddScreen(_map);

            _players = new List<Player>();
            foreach (var item in _map.Data.Objects)
            {
                if (item is PlayerStart)
                {
                    PlayerStart temp = item as PlayerStart;
                    _players.Add(new Player(temp.PlayerIndex, Library.Character.Name.Circle,Library.Colors.ColorName.Blue, game, world)); // TODO: All collision objects need world!
                }
            }
        }
    }
}
