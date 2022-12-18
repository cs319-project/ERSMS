using AutoMapper;

namespace Backend.Data.Profiles
{
    public class ExampleProfile : Profile
    {
        public ExampleProfile()
        {
            /* Creating mapping relations as follows
             *
             * CreateMap<LaserModes, LaserModesDTO>().ReverseMap();
             * CreateMap<Laser, LaserDTO>().ReverseMap();
             * 
             * fields with matching names are mapped automatically
             */

            /* 
             * Mapping using a custom mapper:
             * 
             * CreateMap<Platform, PlatformDTO>().ReverseMap()
             *   .ForMember(d => d.PlatformType,
             *   op => op.MapFrom(o => StringifyUtility.PlatformTypeEnumarator(o.PlatformType)))
             *   .ForMember(d => d.PlatformCategory,
             *   op => op.MapFrom(o => StringifyUtility.PlatformCategoryEnumarator(o.PlatformCategory)));
             */

            /*
             * Usage of mapper (using a IMapper object named mapper, initialized through automapper)
             * 
             * (laser = DTO object)
             * Laser _laser = mapper.Map<Laser>(laser);
             */
        }
    }
}

