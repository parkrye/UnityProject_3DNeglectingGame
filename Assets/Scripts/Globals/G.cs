public static class G
{
    private static UIManager _ui = new UIManager();
    public static UIManager UI { get { return _ui; } }
    private static Datas _data = new Datas();
    public static Datas Data { get { return _data; } }
    public static Stage CurrentStage { get; set; }
    private static V _variables = new V();
    public static V V { get { return _variables;} }
}
