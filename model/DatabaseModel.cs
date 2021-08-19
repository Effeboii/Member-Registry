using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Model
{
  public class DatabaseModel
  {   
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

    public void readMemberListFile()
    {
      throw new System.NotImplementedException();
    }
  }
}
