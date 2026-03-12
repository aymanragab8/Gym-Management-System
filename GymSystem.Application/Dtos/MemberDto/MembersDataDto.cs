using GymSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace GymSystem.Application.Dtos.MemberDto
{
    public class MembersDataDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string CoachName { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberType MemberType { get; set; }


    }
}
