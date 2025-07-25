
using System.ComponentModel.DataAnnotations;

namespace FolderWatcherApp.Models
{
    public class Recipient
    {
        [Key]
        public int Id { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public int SettingId { get; set; }
        public Setting? Setting { get; set; }
    }
}
