namespace Enums {
    public enum SceneNames {
        Menu, Space, End
    }

    static class SceneNamesMethods {
        public static string GetString(this SceneNames sceneNames) {
            return sceneNames.ToString();
        }
    }
}