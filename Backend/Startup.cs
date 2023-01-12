using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Backend.Entities;
using Backend.Data;
using Backend.Interfaces;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Backend.DTOs;
using Newtonsoft.Json;

namespace Backend
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddDbContext<Backend.Data.DataContext>(options =>
            {
                if (Environment.GetEnvironmentVariable("ERSMS_USE_POSTGRESQL") == "1")
                {
                    options.UseNpgsql(_config.GetConnectionString("PostgreSQLConnection"));
                }
                else
                {
                    options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
                }
            });
            services.AddIdentity<AppUser, Role>().AddEntityFrameworkStores<Backend.Data.DataContext>().AddDefaultTokenProviders();
            services.AddScoped<IPasswordHasher<AppUser>, Backend.Utilities.BCryptPasswordHasher>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlacementRepository, PlacementRepository>();
            services.AddScoped<IPlacementService, PlacementService>();
            services.AddScoped<ICTEFormRepository, CTEFormRepository>();
            services.AddScoped<ICTEFormService, CTEFormService>();
            services.AddScoped<IPreApprovalFormRepository, PreApprovalFormRepository>();
            services.AddScoped<IPreApprovalFormService, PreApprovalFormService>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<IToDoItemService, ToDoItemService>();
            services.AddScoped<IEquivalenceRequestRepository, EquivalenceRequestRepository>();
            services.AddScoped<IEquivalenceRequestService, EquivalenceRequestService>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILoggedCourseService, LoggedCourseService>();
            services.AddScoped<ILoggedCourseRepository, LoggedCourseRepository>();
            //Automappper setup
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            services.AddHealthChecks();
            services.AddControllers();
            services.AddCors();
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //     });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/healthcheck");

            if (Environment.GetEnvironmentVariable("ERSMS_USE_POSTGRESQL") == "1")
            {
                System.AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IAuthenticationService>().CreateRoles().Wait();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "http://localhost:8000", "https://localhost:8001"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            using (var scope = app.ApplicationServices.CreateScope())
            {
                var authService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
                // var loggedCourseService = scope.ServiceProvider.GetRequiredService<ILoggedCourseService>();

                // loggedCourseService.CreateLoggedEquivalantCourse(new LoggedEquivalentCourseDto
                // {
                //     ExemptedCourse = new ExemptedCourseDto
                //     {
                //         BilkentCredits = 4,
                //         CourseCode = "CS342",
                //         CourseName = "Operating Systems",
                //         CourseType = "Mandatory Course",
                //         ECTS = 5
                //     },
                //     HostCourseCode = "1DT044",
                //     HostCourseName = "Operating Systems",
                //     HostCourseECTS = 4,
                //     HostSchool = "Uppsala University",
                // });

                // loggedCourseService.CreateLoggedEquivalantCourse(new LoggedEquivalentCourseDto
                // {
                //     ExemptedCourse = new ExemptedCourseDto
                //     {
                //         BilkentCredits = 4,
                //         CourseCode = "",
                //         CourseName = "",
                //         CourseType = "Technical Elective",
                //         ECTS = 3
                //     },
                //     HostCourseCode = "1TD388",
                //     HostCourseName = "Computer Graphics",
                //     HostCourseECTS = 4,
                //     HostSchool = "Uppsala University",
                // });

                // loggedCourseService.CreateLoggedEquivalantCourse(new LoggedEquivalentCourseDto
                // {
                //     ExemptedCourse = new ExemptedCourseDto
                //     {
                //         BilkentCredits = 3,
                //         CourseCode = "",
                //         CourseName = "",
                //         CourseType = "Technical Elective",
                //         ECTS = 3
                //     },
                //     HostCourseCode = "1TD388",
                //     HostCourseName = "Geometrical Product Specifications and Verification",
                //     HostCourseECTS = 3,
                //     HostSchool = "Technische Universiteit Eindhoven",
                // });

                // loggedCourseService.CreateLoggedEquivalantCourse(new LoggedEquivalentCourseDto
                // {
                //     ExemptedCourse = new ExemptedCourseDto
                //     {
                //         BilkentCredits = 4,
                //         CourseCode = "MAN 329",
                //         CourseName = "Integrated Finance",
                //         CourseType = "General Elective",
                //         ECTS = 3
                //     },
                //     HostCourseCode = "MAN 307",
                //     HostCourseName = "Integrated Financial and Operations Managment",
                //     HostCourseECTS = 3,
                //     HostSchool = "Technische Universiteit Eindhoven",
                // });


                if (!authService.UserExists("22002900", "oisep@bilkent.edu.tr").GetAwaiter().GetResult())
                {
                    authService.Register(new DTOs.RegisterDto
                    {
                        ActorType = "Office of International Students and Exchange Programs",
                        UserName = "22002900",
                        Email = "oisep@bilkent.edu.tr",
                        Password = "Test123_",
                        FirstName = "Yelda İrem",
                        LastName = "Ateş",
                    }
                    ).Wait();
                }

                if (!authService.UserExists("22002901", "instructor@bilkent.edu.tr").GetAwaiter().GetResult())
                {
                    authService.Register(new DTOs.RegisterDto
                    {
                        ActorType = "Course Coordinator Instructor",
                        UserName = "22002901",
                        Email = "instructor@bilkent.edu.tr",
                        Password = "Test123_",
                        FirstName = "Eray",
                        LastName = "Tüzün",
                        IsCourseCoordinator = true,
                        Course = new CourseDto
                        {
                            CourseCode = "CS319",
                            CourseName = "Object Oriented Software Engineering"
                        }
                    }).Wait();
                }

                if (!authService.UserExists("1", "admin@bilkent.edu.tr").GetAwaiter().GetResult())
                {
                    authService.Register(new DTOs.RegisterDto
                    {
                        ActorType = "Admin",
                        UserName = "1",
                        Email = "admin@bilkent.edu.tr",
                        Password = "Test123_",
                        FirstName = "Edward",
                        LastName = "Snowden"
                    }).Wait();
                }

                if (!authService.UserExists("22002902", "coordinator@bilkent.edu.tr").GetAwaiter().GetResult())
                {
                    authService.Register(new DTOs.RegisterDto
                    {
                        ActorType = "Exchange Coordinator",
                        UserName = "22002902",
                        Email = "coordinator@bilkent.edu.tr",
                        Password = "Test123_",
                        FirstName = "Can",
                        LastName = "Alkan",
                        Department = new DTOs.DepartmentInfoDto
                        {
                            DepartmentName = "Department of Computer Engineering",
                            FacultyName = "Faculty of Engineering",
                        }
                    }).Wait();
                }

                if (!authService.UserExists("22002903", "dean@bilkent.edu.tr").GetAwaiter().GetResult())
                {
                    authService.Register(new DTOs.RegisterDto
                    {
                        ActorType = "Dean Department Chair",
                        UserName = "22002903",
                        Email = "dean@bilkent.edu.tr",
                        Password = "Test123_",
                        FirstName = "Nail",
                        LastName = "Akar",
                        IsDean = true,
                        Department = new DTOs.DepartmentInfoDto
                        {
                            DepartmentName = "Department of Computer Engineering",
                            FacultyName = "Faculty of Engineering",
                        }
                    }).Wait();
                }

                if (!authService.UserExists("22002904", "chair@bilkent.edu.tr").GetAwaiter().GetResult())
                {
                    authService.Register(new DTOs.RegisterDto
                    {
                        ActorType = "Dean Department Chair",
                        UserName = "22002904",
                        Email = "chair@bilkent.edu.tr",
                        Password = "Test123_",
                        FirstName = "Selim",
                        LastName = "Aksoy",
                        IsDean = false,
                        Department = new DTOs.DepartmentInfoDto
                        {
                            DepartmentName = "Department of Computer Engineering",
                            FacultyName = "Faculty of Engineering",
                        }
                    }).Wait();
                }
            }
        }
    }
}
