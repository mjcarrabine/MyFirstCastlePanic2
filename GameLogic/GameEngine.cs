using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstCastlePanic.GameLogic
{
    public class GameEngine
    {
        public GameState State { get; private set; }
        private Random rng = new();

        public GameEngine(int playerCount)
        {
            State = new GameState
            {
                PlayerCount = playerCount,
                CastleDeck = GameSetup.CreateCastleDeck().OrderBy(_ => rng.Next()).ToList(),
                MonsterPile = GameSetup.CreateMonsterPile().OrderBy(_ => rng.Next()).ToList(),
                BoardSpaces = GameSetup.CreateBoardSpaces(),
                PlayerHands = new List<List<CastleCard>>()
            };
            for (int i = 0; i < playerCount; i++)
                State.PlayerHands.Add(new List<CastleCard>());
            // Deal 1 card to each player
            for (int i = 0; i < playerCount; i++)
                DrawCard(i);
            // Place 3 starred monsters on first 3 spaces
            var starred = State.MonsterPile.Where(m => m.IsStarred).Take(3).ToList();
                int[] startingSpaces = { 1, 2, 3 };
                for (int i = 0; i < starred.Count; i++)
                {
                    starred[i].SpaceIndex = startingSpaces[i];
                    State.MonstersOnBoard.Add(starred[i]);
                    State.MonsterPile.Remove(starred[i]);
                }
        }

        public void DrawCard(int player)
        {
            if (State.CastleDeck.Count > 0)
            {
                State.PlayerHands[player].Add(State.CastleDeck[0]);
                State.CastleDeck.RemoveAt(0);
            }
        }

        public bool PlayCardOnSpace(int player, int cardIndex, int spaceIndex)
        {
            // Validate indices
            if (cardIndex < 0 || cardIndex >= State.PlayerHands[player].Count)
                return false;
            var card = State.PlayerHands[player][cardIndex];
            var space = State.BoardSpaces.FirstOrDefault(s => s.Index == spaceIndex);
            if (space == null)
                return false;
            // Find a monster on this space
            var monster = State.MonstersOnBoard.FirstOrDefault(m => m.SpaceIndex == spaceIndex);
            if (monster == null)
                return false;
            // Card/space matching logic
            bool canPlay = false;
            if (card.Color.HasValue && card.Shape.HasValue)
                canPlay = card.Color == space.Color && card.Shape == space.Shape;
            else if (card.IsHero && card.Color.HasValue)
                canPlay = card.Color == space.Color;
            else if (card.IsAnyColor && card.Shape.HasValue)
                canPlay = card.Shape == space.Shape;
            if (!canPlay)
                return false;
            // Remove monster and card
            State.MonstersOnBoard.Remove(monster);
            State.PlayerHands[player].RemoveAt(cardIndex);
            DrawCard(player);
            return true;
        }

        public void AskForHelp(int player)
        {
            // TODO: Implement ask for help logic
        }

        public void MoveMonsters()
        {
            // Move each monster 1 space closer to the castle (higher index)
            foreach (var monster in State.MonstersOnBoard)
            {
                int currentIdx = State.BoardSpaces.FindIndex(s => s.Index == monster.SpaceIndex);
                if (currentIdx < State.BoardSpaces.Count - 1)
                {
                    monster.SpaceIndex = State.BoardSpaces[currentIdx + 1].Index;
                }
                // else: monster is at the castle, handle damage in future logic
            }
        }

        public void DrawMonster()
        {
            if (State.MonsterPile.Count == 0)
                return;

            var monster = State.MonsterPile[0];
            State.MonsterPile.RemoveAt(0);

            if (monster.Type == MonsterType.Runner)
            {
                // Find the monster closest to the castle (highest board index)
                int maxIdx = -1;
                if (State.MonstersOnBoard.Count > 0)
                {
                    maxIdx = State.MonstersOnBoard.Max(m => State.BoardSpaces.FindIndex(s => s.Index == m.SpaceIndex));
                }
                int runnerSpaceIdx = maxIdx + 1;
                if (runnerSpaceIdx >= State.BoardSpaces.Count)
                {
                    // Runner hits the wall/castle (future logic)
                    // For now, just do not add to board
                }
                else
                {
                    monster.SpaceIndex = State.BoardSpaces[runnerSpaceIdx].Index;
                    State.MonstersOnBoard.Add(monster);
                }
            }
            else
            {
                // Place on START space
                monster.SpaceIndex = State.BoardSpaces[0].Index;
                State.MonstersOnBoard.Add(monster);

                if (monster.Type == MonsterType.Shover)
                {
                    // Move all monsters 1 more space
                    MoveMonsters();
                }
                else if (monster.Type == MonsterType.Marcher)
                {
                    // Move all monsters 1 more space
                    MoveMonsters();
                    // Draw 1 more monster (recursive, so special effects chain)
                    DrawMonster();
                }
            }
        }

        public void CheckGameOver()
        {
            // TODO: Implement win/loss check
        }
    }
}
