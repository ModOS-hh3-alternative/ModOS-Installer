using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    // Make Main an async Task for network operations
    static async Task Main(string[] args)
    {
        // 1. Define the source URL and destination file path
        string OSUpdateDLL = "https://github.com/ModOS-hh3-alternative/DLLs/releases/download/v1.0.0/OSUpdateDLL.dll"; // Replace with your actual link
        string fxASPIIDLL = "https://github.com/ModOS-hh3-alternative/Dlls/releases/download/v1.0.0/fxASPI.dll";
        string LanguageResDLL = "https://github.com/ModOS-hh3-alternative/Dlls/releases/download/v1.0.0/LanguageResource.dll";
        string messagestxt = "https://github.com/ModOS-hh3-alternative/Dlls/releases/download/v1.0.0/messages.txt";
        string Physiumc2a = "https://github.com/ModOS-hh3-alternative/Dlls/releases/download/v1.0.0/Physium.c2a";
        string DLLLauncherexe = "https://github.com/ModOS-hh3-alternative/Dlls/releases/download/v1.0.0/DllLauncher.exe";
        string destinationFilePath = "DownloadedLibrary.dll";

        // 2. Download and write the file
        await DownloadAndWriteDllAsync(dllUrl, destinationFilePath);
    }

    public static async Task DownloadAndWriteDllAsync(string url, string destinationPath)
    {
        // HttpClient is designed to be instantiated once and reused throughout the application lifetime
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Get the response stream directly from the URL. This is efficient for large files.
                // It reads data as it arrives, rather than loading the whole file into memory first.
                using (Stream streamFromUrl = await client.GetStreamAsync(url))
                {
                    // Create a new FileStream to write the data to the local file system
                    // FileMode.Create ensures we overwrite the file if it exists, or create a new one
                    using (FileStream streamToFile = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                    {
                        // Copy the bytes from the source stream (URL) to the destination stream (File)
                        // This method copies all bytes efficiently without manual byte-by-byte looping
                        Console.WriteLine($"Starting download from {url}...");
                        await streamFromUrl.CopyToAsync(streamToFile);

                        Console.WriteLine($"\nDownload complete. File saved to {destinationPath}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}