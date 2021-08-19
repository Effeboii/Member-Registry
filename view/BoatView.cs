using System;
using System.Collections.Generic;
using Model;

namespace View
{
  class BoatView
  {
    MemberModel memberModel = new MemberModel();
    BoatModel boatModel = new BoatModel();
    public void ManageBoats()
    {
      Console.Clear();
      Console.WriteLine("=========================");
      Console.WriteLine("   -- Manage boats --");
      Console.WriteLine("=========================");
      Console.WriteLine("What can I help you with today?");
      Console.WriteLine("1. Register new boat");
      Console.WriteLine("2. Edit boat");
      Console.WriteLine("3. Delete boat");
      Console.WriteLine("4. Return to main menu");

      int menuChoice = Convert.ToInt32(Console.ReadLine());

      switch (menuChoice)
      {
        case 1:
          Console.WriteLine("Option 1");
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

    private void DisplayAllMembers(ListType listType)
    {
      List<MemberModel> memberList = memberModel.DisplayAllMembers();
      foreach (var member in memberList)
      {
        Console.WriteLine(member.ToString(listType));
      }
    }

    public int CheckIfUserExistWithId()
    {
      int memberID;
      try
      {
        do
        {
          Console.WriteLine("=========================");
          Console.WriteLine("Enter your member ID.");
          memberID = Int32.Parse(Console.ReadLine());
        }
        while (!memberModel.CheckIfUserExistWithId(memberID));

        Console.WriteLine(memberID);
        return memberID;    
      }
      catch 
      {
        return 0;
      }
    }

    public int CheckIfBoatExist(int memberID)
    {
      int boatID;
      MemberModel memberFromDb = memberModel.DisplayMember(memberID);
      Console.Clear();
      foreach (BoatModel boat in memberFromDb.boatList)
      {
        Console.WriteLine($"ID: {boat.ID}, Boat type: {boat.Type}, boat length: {boat.Length}");
      }

      do
      {
        Console.WriteLine("===================");
        Console.WriteLine("Enter your boat ID.");
        try
        {
          boatID = Int32.Parse(Console.ReadLine());   
        }
        catch (System.Exception)
        {
          throw new ErrorWhileSearchingForBoat();
        }
      }
      while (!boatModel.checkIfBoatExistWithId(memberID, boatID));
      return boatID;
    }

    public int CheckBoatLength()
    {
      int length = 0;
      try
      {
        do
        {
          Console.Clear();
          Console.WriteLine("Enter length in meters, max 50");
          length = Convert.ToInt32(Console.ReadLine());
          if (length > 50)
          {
            length = 0;
          }
        }
        while (length < 1);
        return length;
      }
      catch
      {
        Console.WriteLine("Boat length must contain a number! Pressa any key to enter a new number");
        Console.ReadKey(true);
        CheckBoatLength();
        return 0;
      }
    }

    private BoatType SelectBoatType()
    {
      BoatType boatType = BoatType.Sailboat;
      bool continueLoop = true;
      int menuChoice;
      do
      {
        Console.Clear();
        Console.WriteLine("===================");
        Console.WriteLine("Select your boat type.");
        Console.WriteLine($"1. {BoatType.Sailboat}");
        Console.WriteLine($"2. {BoatType.Motorsailer}");
        Console.WriteLine($"3. {BoatType.Kayak}");
        Console.WriteLine($"4. {BoatType.Other}");

        menuChoice = Int32.Parse(Console.ReadLine());

        if (menuChoice == 1)
        {
          boatType = BoatType.Sailboat;
          continueLoop = false;
        }
        else if (menuChoice == 2)
        {
          boatType = BoatType.Motorsailer;
          continueLoop = false;
        }
        else if (menuChoice == 3)
        {
          boatType = BoatType.Kayak;
          continueLoop = false;
        }
        else if (menuChoice == 4)
        {
          boatType = BoatType.Other;
          continueLoop = false;
        }
        else
        {
          Console.WriteLine("Not a valid type");
        }
      }
      while (continueLoop);
      return boatType;
    }

    public void RegisterBoat()
    {
      int memberID = CheckIfUserExistWithId();
      BoatType type = BoatType.Sailboat;
      int menuChoice;
      bool continueLoop = true;

        if (memberID > 0)
        {
          do
          {
            Console.Clear();
            Console.WriteLine("===================");
            Console.WriteLine("Select your boat type.");
            Console.WriteLine($"1. {BoatType.Sailboat}");
            Console.WriteLine($"2. {BoatType.Motorsailer}");
            Console.WriteLine($"3. {BoatType.Kayak}");
            Console.WriteLine($"4. {BoatType.Other}");
            Console.WriteLine("5. Return to main menu");

            menuChoice = Int32.Parse(Console.ReadLine());

            if (menuChoice == 1)
            {
              type = BoatType.Sailboat;
              continueLoop = false;
            }
            else if (menuChoice == 2)
            {
              type = BoatType.Motorsailer;
              continueLoop = false;
            }
            else if (menuChoice == 3)
            {
              type = BoatType.Kayak;
              continueLoop = false;
            }
            else if (menuChoice == 4)
            {
              type = BoatType.Other;
              continueLoop = false;
            }
            else
            {
              Console.WriteLine("Not a valid type");
            }
          }
          while (continueLoop);

          int boatLength = CheckBoatLength();

          try
          {
            boatModel.addBoat(memberID, type, boatLength);
            Console.WriteLine("Boat was added");
            Console.WriteLine("Press any button to go back to main menu");
            Console.ReadKey(true);
          }
          catch
          {
            throw new ErrorWhileCreatingBoat();
          }
      }
      else
      {
        Console.WriteLine("Member does not exist, press any key to return to main menu");
        Console.ReadKey(true);
      }
    }

    public void EditBoat()
    {
      Console.Clear();
      int memberID = CheckIfUserExistWithId();
      if (memberModel.FindMemberInDatabase(memberID).boatList.Count > 0)
      {
        BoatType boatType = SelectBoatType();
        int boatID = CheckIfBoatExist(memberID);
        int boatLength = CheckBoatLength();
        
        try
        {
          boatModel.editBoat(memberID, boatID, boatType, boatLength);
          Console.WriteLine("Boat information was changed");
          Console.WriteLine("Press any button to go back to main menu");
          Console.ReadKey(true);     
        }
        catch
        {
          throw new ErrorWhileEditBoat();
        }
      }
      else
      {
        Console.WriteLine("Member has no boats");
        Console.ReadKey(true);     
        return;
      }
    }

    public void DeleteBoat()
    {
      Console.Clear();
      int memberID = CheckIfUserExistWithId();
      if(memberModel.FindMemberInDatabase(memberID).boatList.Count > 0)
      {
        int boatID = CheckIfBoatExist(memberID);
        int uniqueBoat = 0;
        try
        {
          uniqueBoat = boatModel.searchUniqueBoat(memberID, boatID);
          Console.WriteLine(uniqueBoat.GetType());
        
        }
        catch
        {
          throw new ErrorWhileSearchingForBoat();
        }

        Console.WriteLine("=========================");
        Console.WriteLine($"Do you want to delete boat ID {uniqueBoat}?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");

        switch (Console.ReadLine())
        {
          case "1":
            try
            {
              boatModel.deleteBoat(memberID, boatID);
              Console.WriteLine("Boat was removed");
              Console.WriteLine("Press any button to go back to main menu");
              Console.ReadKey(true);
            }
            catch (ErrorWhileDeletingBoat e)
            {
              Console.WriteLine(e);
              throw new ErrorWhileDeletingBoat();
            }
            break;
          default:
            break;
        }
      }
      else
      {
        Console.WriteLine("Member does not own any boats");
      }
    }
  }
}