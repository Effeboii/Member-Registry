using System;
using Model;

namespace View
{
  class MemberView
  {
    MemberModel memberModel = new MemberModel();
    public void ManageMembers()
    {
      Console.Clear();
      Console.WriteLine("==========================");
      Console.WriteLine("   -- Manage members --");
      Console.WriteLine("==========================");
      Console.WriteLine("What can I help you with today?");
      Console.WriteLine("1. Register a new member");
      Console.WriteLine("2. Edit member");
      Console.WriteLine("3. Delete member");
      Console.WriteLine("4. Return to main menu");

      int menuChoice = Convert.ToInt32(Console.ReadLine());

      switch (menuChoice)
      {
        case 1:
          Console.WriteLine("Option 1");
          RegisterMember();
          break;
        case 2:
          Console.WriteLine("Option 2");
          break;
        case 3:
          Console.WriteLine("Option 3");
          break;
        default:
          break;
      }
    }

    public void ListMemberInfo() 
    {
    }

    public void DisplayMember()
    {
    }

    public void DisplayAllMembers()
    {
    }

    public int EnterMemberID()
    {
    }


    public void RegisterMember()
    {
      string name;
      string ssn;
      bool continueDoWhile = true;
      Console.Clear();

      do
      {
        Console.WriteLine("=========================");
        Console.WriteLine("Please enter full name");
        name = Console.ReadLine();
      }
      while (name == "");

      do
      {
        Console.WriteLine("=========================");
        Console.WriteLine("Please enter social security number (format: YYMMDDNNNN)");
        ssn = Console.ReadLine();
      }
      while (ssn == "");

      try
      {
        memberModel.StoreMember(name, ssn);
        System.Console.WriteLine("Member was added successfully");
        System.Console.WriteLine("Press any button to go back to main menu");
        Console.ReadKey(true);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }

    public void EditMember()
    {
    }

    public void DeleteMember()
    {
    }
  }
}