namespace AoSP.Enums
{
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,

        GroupNotFound = 10,
        GroupAlreadyExist = 11,
        
        Ok = 200,
        InternalServerError = 500
    }
}