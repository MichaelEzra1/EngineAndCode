using System.Collections.Generic;
using Pirates;
using System;
using System.Linq;

namespace MyBot
{
    public class MyBot : Pirates.IPirateBot
    {
        Location loc;

        public void DoTurn(IPirateGame game)
        {
            //this is not real code just example

            if (game.GetTurn() == 1)
            {
                loc = new Location(20, 17);

                Location destination = game.GetSailOptions(game.MyPirates()[2], new Location(game.MyPirates()[2].InitialLocation.Row + 1, game.MyPirates()[2].InitialLocation.Col), 1)[0];
                game.SetSail(game.MyPirates()[2], destination);
            }
            if (game.MyLostPirates().Count >= 1)
            {
                Location destination = game.GetSailOptions(game.MyPirates()[1], new Location(game.MyPirates()[1].InitialLocation.Row + 1, game.MyPirates()[1].InitialLocation.Col), 1)[0];
                game.SetSail(game.MyPirates()[1], destination);
            }
            if (game.MySoberPirates().Count > 2 && game.GetTurn() > 1)
            {
                Pirate pirate = game.MySoberPirates()[0];
                Pirate attack = game.MySoberPirates()[1];
                bool Shooted = false;
                bool ShootedA = false;
                Location destination = new Location(0, 0);
                Location AttackDestination = new Location(20, 17);

                #region movement

                if (!attack.HasTreasure && null != IsInRange(game, attack))
                {
                    game.Attack(attack, IsInRange(game, attack));
                    ShootedA = true;
                }

                if (!ShootedA)
                {

                    Location loc1 = new Location(20, 17);
                    Location loc2 = new Location(20, 26);
                    if (attack.HasTreasure)
                        loc = attack.InitialLocation;
                    if (attack.Location.Col == 16 && attack.Location.Row == 20)
                        loc = loc2;
                    if (attack.Location.Col == 28 && attack.Location.Row == 20)
                        loc = loc1;

                    AttackDestination = game.GetSailOptions(attack, loc, 1)[0];

                    game.SetSail(attack, AttackDestination);
                }
                ShootedA = false;
                #endregion

                #region Movement
                if (!pirate.HasTreasure)
                {

                    if (game.Treasures().Count > 8)
                    {

                        destination = game.GetSailOptions(pirate, game.Treasures()[8].Location, 5)[0];
                    }
                    else if (game.Treasures().Count > 0)
                    {
                        destination = game.GetSailOptions(pirate, game.Treasures()[0].Location, 5)[0];
                    }
                }
                else
                {
                    destination = game.GetSailOptions(pirate, pirate.InitialLocation, 1)[1];
                }



                #endregion
                Shooted = false;
                game.SetSail(pirate, destination);
            }

        }

        private Pirate IsInRange(IPirateGame game, Pirate pirate)
        {
            foreach (var item in game.EnemySoberPirates())
            {
                if (game.InRange(pirate, item))
                    return item;
            }
            return null;
        }
    }
}