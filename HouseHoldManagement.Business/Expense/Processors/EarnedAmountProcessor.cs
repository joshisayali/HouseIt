using HouseHoldManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Business.Expense
{
    public class EarnedAmountProcessor
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public List<GetEarnedAmountViewModel> GetEarnedAmount()
        {
            var items = unitOfWork.EarnedAmountRepository.Get(orderBy: q => q.OrderBy(i => i.EarnedAmountDate));

            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EarnedAmount, GetEarnedAmountViewModel>();                
            });
            var mapper = mapperConfig.CreateMapper();
            List<GetEarnedAmountViewModel> earnedAmounts = mapper.Map<List<GetEarnedAmountViewModel>>(items.ToList());

            return earnedAmounts;

        }

        public void CreateEarnedAmount(CreateEarnedAmountViewModel earnedAmount)
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateEarnedAmountViewModel, EarnedAmount>();
            });
            var mapper = mapperConfig.CreateMapper();
            EarnedAmount item = mapper.Map<EarnedAmount>(earnedAmount);
            unitOfWork.EarnedAmountRepository.Insert(item);
            unitOfWork.Save();
        }

        public GetEarnedAmountViewModel UpdateEarnedAmount(GetEarnedAmountViewModel earnedAmount)
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetEarnedAmountViewModel, EarnedAmount>();
                cfg.CreateMap<EarnedAmount, GetEarnedAmountViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();
            EarnedAmount item = mapper.Map<EarnedAmount>(earnedAmount);
            unitOfWork.EarnedAmountRepository.Update(item);
            unitOfWork.Save();

            EarnedAmount updatedItem = unitOfWork.EarnedAmountRepository.GetById(earnedAmount.EarnedAmountId);
            GetEarnedAmountViewModel updatedEarnedAmount = mapper.Map<GetEarnedAmountViewModel>(updatedItem);

            return updatedEarnedAmount;

        }

        public void DeleteEarnedAmount(int id)
        {
            unitOfWork.EarnedAmountRepository.Delete(id);
            unitOfWork.Save();
        }
    }
}
