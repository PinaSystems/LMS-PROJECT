using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OAWA.Data.Dtos;
using OAWA.Data.Enums;
using OAWA.Data.Helpers;

namespace OAWA.Data
{
    public class NuggetRepository: DataRepository, INuggetRepository
    {
        private readonly IMapper _mapper;
        public NuggetRepository(DataContext context, IMapper mapper):base(context)
        {
            _mapper= mapper;
        }

        public async Task<PagedList<NuggetDto>> GetNuggets(NuggetParams nuggetParams)
        {
            var nuggets= await _context.Nuggets
                                            .Include(item => item.Lesson)
                                            .Where(item => item.ClassDate<=DateTime.UtcNow.ToLocalTime() &&
                                                            item.NuggetStatus==NuggetStatus.Enabled)
                                            .ToListAsync();
            var nuggetForViewDto= new List<NuggetDto>();
            _mapper.Map(nuggets,nuggetForViewDto);
            return await PagedList<NuggetDto>.CreateAsyncWithDtos(nuggetForViewDto.AsQueryable(), nuggetParams.Page, nuggetParams.Per_Page);
        }
    }
}