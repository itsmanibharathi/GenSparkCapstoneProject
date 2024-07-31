﻿using api.Models.Dtos.PropertyDtos;

namespace api.Services.Interfaces
{
    public interface IPropertyService
    {
        public Task<ReturnPropertyDto> GetAsync(int id);
        public Task<ReturnPropertyDto> CreateAsync(CreatePropertyDto getProperityDto);
        public Task<ReturnPropertyDto> UpdateAsync(EditPropertyDto editPropertyDto);
    }
}
