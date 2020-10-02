//using Domain.DataLayer.Shared;
//using Domain.Model;
//using Domain.Service.Model.Shared;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Service.Shared
//{
//    public abstract class BaseService<TRepository> : IBaseService where TRepository : IGenericRepository
//    {
//        private readonly IGenericRepository _genericRepository;
//        protected BaseService(IGenericRepository genericRepository)
//        {
//            _genericRepository = genericRepository;
//        }
//        public async Task<Guid> CreateAsync<T>(T entity) where T : EntityBase
//        {
//            _genericRepository.Add<T>(entity);
//            await _genericRepository.CommitAsync();
//            return entity.Id;
//        }
//        public void Dispose()
//        {
//            _genericRepository.Dispose();
//        }

//        public async Task PassivateAsync<T>(Guid Id) where T : EntityBase
//        {
//            await _genericRepository.PassivateEntityAsync<T>(Id);
//            await Task.CompletedTask;
//        }

//        public async Task<Guid> UpdateAsync<T>(T entity) where T : EntityBase
//        {
//            _genericRepository.Update<T>(entity);
//            await _genericRepository.CommitAsync();
//            return entity.Id;
//        }
//    }
//}
