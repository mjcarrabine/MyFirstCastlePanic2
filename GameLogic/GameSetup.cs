using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstCastlePanic.GameLogic
{
    public static class GameSetup
    {
        public static List<CastleCard> CreateCastleDeck()
        {
            var deck = new List<CastleCard>();
            // 2 of each color/shape
            foreach (var color in new[] { CardColor.Blue, CardColor.Green, CardColor.Red })
            {
                foreach (var shape in new[] { CardShape.Square, CardShape.Triangle, CardShape.Circle })
                {
                    deck.Add(new CastleCard { Color = color, Shape = shape, Name = $"{color} {shape}" });
                    deck.Add(new CastleCard { Color = color, Shape = shape, Name = $"{color} {shape}" });
                }
            }
            // Hero cards
            deck.Add(new CastleCard { Color = CardColor.Blue, IsHero = true, Name = "Blue Hero" });
            deck.Add(new CastleCard { Color = CardColor.Green, IsHero = true, Name = "Green Hero" });
            deck.Add(new CastleCard { Color = CardColor.Red, IsHero = true, Name = "Red Hero" });
            // Any-Color cards
            deck.Add(new CastleCard { Shape = CardShape.Square, IsAnyColor = true, Name = "Square Any-Color" });
            deck.Add(new CastleCard { Shape = CardShape.Triangle, IsAnyColor = true, Name = "Triangle Any-Color" });
            deck.Add(new CastleCard { Shape = CardShape.Circle, IsAnyColor = true, Name = "Circle Any-Color" });
            // Utility cards
            deck.Add(new CastleCard { IsUtility = true, Name = "Wall" });
            deck.Add(new CastleCard { IsUtility = true, Name = "Boot" });
            return deck;
        }

        public static List<MonsterToken> CreateMonsterPile()
        {
            var pile = new List<MonsterToken>();
            // Regular monsters: 10 (3 starred)
            for (int i = 0; i < 10; i++)
                pile.Add(new MonsterToken { Type = MonsterType.Regular, IsStarred = i < 3 });
            // Shover: 2
            for (int i = 0; i < 2; i++)
                pile.Add(new MonsterToken { Type = MonsterType.Shover });
            // Runner: 2
            for (int i = 0; i < 2; i++)
                pile.Add(new MonsterToken { Type = MonsterType.Runner });
            // Marcher: 2
            for (int i = 0; i < 2; i++)
                pile.Add(new MonsterToken { Type = MonsterType.Marcher });
            return pile;
        }

        public static List<BoardSpace> CreateBoardSpaces()
        {
            return new List<BoardSpace>
            {
                new BoardSpace { Index = 1, Color = CardColor.Red, Shape = CardShape.Triangle },
                new BoardSpace { Index = 2, Color = CardColor.Green, Shape = CardShape.Circle },
                new BoardSpace { Index = 3, Color = CardColor.Blue, Shape = CardShape.Square },
                new BoardSpace { Index = 4, Color = CardColor.Green, Shape = CardShape.Triangle },
                new BoardSpace { Index = 5, Color = CardColor.Blue, Shape = CardShape.Circle },
                new BoardSpace { Index = 6, Color = CardColor.Red, Shape = CardShape.Square },
                new BoardSpace { Index = 7, Color = CardColor.Blue, Shape = CardShape.Triangle },
                new BoardSpace { Index = 8, Color = CardColor.Red, Shape = CardShape.Circle },
                new BoardSpace { Index = 9, Color = CardColor.Green, Shape = CardShape.Square },
            };
        }
    }
}
