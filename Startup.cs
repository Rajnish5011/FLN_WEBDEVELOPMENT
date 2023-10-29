using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ASPNetCoreFLN_APIs.Context;
using ASPNetCoreFLN_APIs.Contracts;
using ASPNetCoreFLN_APIs.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using AutoMapper;

namespace ASPNetCoreFLN_APIs
{   public class Startup
    {        
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.ValueCountLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue;
                x.MultipartBoundaryLengthLimit = int.MaxValue;
                x.BufferBodyLengthLimit = long.MaxValue;
                x.BufferBody = true;
                x.MemoryBufferThreshold = int.MaxValue;
                x.KeyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
                x.MultipartHeadersCountLimit = int.MaxValue;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {                         
                                      policy.WithOrigins("https://flnstagingapi.weexceldemo.com", "https://api.nipunharyana.in", "https://nipunharyana.in",
                                          "http://localhost:3000",
                                                          "https://dashboard.nipunharyana.in")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                                  });
            });


            services.AddControllers();

            services.AddMemoryCache();
            services.AddSingleton<DapperContext>();
            services.AddScoped<IMastersRepository, MastersRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();                   
            services.AddScoped<IPeriodicAssessmentRepository, PeriodicAssessmentRepository>();
            services.AddScoped<IMentorSchoolScheduleRepository, MentorSchoolScheduleRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IMentorSchoolScheduleVisitRepository, MentorSchoolScheduleVisitRepository>();
            services.AddScoped<IMentorClassObservationRepository, MentorClassObervationRepository>();
            services.AddScoped<IMentorSchoolScheduleStartRepository, MentorSchoolScheduleStartRepository>();
            services.AddScoped<ISpotAssessmentRepository, SpotAssessmentRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ITeachersMeetingRepository, TeachersMeetingRepository>();
            services.AddScoped<IManageGrievanceRepository, ManageGrievanceRepository>();
            services.AddScoped<ITeachersTrainingRepository, TeachersTrainingRepository>();
            services.AddScoped<IMockAssessmentRepository, MockAssessmentRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IManageTargetsRepository, ManageTargetsRepository>();
            services.AddScoped<IStudentTeacherCommunicationRepository, StudentTeacherCommunicationRepository>();
            services.AddScoped<IMentorReportRepository, MentorReportRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "FLN APIs", Version = "v1" }));

            #region AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Helper.AutoMapperProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FLN APIs V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();            
                        
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
