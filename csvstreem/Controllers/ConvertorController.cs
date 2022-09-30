using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace csvstreem.Controllers;




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
    [Consumes("application/json")]
    public async Task<ActionResult> ConvertJson([BindRequired, FromQuery] RequestInfoFile param)
    {

        var _param = new RequestInfoFile{ csvUri = param.csvUri,separator = param.separator,type= "json" };
        return await Transform(_param);

    }
    [HttpGet(Name = "Convertor")]
    [Consumes("application/xml")]
    public async Task<ActionResult> ConvertXML([BindRequired, FromQuery] RequestInfoFile param)
    {
        var _param = new RequestInfoFile{ csvUri = param.csvUri,separator = param.separator,type= "xml" };
        return await Transform(_param);
    }
    private async Task<ActionResult> Transform(RequestInfoFile param)
    {
        if (!ModelState.IsValid)  
        {  
           return BadRequest();
        } 
        try
        {
            return await ConvertTo(param.csvUri,param.separator,param.type);
        }
        catch(BadUriException e)
        {
               return BadRequest("The CSV is badly formatted");
        }
        catch (BadFormatException e)
        {
            return BadRequest("The CSV is badly formatted");
        }
        catch (Exception e)
        {
                    Response.StatusCode = 400;
        }


        return new EmptyResult();
    }
    private async Task<EmptyResult> ConvertTo(string url, char separator, string type)
    {
        var Css = new CssData().SetSeparator(separator);


        IConvertData ConvertData;
        if (type == "json")
            ConvertData = new JsonConvert();
        else
            ConvertData = new XMLConvert();

        ConvertData.SetData(Css);


        Action<string> outputdata = async (line) => {await printPageAsync(line);};
        //Action<string> outputdataSync = (line) => {printPage(line);};

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
                   Response.StatusCode = 200;
                   verified = true;
                   outputdata(ConvertData.GetPreHeader());
               }
               else if(Css.isReady) 
                    outputdata(ConvertData.GetPartialConvert());
               //Thread.Sleep(1000);
           });

        outputdata(ConvertData.GetAfterFooter());

        return new EmptyResult();
    }
    private async Task<EmptyResult> printPageAsync(string line)
    {
        StreamWriter sw;
        await using ((sw = new StreamWriter(Response.Body)).ConfigureAwait(false))
        {
            await sw.WriteLineAsync(line).ConfigureAwait(false);
            await sw.FlushAsync().ConfigureAwait(false);

        }
        return null;
    }
    private async void printPage(string line)
    {
        StreamWriter sw;
        await using ((sw = new StreamWriter(Response.Body)))
        {
            sw.Write(line);
        }
    }
}
