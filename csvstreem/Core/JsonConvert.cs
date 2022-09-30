using System.Linq;
using System.Text.Json;
public class JsonConvert : IConvertData
{
    CssData CssData;
    public string GetAfterFooter()
            => "{}\n]";


    public string GetPartialConvert()
    {
        try
        {
            return !CssData.isReady ? "" : "    {\n" + string.Join(",\n", CssData.HeaderData.Select((str, i) =>
                                                                            {
                                                                                var key = "     \"" + JsonEncodedText.Encode(str) + "\" : ";
                                                                                var value = double.TryParse(CssData.LignesData[0][i], out var resd) 
                                                                                            ?  resd.ToString()
                                                                                            : "\"" + JsonEncodedText.Encode(CssData.LignesData[0][i]) + "\"";

                                                                                return key + value;
                                                                            }
                                            ).ToList()

                                        ) + "\n    },";

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public string GetPreHeader() => "[";

    IConvertData IConvertData.SetData(CssData Data)
    {
        this.CssData = Data;
        return this;
    }
}