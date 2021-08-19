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

  public class ErrorWhileSearchingForBoat : Exception
  {
    public ErrorWhileSearchingForBoat(){}
  }

    public class ErrorWhileCreatingBoat : Exception
  {
    public ErrorWhileCreatingBoat(){}
  }

    public class ErrorWhileEditBoat : Exception
  {
    public ErrorWhileEditBoat(){}
  }

    public class ErrorWhileDeletingBoat : Exception
  {
    public ErrorWhileDeletingBoat(){}
  }
}
