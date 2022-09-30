using System.Linq;
using System.Text.Json;
public class JsonConvert : IConvertData
{
    CssData CssData;
    public string GetAfterFooter()
    {
        return "{}\n]";
    }

    public string GetPartialConvert()
    {
        try{
            if(CssData.isReady)
            {
                return "    {\n" + string.Join(",\n",CssData.HeaderData.Select((str,i)=> 
                                                                                {
                                                                                    var key = "     \"" + JsonEncodedText.Encode(str) + "\" : ";
                                                                                    var value = double.TryParse(CssData.LignesData[0][i], out var resd) ?     resd.ToString()
                                                                                                                                                        : "\"" + JsonEncodedText.Encode(CssData.LignesData[0][i]) + "\""  ;

                                                                                    return key + value;
                                                                                }
                                                ).ToList()
                                             
                                             ) + "\n    },";
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
        return "[";
    }

    IConvertData IConvertData.SetData(CssData Data)
    {
        this.CssData = Data;
        return this;
    }
}