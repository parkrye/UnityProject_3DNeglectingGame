public static class Global
{
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }
    private static Datas _data = new Datas();
    public static Datas Datas { get { return _data; } }
    public static Stage CurrentStage { get; set; }
}
