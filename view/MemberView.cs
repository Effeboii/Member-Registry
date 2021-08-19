using System;
using System.Collections.Generic;
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
      Console.Clear();
      Console.WriteLine("=========================");
      Console.WriteLine("Show list of all members");
      Console.WriteLine("=========================");
      Console.WriteLine("Choose a number!");
      Console.WriteLine($"1. List a specific member");
      Console.WriteLine($"2. {ListType.Compact} list");
      Console.WriteLine($"3. {ListType.Verbose} list");
      Console.WriteLine("4. Return to main menu");

      int menuChoice = Convert.ToInt32(Console.ReadLine());

        switch (menuChoice)
      {
        case 1:
          DisplayMember();
          Console.WriteLine("Press any button to go back to main menu");
          Console.ReadKey(true);
          break;
        case 2:
          DisplayAllMembers(ListType.Compact);
          Console.WriteLine("Press any button to go back to main menu");
          Console.ReadKey(true);
          break;
        case 3:
          DisplayAllMembers(ListType.Verbose);
          Console.WriteLine("Press any button to go back to main menu");
          Console.ReadKey(true);
          break;
        default:
          break;
      }
    }

    public void DisplayMember()
    {
      int id = EnterMemberID();
      Console.WriteLine(memberModel.DisplayMember(id).ToString(ListType.Verbose));
    }

    public void DisplayAllMembers(ListType listType)
    {
      List<MemberModel> memberList = memberModel.DisplayAllMembers();
      foreach (var member in memberList)
      {
        Console.WriteLine(member.ToString(listType));
      }
    }

    public int EnterMemberID()
    {
      int id = 0;
      try
      {
        do
        {
          Console.WriteLine("=========================");
          Console.WriteLine("Enter member ID.");
          id = Convert.ToInt32(Console.ReadLine());
        }
        while (id.ToString().Length < 0);

        return id; 
      }
      catch 
      {
        throw new ErrorWhileEnteringMemberId();
      }
    }

    public void RegisterMember()
    {
      string name;
      string ssn;
      bool loop = true;
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

        if (memberModel.checkSSN(ssn))
        {
          loop = false;
        }
      }
      while (loop == true);

      try
      {
        memberModel.CreateMember(name, ssn);
        System.Console.WriteLine("Member was added successfully");
        System.Console.WriteLine("Press any button to go back to main menu");
        Console.ReadKey(true);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw new ErrorWhileCreatingMember();
      }
    }

    public void EditMember()
    {
      int id = EnterMemberID();
      Console.Clear();

      try
      {
        List<MemberModel> dataFromDatabase = memberModel.getDataFromDatabase();
        foreach (var member in dataFromDatabase)
        {
          if (member.ID == id)
          {
            Console.WriteLine($"Current name: {member.Name}");
            string newName = "";

            while (newName.Length <= 0)
            {
              Console.WriteLine("Enter new name: ");
              newName = Console.ReadLine(); 
            }

            Console.WriteLine($"Current SSN: {member.SSN}");
            member.Name = newName;
            Console.WriteLine("Enter new SSN: ");
            string newSSN = Console.ReadLine();

            while (!memberModel.checkSSN(newSSN))
            {
              Console.WriteLine("Enter new SSN: ");
              newSSN = Console.ReadLine();
            }
            member.SSN = newSSN;
          }
        }
        memberModel.writeMemberToDatabase(dataFromDatabase);
        Console.WriteLine("Member information was changed");
        Console.WriteLine("Press any button to go back to main menu");
        Console.ReadKey(true);
      }
      catch
      {
        throw new ErrorWhileEditingMember();
      }
    }

    public void DeleteMember()
    {
      Console.Clear();
      int id = EnterMemberID();
      MemberModel uniqueMember = new MemberModel();

      try
      {
        uniqueMember = memberModel.findMemberInDb(id);
      }
      catch
      {
        throw new Exception("Error while deleting member");
      }

      Console.WriteLine("=========================");
      Console.WriteLine($"Do you want to delete {uniqueMember.Name}?");
      Console.WriteLine("1. Yes");
      Console.WriteLine("2. No");

      switch (Console.ReadLine())
      {
        case "1":
          memberModel.removeMember(id);
          System.Console.WriteLine("Member was removed");
          System.Console.WriteLine("Press any button to go back to main menu");
          Console.ReadKey(true);
          break;
        default:
          break;
      }
    }
  }
}