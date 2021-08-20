using System;

namespace View
{
  class MenuView
  {
    BoatView boatView = new BoatView();
    MemberView memberView = new MemberView();

    public void Start()
    {
      try
      {
        while (true)
        {
          Console.Clear();
          Console.WriteLine("================================================");
          Console.WriteLine("                -- Welcome to --                ");
          Console.WriteLine("      \"The Jolly Pirate Management System\"    ");
          Console.WriteLine("================================================");
          Console.WriteLine("What can I help you with today?");
          Console.WriteLine("1: Manage boats");
          Console.WriteLine("2: Manage members");
          Console.WriteLine("3: List member information");
          Console.WriteLine("4: Exit");

          int menuChoice = Convert.ToInt32(Console.ReadLine());

          switch (menuChoice)
          {
            case 1:
              boatView.ManageBoats();
              break;
            case 2:
              memberView.ManageMembers();
              break;
            case 3:
              memberView.ListMemberInfo();
              break;
            case 4:
              Console.WriteLine("================================================");
              Console.WriteLine("Thank you for choosing \"The Jolly Pirate Management System\".");
              Console.WriteLine("Console terminates...");
              Environment.Exit(0);
              break;
            default:
              break;
          }
        }
      }
      catch (Exception e)
      {               
        Console.WriteLine("ERROR: " + e);
        Start();
      }
    }
  }
}