namespace ASPnetWebApp.Enums;

public enum RoleType
{
    /// <summary>
    /// Unknown user
    /// </summary>
    Undefined = 0,
    /// <summary>
    /// Administrator
    /// </summary>
    Admin = 1,
    /// <summary>
    /// Default user
    /// </summary>
    User = 2
}
public static class Role
{
    public static RoleType FromString(string str)
    {
        foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
        {
            if (role.ToString() == str)
                return role;
        }

        return RoleType.Undefined;
    }
}