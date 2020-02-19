using System.Linq;
using AutoMapper;
using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {


        public AutoMapperProfiles()
        {
            CreateMap<User, UserStatisticsDto>().ForMember(
                dest => dest.experience, opt => {
                    opt.MapFrom(src => src.userExperience.experience);
                }
            ).ForMember(
                dest => dest.level, opt => {
                    opt.MapFrom(src => src.userExperience.level);
                }
            ).ForMember(
                dest => dest.betsSended, opt => {
                    opt.MapFrom(src => src.lottoGame.betsSended);
                }
            ).ForMember(
                dest => dest.amountOfThree, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfThree);
                }
            ).ForMember(
                dest => dest.amountOfFour, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfFour);
                }
            ).ForMember(
                dest => dest.amountOfFive, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfFive);
                }
            ).ForMember(
                dest => dest.amountOfSix, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfSix);
                }
            );

            CreateMap<User, UserDetailsDto>().ForMember(
                dest => dest.experience, opt => {
                    opt.MapFrom(src => src.userExperience.experience);
                }
            )
            .ForMember(
                dest => dest.level, opt => {
                    opt.MapFrom(src => src.userExperience.level);
                }
            ).ForMember(
                dest => dest.betsSended, opt => {
                    opt.MapFrom(src => src.lottoGame.betsSended);
                }
            ).ForMember(
                dest => dest.amountOfThree, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfThree);
                }
            ).ForMember(
                dest => dest.amountOfFour, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfFour);
                }
            ).ForMember(
                dest => dest.amountOfFive, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfFive);
                }
            ).ForMember(
                dest => dest.amountOfSix, opt => {
                    opt.MapFrom(src => src.lottoGame.amountOfSix);
                }
            );
        }

    }
}