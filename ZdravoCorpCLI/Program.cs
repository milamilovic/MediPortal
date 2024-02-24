using ZdravoCorpCLI;

public class Program
{
    public static void Main(String[] args)
    {
        while (true)
        {
            Console.WriteLine("Click 1 to renovate rooms or x to exit: ");
            string choice;
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RoomRenovationCLI.RoomRenovation();
                    break;
                case "x":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Input not recognized, please try again");
                    break;
            }
        }
    }
}