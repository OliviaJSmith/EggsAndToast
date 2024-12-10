using IntelliTect.Coalesce.AuditLogging;

namespace EggsAndToast.Data.Models;

[Edit(DenyAll)]
[Delete(DenyAll)]
[Create(DenyAll)]
[Read(nameof(Permission.ViewAuditLogs))]
public class AuditLog : DefaultAuditLog
{
    public string? UserId { get; set; }

    [Display(Name = "Changed By")]
    public User? User { get; set; }

}