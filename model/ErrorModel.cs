using System;

namespace Model
{
  public class ErrorWhileGettingReadFromDatabase : Exception
  {
    public ErrorWhileGettingReadFromDatabase(){}
  }

  public class ErrorWhileEnteringMemberId : Exception
  {
    public ErrorWhileEnteringMemberId(){}
  }

  public class ErrorWhileCreatingMember : Exception
  {
    public ErrorWhileCreatingMember(){}
  }

  public class ErrorWhileEditingMember : Exception
  {
    public ErrorWhileEditingMember(){}
  }

  public class ErrorWhileSearchingForMember : Exception
  {
    public ErrorWhileSearchingForMember(){}
  }
}
