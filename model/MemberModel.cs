using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
  public class MemberModel
  {
    DatabaseModel database = new DatabaseModel();
    public List<BoatModel> boatList = new List<BoatModel>();
    private int _memberID;
    private string _fullName;
    private string _socialSecurityNumber;

    public int MemberID
    {
      get { return _memberID; }
      set { _memberID = value; }
    }

    public string FullName
    {
      get { return _fullName; }
      set { _fullName = value; }
    }

    public string SocialSecurityNumber
    {
      get { return _socialSecurityNumber; }
      set { _socialSecurityNumber = value; }
    }

    public int GetMemberWithHighestID() 
    {
      int highestNumber = 0;

      foreach (var member in database.ReadFromDatabase())
      {
        if (highestNumber < member.MemberID)
        {
          highestNumber = member.MemberID;
        } 
      }

      return highestNumber + 1;
    }

    public bool CheckSSN (string ssn) 
    {
      if (Regex.IsMatch(ssn, @"^\d{6}\d{4}$"))
      {
        if (CheckIfUserExistWithSSN(ssn))
        {
          return false;
        }
        else
        {
          return true;
        }
      } 
      else
      {
        return false;
      }
    }

    public bool CheckIfUserExistWithSSN(string ssn)
    {
      bool memberFound = false;

      if (database.ReadFromDatabase().ToString().Length == 0)
      {
        return memberFound;
      }

      return memberFound;    
    }

    public bool CheckIfUserExistWithId(int id)
    {
      if (FindMemberInDatabase(id).FullName.Length > 0)
      {
        return true;
      }
      else
      {
        return false;    
      }
    }

    public MemberModel DisplayMember(int id)
    {
      return FindMemberInDatabase(id);
    }

    public List<MemberModel> DisplayAllMembers()
    {
      return database.ReadFromDatabase();
    }

    public MemberModel FindMemberInDatabase(int id)
    {
      MemberModel memberFromDb = new MemberModel();
      try
      {
        foreach (var member in database.ReadFromDatabase())
        {
          if (member.MemberID == id)
          {
            memberFromDb = member;
          }
        }
        return memberFromDb;
      }
      catch
      {
        throw new ErrorWhileSearchingForMember();
      }
    }

    public void CreateMember(string name, string ssn)
    {
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();
      var newMember = new MemberModel
      {
        MemberID = GetMemberWithHighestID(),
        FullName = name,
        SocialSecurityNumber = ssn
      };

      dataFromDatabase.Add(newMember);
      database.WriteToDatabase(dataFromDatabase);
    }

    public void DeleteMember(int id)
    {
      List<MemberModel> dataFromDatabase = database.ReadFromDatabase();
      MemberModel memberToBeRemoved = new MemberModel();

      foreach (var member in dataFromDatabase)
      {
        if (member.MemberID == id)
        {
          memberToBeRemoved = member;
        }
      }

      dataFromDatabase.Remove(memberToBeRemoved);
      database.WriteToDatabase(dataFromDatabase);
    }

    public List<MemberModel> GetDataFromDatabase()
    {
      return database.ReadFromDatabase();
    }

    public void WriteMemberToDatabase(List<MemberModel> editedMember)
    {
      database.WriteToDatabase(editedMember);
    }

    public string ToString(ListType listType)
    {
      string returnString = "===================-Member-======================\n";

      if (listType == ListType.Compact)
      {
        returnString += $"ID: {MemberID}, Name: {FullName}, SSN: {SocialSecurityNumber}, Number of boats: {boatList.Count}\n";
      } 
      else if (listType == ListType.Verbose)
      {
        returnString += $"ID: {MemberID}\nName: {FullName}\nSSN: {SocialSecurityNumber}\n";
        returnString += "---------------------Boats----------------------\n";
        
        foreach (var boat in boatList)
        {
          returnString += $"Boat ID: {boat.ID}, Boat type: {boat.Type}, Boat length: {boat.Length}m\n";
        }
        returnString += "------------------------------------------------\n";
      }
      return returnString;
    }
  }
}