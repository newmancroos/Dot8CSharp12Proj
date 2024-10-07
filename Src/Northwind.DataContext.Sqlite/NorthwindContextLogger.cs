using static System.Environment;
namespace Northwind.DataContext.Sqlite;

public class NorthwindContextLogger
{
    public static void WriteLine(string message)
    {
        string path = Path.Combine(GetFolderPath(SpecialFolder.DesktopDirectory), "Northwindlog.txt");

        StreamWriter textFile = File.AppendText(path);

        textFile.WriteLine(message);
        textFile.Close();
    }

}
