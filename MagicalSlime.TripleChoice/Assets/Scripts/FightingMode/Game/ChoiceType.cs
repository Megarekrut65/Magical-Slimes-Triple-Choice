namespace FightingMode.Game
{
    public enum ChoiceType
    {
        Top = 0,
        Center = 1,
        Bottom = 2,
        None = -1
        
    }

    public static class ChoiceTypeCorrect
    {
        public static bool IsCorrect(int choice)
        {
            return choice is >= (int)ChoiceType.Top and <= (int)ChoiceType.Bottom;
        }
    }
}