namespace asp.netCoreWebApi.logging
{
    public class Logging: ILogging
    {
        public void LogInformation(string message, string type)
        {
            if(type == "error")
            {
                Console.WriteLine("Error  -  " +  message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
