using System.Runtime.Serialization;

namespace api.Models
{
    public enum Status
    {
        [EnumMember(Value = "Active")] Active,
        [EnumMember(Value = "Completed")] Completed,
        [EnumMember(Value = "Overdue")] Overdue,
        [EnumMember(Value = "Late")] Late,
    }
}