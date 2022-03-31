using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Services
{
    public class FavoriteForumService : IFavoriteForumService
    {
        private readonly ICoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoriteForumService(ICoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}