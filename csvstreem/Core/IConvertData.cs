interface IConvertData
{
    IConvertData SetData(CssData Data);
    string GetPreHeader();

    string GetPartialConvert();

    string GetAfterFooter();
}