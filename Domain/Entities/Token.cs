using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public string TokenStr { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
