namespace TelecomServiceSystem.Services.HtmlToPDF
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class HtmlToPdfConverter : IHtmlToPdfConverter
    {
        public byte[] Convert(string basePath, string htmlCode, string formatType = "A4", string orientationType = "Portrait")
        {
            var inputFileName = $"input_{Guid.NewGuid()}.html";
            var outputFileName = $"output_{Guid.NewGuid()}.pdf";
            File.WriteAllText($"{basePath}/{inputFileName}", htmlCode);
            var startInfo = new ProcessStartInfo(@"C:\PhantomJs\bin\phantomjs\bin\phantomjs.exe")
            {
                WorkingDirectory = basePath,
                Arguments = $"rasterize.js \"{inputFileName}\" \"{outputFileName}\" \"{formatType}\" \"{orientationType.ToLower()}\"",
                UseShellExecute = true,
            };

            var process = new Process { StartInfo = startInfo };
            process.Start();

            process.WaitForExit();

            var bytes = File.ReadAllBytes($"{basePath}/{outputFileName}");

            File.Delete($"{basePath}/{inputFileName}");
            File.Delete($"{basePath}/{outputFileName}");

            return bytes;
        }

        public byte[] ConvertToImage(string basePath, string htmlCode, string formatType = "A4", string orientationType = "Portrait")
        {
            var inputFileName = $"input_{Guid.NewGuid()}.html";
            var outputFileName = $"output_{Guid.NewGuid()}.png";
            File.WriteAllText($"{basePath}/{inputFileName}", htmlCode);
            var startInfo = new ProcessStartInfo(@"C:\PhantomJs\bin\phantomjs\bin\phantomjs.exe")
            {
                WorkingDirectory = basePath,
                Arguments = $"rasterize.js \"{inputFileName}\" \"{outputFileName}\" \"{formatType}\" \"{orientationType.ToLower()}\"",
                UseShellExecute = true,
            };

            var process = new Process { StartInfo = startInfo };
            process.Start();

            process.WaitForExit();

            var bytes = File.ReadAllBytes($"{basePath}/{outputFileName}");

            File.Delete($"{basePath}/{inputFileName}");
            File.Delete($"{basePath}/{outputFileName}");

            return bytes;
        }
    }
}
