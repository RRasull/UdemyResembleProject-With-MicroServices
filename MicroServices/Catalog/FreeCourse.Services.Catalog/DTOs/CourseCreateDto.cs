﻿using FreeCourse.Services.Catalog.Entities;

namespace FreeCourse.Services.Catalog.DTOs
{
    public class CourseCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public Feature Feature { get; set; }
        public string Picture { get; set; }
        public string CategoryId { get; set; }
    }
}
