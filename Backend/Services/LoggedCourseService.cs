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
    public class LoggedCourseService : ILoggedCourseService
    {
        private readonly ILoggedCourseRepository _loggedCourseRepository;
        private readonly IMapper _mapper;

        // Constructor
        public LoggedCourseService(ILoggedCourseRepository loggedCourseRepository, IMapper mapper)
        {
            _loggedCourseRepository = loggedCourseRepository;
            _mapper = mapper;
        }

        // Methods
        public async Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalantCourseDto loggedEquivalantCourse)
        {
            var loggedEquivalantCourseEntity = _mapper.Map<LoggedEquivalantCourse>(loggedEquivalantCourse);

            // dont allow duplicates
            var loggedCourses = await _loggedCourseRepository.GetLoggedEquivalantCourses();
            foreach (var loggedCourse in loggedCourses)
            {
                if (loggedCourse.HostCourseCode == loggedEquivalantCourseEntity.HostCourseCode
                        && loggedCourse.ExemptedCourse.CourseCode == loggedEquivalantCourseEntity.ExemptedCourse.CourseCode)
                {
                    return false;
                }
            }

            return await _loggedCourseRepository.CreateLoggedEquivalantCourse(loggedEquivalantCourseEntity);
        }

        // This method will allow duplicates to show whether the same course has been transferred multiple times
        // for many different exchange periods
        public async Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse)
        {
            var loggedTransferredCourseEntity = _mapper.Map<LoggedTransferredCourse>(loggedTransferredCourse);
            return await _loggedCourseRepository.CreateLoggedTransferredCourse(loggedTransferredCourseEntity);
        }
        public async Task<bool> DeleteLoggedEquivalantCourse(Guid id)
        {
            return await _loggedCourseRepository.DeleteLoggedEquivalantCourse(id);
        }
        public async Task<bool> DeleteLoggedTransferredCourse(Guid id)
        {
            return await _loggedCourseRepository.DeleteLoggedTransferredCourse(id);
        }
        public async Task<LoggedEquivalantCourseDto> GetLoggedEquivalantCourse(Guid id)
        {
            var loggedEquivalantCourse = await _loggedCourseRepository.GetLoggedEquivalantCourse(id);
            return _mapper.Map<LoggedEquivalantCourseDto>(loggedEquivalantCourse);
        }
        public async Task<IEnumerable<LoggedEquivalantCourseDto>> GetLoggedEquivalantCourses()
        {
            var loggedEquivalantCourses = await _loggedCourseRepository.GetLoggedEquivalantCourses();
            return _mapper.Map<IEnumerable<LoggedEquivalantCourseDto>>(loggedEquivalantCourses);
        }
        public async Task<LoggedTransferredCourseDto> GetLoggedTransferredCourse(Guid id)
        {
            var loggedTransferredCourse = await _loggedCourseRepository.GetLoggedTransferredCourse(id);
            return _mapper.Map<LoggedTransferredCourseDto>(loggedTransferredCourse);
        }
        public async Task<IEnumerable<LoggedTransferredCourseDto>> GetLoggedTransferredCourses()
        {
            var loggedTransferredCourses = await _loggedCourseRepository.GetLoggedTransferredCourses();
            return _mapper.Map<IEnumerable<LoggedTransferredCourseDto>>(loggedTransferredCourses);
        }

        public async Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalantCourseDto loggedEquivalantCourse)
        {
            return await _loggedCourseRepository.UpdateLoggedEquivalantCourse(_mapper.Map<LoggedEquivalantCourse>(loggedEquivalantCourse));
        }

        public async Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse)
        {
            return await _loggedCourseRepository.UpdateLoggedTransferredCourse(_mapper.Map<LoggedTransferredCourse>(loggedTransferredCourse));
        }
    }
}
