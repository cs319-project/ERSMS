using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utilities.Enum;

namespace Backend.Interfaces
{
    /// <summary>An interface for the notification service.</summary>
    public interface INotificationService
    {
        Task<bool> CreateNewFormNotification(Form formObject, FormType formType);
        Task<bool> CreateNewApprovalNotification(Form formObject, FormType formType, bool isApproved, string approverName);
        Task<bool> MarkAsRead(Guid id);
        Task<bool> MarkAllAsRead(string userId);
        Task<List<NotificationDto>> GetNotifications(string userId, bool unread = false);
    }
}
