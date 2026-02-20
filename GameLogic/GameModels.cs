using System.Collections.Generic;

namespace MyFirstCastlePanic.GameLogic
{
    public enum CardColor { Blue, Green, Red }
    public enum CardShape { Square, Triangle, Circle }
    public enum MonsterType { Regular, Shover, Runner, Marcher }

    public class CastleCard
    {
        public CardColor? Color { get; set; }
        public CardShape? Shape { get; set; }
        public bool IsHero { get; set; }
        public bool IsAnyColor { get; set; }
        public bool IsUtility { get; set; }
        public string? Name { get; set; }
    }

    public class MonsterToken
    {
           public MonsterType Type { get; set; }
           public bool IsStarred { get; set; }
           public int SpaceIndex { get; set; } // 1-based index of board space
    }

    public class BoardSpace
    {
        public int Index { get; set; }
        public CardColor Color { get; set; }
        public CardShape Shape { get; set; }
        public string Name => $"{Color} {Shape}";
    }

    public class GameState
    {
        public List<CastleCard> CastleDeck { get; set; } = new();
        public List<MonsterToken> MonsterPile { get; set; } = new();
        public List<MonsterToken> MonstersOnBoard { get; set; } = new();
        public List<BoardSpace> BoardSpaces { get; set; } = new();
        public int WallHealth { get; set; } = 1;
        public int CastleHealth { get; set; } = 1;
        public int CurrentPlayer { get; set; } = 0;
        public int PlayerCount { get; set; } = 2;
        public List<List<CastleCard>> PlayerHands { get; set; } = new();
        public bool IsGameOver { get; set; } = false;
        public bool IsWin { get; set; } = false;
    }
}
