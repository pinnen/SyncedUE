using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Synced.MapNamespace;
using Synced.Content;
using Synced.Static_Classes;
using Synced.Actors;
using FarseerPhysics.Dynamics;

namespace Synced.Interface
{
    class GameScreen : Screen
    {
        Map _map;
        List<Player> _players;
        World world; 

        public GameScreen(Game game) // TODO: tmp added world to parameters, might solve in a different way later. 
            : base (game)
        {
            world = new World(Vector2.Zero);

            _map = new Map(Library.Map.Path[Library.Map.Name.Paper], game);
            GameComponents.Add(_map);

            _players = new List<Player>();
            //foreach (var item in _map.Data.Objects)
            //{
            //    if (item is PlayerStart)
            //    {
            //        PlayerStart temp = item as PlayerStart;
            //        _players.Add(new Player(temp.PlayerIndex, Library.Character.Name.Circle, game, world)); // TODO: All collision objects need world!
            //    }
            //}
        }
    }
}
