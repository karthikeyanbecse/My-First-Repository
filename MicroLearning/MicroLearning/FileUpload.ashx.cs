using System;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace MicroLearning.Services
{
    /// <summary>
    /// File Upload httphandler to receive files and save them to the server.
    /// </summary>
    public class FileUpload : IHttpHandler
    {
        private string ffmpegPhysicalPath = @"C:\***\***\ffmpeg.exe";

        public void ProcessRequest(HttpContext context)
        {
            const string path = "Capture/Videos";
            String filename = HttpContext.Current.Request.Headers["X-File-Name"];

            if (string.IsNullOrEmpty(filename) && HttpContext.Current.Request.Files.Count <= 0)
            {
                context.Response.Write("{success:false}");
            }
            else
            {
                string mapPath = HttpContext.Current.Server.MapPath(path);
                string phyicalFilePath = string.Empty;
                if (Directory.Exists(mapPath) == false)
                {
                    Directory.CreateDirectory(mapPath);
                }
                if (filename == null)
                {
                    //This work for IE
                    try
                    {
                        HttpPostedFile uploadedfile = context.Request.Files[0];
                        filename = uploadedfile.FileName;
                        phyicalFilePath = mapPath + "\\" + filename;
                        uploadedfile.SaveAs(phyicalFilePath);

                        var flvpath =ConvertToFLV(phyicalFilePath);
                        var tumbnail = CreateThumbnail(phyicalFilePath);
                        

                        context.Response.Write("{success:true, name:\"" + filename + "\", path:\"" + path + "/" +
                                               Path.GetFileName(flvpath) + "\", image:\"" + path+"/"+Path.GetFileName(tumbnail) + "\"}");

                    }
                    catch (Exception)
                    {
                        context.Response.Write("{success:false}");

                    }
                }
                else
                {
                    //This work for Firefox and Chrome.
                    phyicalFilePath = mapPath + "\\" + filename;
                    FileStream fileStream = new FileStream(phyicalFilePath, FileMode.OpenOrCreate);

                    try
                    {
                        Stream inputStream = HttpContext.Current.Request.InputStream;
                        inputStream.CopyTo(fileStream);

                        var flvpath = ConvertToFLV(phyicalFilePath);
                        var tumbnail = CreateThumbnail(phyicalFilePath);
                        
                        context.Response.Write("{success:true, name:\"" + filename + "\", path:\"" + path + "/" +
                                               Path.GetFileName(flvpath) + "\", image:\"" + path + "/" + Path.GetFileName(tumbnail) + "\"}");

                    }
                    catch (Exception)
                    {
                        context.Response.Write("{success:false}");
                    }
                    finally
                    {
                        fileStream.Close();
                    }

                }
            }

        }

        private string CreateThumbnail(string phyicalFilePath)
        {
            if (AuthenticationHelper.ImpersonateValidUser("user", ".", "*****"))
            {
                //var argument = string.Format("-ss 12 -i {0} -f image2 -vframes 1 {1}", phyicalFilePath, Path.ChangeExtension(phyicalFilePath, "jpg"));
                var argument = string.Format("-y -i {0} -an -ss 00:00:14.35 -r 1 -vframes 1 -f mjpeg  {1}", phyicalFilePath, Path.ChangeExtension(phyicalFilePath, "jpg"));
                
                ProcessStartInfo process = new ProcessStartInfo(ffmpegPhysicalPath, argument);
                Process proc = new Process();
                proc.StartInfo = process;
                proc.Start();
                proc.WaitForExit();
                AuthenticationHelper.UndoImpersonation();
                return Path.ChangeExtension(phyicalFilePath, "jpg");
            }

            return string.Empty;
        }

        private string ConvertToFLV(string phyicalFilePath)
        {
            if (Path.GetExtension(phyicalFilePath).Equals(".flv")) return phyicalFilePath;
            //if (AuthenticationHelper.ImpersonateValidUser("user", ".", "*********"))
            //{
                var argument = string.Format("-i {0} -vcodec flv -f flv -r 29.97 -s 320x240 -aspect 4:3 -b 300k -g 160 -cmp dct  -subcmp dct  -mbd 2 -flags +aic+cbp+mv0+mv4 -trellis 1 -ac 1 -ar 22050 -ab 56k {1}", phyicalFilePath, Path.ChangeExtension(phyicalFilePath, "flv"));
               // var argument = string.Format("-i {0} -crf 35.0 -vcodec libx264 -acodec libfaac -ar 48000 -ab 128k -coder 1 -flags +loop -cmp +chroma -partitions +parti4x4+partp8x8+partb8x8 -me_method hex -subq 6 -me_range 16 -g 250 -keyint_min 25 -sc_threshold 40 -i_qfactor 0.71 -b_strategy 1 -threads 0 {1}", phyicalFilePath, Path.ChangeExtension(phyicalFilePath, "mp4"));
                try
                {
                    ProcessStartInfo process = new ProcessStartInfo(ffmpegPhysicalPath, argument);
                    Process proc = new Process();
                    proc.StartInfo = process;
                    proc.Start();
                    proc.WaitForExit();
                    AuthenticationHelper.UndoImpersonation();
                    return Path.ChangeExtension(phyicalFilePath, "flv");
                }
                catch { }
            //}

            return string.Empty;
        }

        public bool IsReusable
        {
            get { return false; }
        }


    }

    public static class AuthenticationHelper
    {
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;


        private static WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
                                            String lpszDomain,
                                            String lpszPassword,
                                            int dwLogonType,
                                            int dwLogonProvider,
                                            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
                                                int impersonationLevel,
                                                ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);


        public static bool ImpersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                               LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        public static void UndoImpersonation()
        {
            impersonationContext.Undo();
        }
    }
}