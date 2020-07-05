using AutoMapper;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.ViewModels;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMM.Library.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryAppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _unitOfWork.CategoryRepository.Add(category);

            await _unitOfWork.Commit();
        }

        public async Task Update(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            _unitOfWork.CategoryRepository.Update(category);

            await _unitOfWork.Commit();
        }

        public async Task Delete(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);

            _unitOfWork.CategoryRepository.Delete(category);

            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _unitOfWork.CategoryRepository.GetAll());
        }

        public async Task<CategoryViewModel> GetById(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _unitOfWork.CategoryRepository.GetById(id));
        }       
    }
}
