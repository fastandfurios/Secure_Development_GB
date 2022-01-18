using System.Text.Json.Serialization;

namespace Debit_Cards_No_EF_Project.DAL.Models
{
    public sealed class Holder
    {
        public string FirstName { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
        public string LastName { get; set; }
    }
}
