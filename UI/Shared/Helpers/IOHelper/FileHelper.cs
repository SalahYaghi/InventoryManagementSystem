using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Shared.Helpers.IO_Helper
{
    public static class FileHelper
    {
        public static string ChooseFolderPathDialog() {

            using (FolderBrowserDialog browser = new FolderBrowserDialog()) {

                browser.Description = "Select Folder";

                if (browser.ShowDialog() == DialogResult.OK) {

                    return browser.SelectedPath;
                }

            }

            return string.Empty;
        }
        public static Image BytesToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(bytes))
            using (Image tmp = Image.FromStream(ms))
            {
                return new Bitmap(tmp);
            }
        }
        public static async Task<List<FileResponse>> DecompressToZip(byte[] zip) {

            var stream = new MemoryStream(zip);

            List<FileResponse> result = new List<FileResponse>();

            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
            {

                foreach (var entry in archive.Entries)
                {
                    if (entry.Length == 0)
                        continue;
                     using (var entryStream = entry.Open())
                    {
                        
                        using (var memory = new MemoryStream())
                        {
                            await entryStream.CopyToAsync(memory);
                            result.Add(new FileResponse()
                            {
                                FileBytes = memory.ToArray(),
                                FileName = Path.GetFileNameWithoutExtension(entry.Name),
                                ContentType = Path.GetExtension(entry.Name)
                            }); 
                        }
                    }
                
                }
            
            }

            return result;
        }

    }
}

