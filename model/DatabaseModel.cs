using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Model
{
  public class DatabaseModel
  {
    // ====================--Database--====================

    public List<MemberModel> ReadFromDatabase()
    {
      try
      {
        var json = System.IO.File.ReadAllText("data/Database.json");
        List<MemberModel> list = JsonConvert.DeserializeObject<List<MemberModel>>(json);
        return list;
      }
      catch 
      {
        throw new ErrorWhileGettingReadFromDatabase();
      }
    }

    public void WriteToDatabase(List<MemberModel> database) 
    {
      File.WriteAllText("data/Database.json", JsonConvert.SerializeObject(database));
    }

    // ======================--WIP--======================

    public List<MemberModel> GetDataFromDatabase()
    {
      return ReadFromDatabase();
    }

    public void WriteMemberToDatabase(List<MemberModel> editedMember)
    {
      WriteToDatabase(editedMember);
    }

    // =====================--Member--=====================

    private int GetMemberWithHighestID() 
    {
      List<MemberModel> storedMembers = ReadFromDatabase();
      int highestNumber = 0;

      if (storedMembers.Count > 0)
      {
        foreach (var member in storedMembers)
        {
          if (highestNumber < member.ID)
          {
            highestNumber = member.ID;
          } 
        }

        return highestNumber + 1;
      } 
      else
      {
        return 1;
      }
    }

    public bool CheckIfUserExistWithId(int id)
    {
      MemberModel member = SearchForMemberInDb(id);
      int memberID = member.ID;

      if (memberID == id)
      {
        return true;
      }

      return false;    
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
      bool foundMember = false;

      if (ReadFromDatabase().ToString().Length == 0)
      {
        return foundMember;
      }

      return foundMember;    
    }

    public MemberModel DisplayMember(int id)
    {
      return SearchForMemberInDb(id);
    }

    public List<MemberModel> DisplayAllMembers()
    {
      return ReadFromDatabase();
    }

    public MemberModel SearchForMemberInDb(int id)
    {
      try
      {
        foreach (var member in ReadFromDatabase())
        {
          if (member.ID == id)
          {
            return member;
          }
        }

        throw new ErrorWhileSearchingForMember();
      }
      catch
      {
        throw new ErrorWhileSearchingForMember();
      }
    }

    public void CreateMember(string name, string ssn)
    {
      List<MemberModel> database = ReadFromDatabase();
      var newMember = new MemberModel(GetMemberWithHighestID(), name, ssn);

      database.Add(newMember);
      WriteToDatabase(database);
    }

    public void DeleteMember(MemberModel member)
    {
      List<MemberModel> database = ReadFromDatabase();
      MemberModel memberToBeRemoved = null;

      foreach (var storedMember in database)
      {
        if (storedMember.ID == member.ID)
        {
          memberToBeRemoved = storedMember;
        }
      }

      database.Remove(memberToBeRemoved);
      WriteToDatabase(database);
    }

    // =====================--Boat--=====================

    public int GetBoatWithHighestID() 
    {
      int highestNumber = 0;

      foreach (var member in ReadFromDatabase())
      {
        foreach (var boat in member.Boats)
        {
          if (highestNumber < boat.ID)
          {
            highestNumber = boat.ID;
          } 
        }
      }

      return highestNumber + 1;
    }

    public bool CheckIfBoatExistWithId(MemberModel member, BoatModel boat)
    {            
      List<MemberModel> database = ReadFromDatabase();
      
      foreach (var storedMember in database)
      {
        if (storedMember.ID == member.ID)
        {
          foreach (var storedBoat in storedMember.Boats)
          {
            if (storedBoat.ID == boat.ID)
            {
              return true;
            }
          }
        }
      }

      return false;
    }

    public int SearchUniqueBoat(int memberID, int boatID)
    {            
      List<MemberModel> database = ReadFromDatabase();
      int boatName = 0;

      foreach (var member in database)
      {
        if (member.ID == memberID)
        {
          foreach (var boat in member.Boats)
          {
            if (boat.ID == boatID)
            {
              boatName = boat.ID;
            }
          }
        }
      }

      return boatName;
    }

    public void CreateBoat(MemberModel member, BoatModel boat)
    {
      List<MemberModel> database = ReadFromDatabase();

      foreach (var storedMember in database)
      {
        if (storedMember.ID == member.ID) 
        {
          storedMember.Boats.Add(boat);
        }
      }

      WriteToDatabase(database);
    }

    public void EditBoat(MemberModel member, BoatModel boat)
    {
      List<MemberModel> database = ReadFromDatabase();

      foreach (var storedMember in database)
      {
        if (storedMember.ID == member.ID)
        {
          foreach (var storedBoat in storedMember.Boats)
          {
            if (storedBoat.ID == boat.ID)
            {
              storedBoat.Type = boat.Type;
              storedBoat.Length = boat.Length;
            }
          }
        }
      }

      WriteToDatabase(database);
    }

    public void DeleteBoat(MemberModel member, BoatModel boat)
    {
      List<MemberModel> database = ReadFromDatabase();
      BoatModel boatToBeRemoved = new BoatModel();

      foreach (var storedMember in database)
      {
        if (storedMember.ID == member.ID)
        {
          foreach (var storedBoat in storedMember.Boats)
          {
            if (storedBoat.ID == boat.ID)
            {
              boatToBeRemoved = storedBoat;
            }
          }

          storedMember.Boats.Remove(boatToBeRemoved);
        }
      }

      WriteToDatabase(database);
    }
  }
}