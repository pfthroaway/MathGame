using System;
using System.IO;

namespace MathGame
{
    public static class AppData
    {
        internal static string Location = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MathGame");
    }
}