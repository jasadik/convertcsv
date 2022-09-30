using System.Text.Json;
using System.Xml;

namespace csvstreemtest
{
    internal static class fileverification
    {
        private static bool IsJsonValid(this string txt)
        {
            try { return JsonDocument.Parse(txt) != null; } catch { }

            return false;
        }
        private static bool validateXML(string xml)
        {
            try
            {
                XmlDocument myDoc = new XmlDocument();
                myDoc.LoadXml(xml);
                return true;
            }
            catch (XmlException ex)
            {
                //take care of the exception
            }
            return false;
        }
    }
}
