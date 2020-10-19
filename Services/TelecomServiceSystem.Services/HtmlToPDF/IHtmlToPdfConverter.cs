namespace TelecomServiceSystem.Services.HtmlToPDF
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public interface IHtmlToPdfConverter
    {
        byte[] Convert(string basePath, string htmlCode, string formatType, string orientationType);
    }
}
