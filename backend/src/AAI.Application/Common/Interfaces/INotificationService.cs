namespace AAI.Application.Common.Interfaces;

/// <summary>
/// Interface para serviço de notificações em tempo real
/// </summary>
public interface INotificationService
{
    Task SendToUserAsync(Guid userId, string message, CancellationToken cancellationToken = default);
    Task SendAlertAsync(Guid userId, AlertNotification alert, CancellationToken cancellationToken = default);
    Task BroadcastAsync(string message, CancellationToken cancellationToken = default);
}

public record AlertNotification(
    string Title,
    string Message,
    string Type,
    DateTime Timestamp,
    Dictionary<string, object>? Metadata = null
);
