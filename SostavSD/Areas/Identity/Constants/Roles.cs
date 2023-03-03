using System.Data;

namespace SostavSD.Areas.Identity.Constants;

public static class Roles
{
    public static List<string> AllRoles
    {
        get
        {
            return new List<string> {
                ReadOnly,
                Chief,
                HeadOfGroup,
                Estimator,
                Calculator,
                Secretary,
                Inspector,
            };
        }
    }

    public static string SeveralRoles(params string[] roles) => string.Join( ",", roles);

    /// <summary>
    /// Default, readOnly
    /// </summary>
    public const string ReadOnly = "ReadOnly";

    /// <summary>
    /// Head of Department
    /// </summary>
    public const string Chief = "Chief";

    /// <summary>
    /// Head of group
    /// </summary>
    public const string HeadOfGroup = "HeadOfGroup";

    /// <summary>
    /// Estimator
    /// </summary>
    public const string Estimator = "Estimator";

    /// <summary>
    /// Calculator
    /// </summary>
    public const string Calculator = "Calculator";

    /// <summary>
    /// Secretary
    /// </summary>
    public const string Secretary = "Secretary";

    /// <summary>
    /// Inspector
    /// </summary>
    public const string Inspector = "Inspector";
}
