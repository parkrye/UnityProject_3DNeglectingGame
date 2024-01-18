public static class Global
{
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }
    private static UserData _userData = new UserData();
    public static UserData UD { get { return _userData; } }
    public static Stage CurrentStage { get; set; }
}
