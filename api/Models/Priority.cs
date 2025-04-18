using System.Runtime.Serialization;

namespace api.Models
{
    public enum Priority
    {
        [EnumMember(Value = "Low")] Low,
        [EnumMember(Value = "Medium")] Medium,
        [EnumMember(Value = "High")] High,
        [EnumMember(Value = "Critical")] Critical,
    }
}