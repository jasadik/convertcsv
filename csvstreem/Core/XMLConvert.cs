public class XMLConvert: IConvertData
{
    CssData CssData;
    public string GetAfterFooter()
    {
        return "</root>";
    }

    public string GetPartialConvert()
    {
        try{
            if(CssData.isReady)
            {
                return "    <row>\n" + string.Join("\n",
                                        CssData.HeaderData.Select(
                                            (s,i)=> 
                                                $@"         <{s.Trim()}>"+CssData.LignesData[0][i] +$@"</{s.Trim()}>" 
                ).ToList()) + "\n   </row>";
            }
            return "";
        }
        catch(Exception e)
        {
            throw e;
        }
    }

    public string GetPreHeader()
    {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n<root>";
    }

    IConvertData IConvertData.SetData(CssData Data)
    {
        this .CssData = Data;
        return this;
    }
}