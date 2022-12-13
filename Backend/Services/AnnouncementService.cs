using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;

namespace Backend.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        // Constructor
        public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        // Methods
        public async Task<bool> AddAnnouncement(AnnouncementDto announcement)
        {
            var announcementEntity = _mapper.Map<Announcement>(announcement);
            return await _announcementRepository.AddAnnouncement(announcementEntity);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncements()
        {
            var announcements = await _announcementRepository.GetAnnouncements();
            return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        }

        public async Task<bool> DeleteAnnouncement(Guid id)
        {
            return await _announcementRepository.DeleteAnnouncement(id);
        }

        public async Task<AnnouncementDto> GetAnnouncement(Guid id)
        {
            var announcement = await _announcementRepository.GetAnnouncement(id);
            return _mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task<bool> UpdateAnnouncement(AnnouncementDto announcement)
        {
            var announcementEntity = _mapper.Map<Announcement>(announcement);
            return await _announcementRepository.UpdateAnnouncement(announcementEntity);
        }
    }
}
