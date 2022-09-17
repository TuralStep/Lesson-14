using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lesson_14;

class Program
{

    public static void OpenPicture(string path)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        cmd.StandardInput.WriteLine("\"" + path + "\"");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();
    }

    static void Main()
    {
        string message = "";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Images\\";

        Directory.CreateDirectory(path);

        while (true)
        {

            Console.WriteLine("-----Screen Control-----");
            Console.WriteLine("1) Take screenshot");
            Console.WriteLine("2) Show list of screenshots");
            Console.WriteLine("3) Delete screenshot by id");
            Console.WriteLine("4) Open screenshot by id");
            Console.WriteLine("5) Exit program\n");
            Console.WriteLine(message);
            Console.WriteLine("\n");

            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
#pragma warning disable CA1416
                    var bitmap = new Bitmap(1366, 768);
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(0, 0, 0, 0,
                            bitmap.Size, CopyPixelOperation.SourceCopy);
                    }

                    string imgName = DateTime.Now.ToString("M-d-yyyy___hh mm ss");

                    bitmap.Save(path + imgName + ".png", ImageFormat.Png);
                    message = "Screenshot succesfully saved in Images folder...";
#pragma warning restore CA1416

                    break;
                case ConsoleKey.D2:

                    short index = 1;
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    foreach (FileInfo file in dirInfo.GetFiles())
                        Console.WriteLine($"{index++}) {file.Name}");

                    if (index == 1)
                    {
                        message = "No screenshots found...";
                        break;
                    }
                    else message = "";

                    Console.WriteLine("\n\nPress any key to continue...");
                    Console.ReadKey();

                    break;
                case ConsoleKey.D3:

                    short index2 = 1;

                    List<string> list = new List<string>();

                    DirectoryInfo dirInfor = new DirectoryInfo(path);
                    foreach (FileInfo file in dirInfor.GetFiles())
                    {
                        Console.WriteLine($"{index2++}) {file.Name}");
                        list.Add(file.Name);
                    }
                    if (index2 == 1)
                    {
                        message = "No screenshots to delete...";
                    }
                    else
                    {
                        Console.Write("Enter an ID to delete : ");
                        int delInput = 0;
                        try
                        {
                            delInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            message = "Invalid input...";
                            break;
                        }

                        if (delInput > 0 && delInput < list.Count)
                        {
                            File.Delete(path + list[delInput - 1]);
                            message = "Screenshot succesfully deleted...";
                            //message = path + list[delInput - 1];
                        }
                        else
                            message = "No screenshot with that ID...";

                    }

                    break;
                case ConsoleKey.D4:

                    short index3 = 1;

                    List<string> list2 = new List<string>();

                    DirectoryInfo dirInfor2 = new DirectoryInfo(path);
                    foreach (FileInfo file in dirInfor2.GetFiles())
                    {
                        Console.WriteLine($"{index3++}) {file.Name}");
                        list2.Add(file.Name);
                    }
                    if (index3 == 1)
                    {
                        message = "No screenshots found to open...";
                    }
                    else
                    {
                        Console.Write("Enter an ID to open : ");
                        int openInput = 0;
                        try
                        {
                            openInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            message = "Invalid input...";
                            break;
                        }

                        if (openInput > 0 && openInput < list2.Count)
                        {
                            OpenPicture(path + list2[openInput - 1]);
                            message = "Image succesfully opened...";
                            // message = path + list2[openInput - 1];
                        }
                        else
                            message = "No screenshot with that ID...";
                    }

                    break;
                case ConsoleKey.D5:
                    Console.Clear();
                    Console.WriteLine("\n\tThanks for using our program...\n");
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();

        }

    }

}