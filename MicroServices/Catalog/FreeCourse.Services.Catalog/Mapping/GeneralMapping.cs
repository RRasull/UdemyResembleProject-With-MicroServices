﻿using AutoMapper;
using FreeCourse.Services.Catalog.DTOs;
using FreeCourse.Services.Catalog.Entities;

namespace FreeCourse.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();


            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();

        }
    }
}