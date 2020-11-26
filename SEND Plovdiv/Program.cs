using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace SEND_Plovdiv
{
   
    public class WebRequestGetExample
    {

        public static void Main()
        {
            string[] lineOfContents = File.ReadAllLines(@"C:\\M\send.txt");

            string username = "";
            string password = "";
            foreach (var line in lineOfContents)
            {
                string[] tokens = line.Split(',');
                string user = tokens[0];
                string pass = tokens[1];

                username = user;
                password = pass;
            }

            string pathFile = @"C:\M\Telegrami\";
            string zipPath = @"C:\M\Plovdiv.zip";

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            ZipFile.CreateFromDirectory(pathFile, zipPath, CompressionLevel.Fastest, false);

            /* // .zip файла идва счупен :)
             * 
        // Get the object used to communicate with the server.
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://195.96.246.42/Plovdiv.zip");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(username, password);

            // Copy the contents of the file to the request stream.  
            StreamReader sourceStream = new StreamReader(@"C:\M\Plovdiv.zip");
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());

            sourceStream.Close();

            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
            Console.ReadKey();

            response.Close();
            */

            //Second method
            String sourcefilepath = zipPath;
            String ftpurl = "ftp://192.168.0.1/Plovdiv.zip";
            String ftpusername = username; 
            String ftppassword = password;

            try
            {
                string filename = Path.GetFileName(zipPath);
                string ftpfullpath = ftpurl;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = File.OpenRead(zipPath);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();

                Console.WriteLine("Телеграмата е изпратена успешно..");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Изпращането е неуспешно..!" + ex);
                Console.ReadKey();
            }

        }
    }
}

