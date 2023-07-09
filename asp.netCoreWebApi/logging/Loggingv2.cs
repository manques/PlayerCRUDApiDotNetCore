namespace asp.netCoreWebApi.logging
{
    public class Loggingv2: ILogging
    {
        public void LogInformation(string message, string type)
        {
            switch (type)
            {
                case "error":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(type + " - " + message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "warning":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(type + " - " + message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "success":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(type + " - " + message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                default:
                    Console.WriteLine(message);
                    break;
            }
        }
    }
}
