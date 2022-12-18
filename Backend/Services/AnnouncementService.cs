using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;

namespace Backend.Services
{
    /// <summary>A service for announcement operations.</summary>
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="AnnouncementService"/> class.</summary>
        /// <param name="announcementRepository">The announcement repository.</param>
        /// <param name="mapper">The mapper.</param>
        public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        /// <summary>Adds an announcement to the database.</summary>
        /// <param name="announcement">The announcement to add.</param>
        /// <returns>Whether the announcement was added successfully.</returns>
        public async Task<bool> AddAnnouncement(AnnouncementDto announcement)
        {
            var announcementEntity = _mapper.Map<Announcement>(announcement);
            return await _announcementRepository.AddAnnouncement(announcementEntity);
        }

        /// <summary>Gets the announcements.</summary>
        /// <returns>The announcements.</returns>
        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncements()
        {
            var announcements = await _announcementRepository.GetAnnouncements();
            return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        }

        /// <summary>Deletes an announcement.</summary>
        /// <param name="id">The ID of the announcement to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> DeleteAnnouncement(Guid id)
        {
            return await _announcementRepository.DeleteAnnouncement(id);
        }

        /// <summary>Gets an announcement.</summary>
        /// <param name="id">The announcement's ID.</param>
        /// <returns>The announcement.</returns>
        public async Task<AnnouncementDto> GetAnnouncement(Guid id)
        {
            var announcement = await _announcementRepository.GetAnnouncement(id);
            return _mapper.Map<AnnouncementDto>(announcement);
        }

        /// <summary>Updates an announcement.</summary>
        /// <param name="announcement">The announcement to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> UpdateAnnouncement(AnnouncementDto announcement)
        {
            var announcementEntity = _mapper.Map<Announcement>(announcement);
            return await _announcementRepository.UpdateAnnouncement(announcementEntity);
        }
    }
}
