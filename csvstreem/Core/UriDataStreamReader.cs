using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
class UriDataStreamReader
{
    public string Uri { get; set; }
    public UriDataStreamReader SetUri(string Uri)
    {
        if (! System.Uri.IsWellFormedUriString(Uri, UriKind.Absolute))
        {
            throw new BadUriException("Bad Uri : IsWellFormedUriString");
        }
            this.Uri = Uri;
        
        return this;
    }
    public async Task ExecEachLine(Action<string> TraitAction)
    {
        //https://people.sc.fsu.edu/~jburkardt/data/csv/addresses.csv
        using (HttpClient client = new HttpClient())
        {
            using (HttpResponseMessage response = await client.GetAsync(Uri, HttpCompletionOption.ResponseHeadersRead))
            using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
            using(var streamReader = new StreamReader(streamToReadFrom))
            {
                //System.Web.Script.Serialization.JavaScriptSerializer().Serialize(csv);

                while(!streamReader.EndOfStream)
                {
                    var Line = streamReader.ReadLine();
                    TraitAction(Line);
                    //read += Encoding.UTF8.GetString(buffer, 0, readed);
                }
                 
            }
        }
    }
}