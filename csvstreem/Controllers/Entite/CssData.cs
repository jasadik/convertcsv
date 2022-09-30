public class CssData
{
    public bool isReady { get { return HeaderData != null && LignesData != null && LignesData.Count > 0; } }
    public List<string> HeaderData { get; set; }
    public List<List<string>> LignesData { get; set; }

    public int DataSizeMax { get; set; } = 1;
    public char Separator { get; set; }

    public CssData()
    {
        LignesData = new List<List<string>>();
    }
    public CssData(char Separator) : this()
    {
        this.Separator = Separator;
    }
    public CssData SetSeparator(char Separator)
    {
        this.Separator = Separator;
        return this;
    }
    public CssData SetDataSizeMax(int DataSize)
    {
        this.DataSizeMax = DataSize;
        return this;
    }
    public CssData ParceHeader(string Header)
    {
        try
        {
            HeaderData = Header.Split(Separator).ToList();
        }
        catch (Exception e)
        {
            throw e;
        }
        return this;
    }
    public CssData AddData(string Line)
    {
        if (HeaderData == null)
            return this.ParceHeader(Line);
        return this.ParceAddLigne(Line);
    }
    private void addLigne(List<string> Ligne)
    {
        if (LignesData.Count > DataSizeMax && DataSizeMax > -1)
        {
            LignesData.RemoveAt(0);
        }
        LignesData.Add(Ligne);
    }

    public CssData ParceAddLigne(string Ligne)
    {
        try
        {
            var res = Ligne.Split(Separator).ToList();
            addLigne(res);
        }
        catch (Exception e)
        {
            throw e;
        }
        return this;
    }
    public bool Verify()
    {
        return this.isReady && HeaderData.Count == LignesData[0].Count;
    }

}