using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        // Constructor
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // Endpoints
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
