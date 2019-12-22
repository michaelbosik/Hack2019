namespace Enums {
    public enum ScriptNames {
        Menu, Game, End, PowerUpManager, AlienManager, PlayerManager
    }

    static class ScriptNamesMethods {
        public static string GetString(this ScriptNames scriptNames) {
            return scriptNames.ToString();
        }
    }
}