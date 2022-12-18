using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>Controller for the notification API.</summary>
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        // Constructor

        /// <summary>Initializes a new instance of the <see cref="NotificationController"/> class.</summary>
        /// <param name="notificationService">The notification service.</param>
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // Endpoints

        /// <summary>Gets the notifications for the specified user.</summary>
        /// <param name="userId">The user's id.</param>
        /// <param name="unread">Whether to get only unread notifications.</param>
        /// <returns>The notifications for the specified user.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<NotificationDto>>> GetNotifications(string userId, bool unread = true)
        {
            var notifications = await _notificationService.GetNotifications(userId, unread);
            if (notifications == null || notifications.Count() == 0)
            {
                return NotFound();
            }

            return Ok(notifications);
        }

        /// <summary>Marks all notifications as read.</summary>
        /// <param name="id">The user ID.</param>
        /// <returns>An <see cref="ActionResult"/> indicating whether the operation succeeded.</returns>
        [HttpPatch("markAllAsRead/{id}")]
        public async Task<ActionResult> MarkAllAsRead(string id)
        {
            var result = await _notificationService.MarkAllAsRead(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>Marks a notification as read.</summary>
        /// <param name="id">The notification ID.</param>
        /// <returns>An <see cref="ActionResult"/> indicating success.</returns>
        [HttpPatch("markAsRead/{id:guid}")]
        public async Task<ActionResult> MarkAsRead(Guid id)
        {
            var result = await _notificationService.MarkAsRead(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
