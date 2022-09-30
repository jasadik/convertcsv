using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace csvstreem.Controllers;

public class RequestInfoFile
{
    public string csvUri { get; set; }
    public string type { get; set; }
    public char? separator { get; set; }


}


[ApiController]
[Route("[controller]")]
public class ConvertorController : ControllerBase
{
    private readonly ILogger<ConvertorController> _logger;

    public ConvertorController(ILogger<ConvertorController> logger)
    {
        _logger = logger;
    }

    

    [HttpGet(Name = "Convertor")]
    public async Task<ActionResult> GetAll([BindRequired, FromQuery] RequestInfoFile param)
    {
        string url = param.csvUri;
        string type = param.type;

        try
        {
            return await ConvertTo(url,';',type);
        }
        catch(BadUriException e)
        {
            return BadRequest("Url Not Formated");
        }
        catch (BadFormatException e)
        {
                    return BadRequest("File not formated");
        }
        catch (Exception e)
        {
                    Response.StatusCode = 400;
        }


        return new EmptyResult();
    }
    private async Task<EmptyResult> ConvertTo(string url, char separator = ',', string type = "xml")
    {
        var Css = new CssData().SetSeparator(separator);


        IConvertData ConvertData;
        if (type == "json")
            ConvertData = new JsonConvert();
        else
            ConvertData = new XMLConvert();

        ConvertData.SetData(Css);


        bool verified = false;
        await new UriDataStreamReader()
        .SetUri(url)
        .ExecEachLine(async (Line) =>
           {
               Css.AddData(Line);
               if (!verified && Css.isReady)
               {
                   if (!Css.Verify())
                   {
                       throw new BadFormatException("The CSV is badly formatted");
                   }
                    //Response.ContentType = "application/json";
                    Response.StatusCode = 200;
                   await printPage(ConvertData.GetPreHeader());
                   verified = true;
               }
               await printPage(ConvertData.GetPartialConvert());
               //Thread.Sleep(1000);
           });

        await printPage(ConvertData.GetAfterFooter());

        return null;
    }
    private async Task<EmptyResult> printPage(string line)
    {
        StreamWriter sw;
        await using ((sw = new StreamWriter(Response.Body)).ConfigureAwait(false))
        {
            await sw.WriteLineAsync(line).ConfigureAwait(false);
            await sw.FlushAsync().ConfigureAwait(false);

        }
        return null;
    }
}
