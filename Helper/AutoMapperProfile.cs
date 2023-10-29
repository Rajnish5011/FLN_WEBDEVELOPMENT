using ASPNetCoreFLN_APIs.Dto.ObservationQuestion;
using ASPNetCoreFLN_APIs.Entities;
using AutoMapper;
using System.Linq;

namespace ASPNetCoreFLN_APIs.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<ObservationQuestionScopeMaster, ObservationQuestionDto>().ReverseMap();
            //CreateMap<ObservationQuestionScopeMaster, ObservationQuestionDto>()
            //.ForMember(dest => dest.Question_Scope_Id, opt => opt.MapFrom(src => src.Question_Scope_Id))
            //.ForMember(dest => dest.Question_Scope, opt => opt.MapFrom(src => src.Question_Scope))
            //.ForMember(dest => dest.observationQuestionMasters, opt => opt.MapFrom(src => src.ObservationQuestionMasters))
            //.ForMember(dest => dest.observationQuestionOptions, opt => opt.MapFrom(src => src.ObservationQuestionMasters
            //    .SelectMany(q => q.ObservationQuestionOptions)))
            //.ForMember(dest => dest.Answers, opt => opt.Ignore())
            //.ForMember(dest => dest.AnswersId, opt => opt.Ignore());

            //CreateMap<ObservationQuestionMaster, ObservationQuestionMasterDto>()
            //    .ForMember(dest => dest.Observation_Question_Id, opt => opt.MapFrom(src => src.Observation_Question_Id))
            //    .ForMember(dest => dest.Observation_Question, opt => opt.MapFrom(src => src.Observation_Question))
            //    .ForMember(dest => dest.Question_Number, opt => opt.MapFrom(src => src.Question_Number))
            //    .ForMember(dest => dest.Dependent_Question_ID, opt => opt.MapFrom(src => src.Dependent_Question_ID))
            //    .ForMember(dest => dest.Dependent_Question_Option_Id, opt => opt.MapFrom(src => src.Dependent_Question_Option_Id))
                //.ForMember(dest => dest.Is_Dependent, opt => opt.MapFrom(src => src.Is_Dependent))
              //  .ForMember(dest => dest.Is_Question_Have_Alert, opt => opt.MapFrom(src => src.Is_Question_Have_Alert))
              //  .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            //CreateMap<ObservationQuestionOptions, ObservationQuestionOptionsDto>()
            //    .ForMember(dest => dest.Observation_Question_Option_Id, opt => opt.MapFrom(src => src.Observation_Question_Option_Id))
            //    .ForMember(dest => dest.Observation_Question_Option, opt => opt.MapFrom(src => src.Observation_Question_Option))
            //    .ForMember(dest => dest.Show_Alert_OnSelect, opt => opt.MapFrom(src => src.show_Alert_OnSelect));
        }
    }
}

