using HouseHoldManagement.Business.Shared;
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

        public List<GetEarnedAmountViewModel> GetEarnedAmount(string sortOrder, FilterResultViewModel filter)
        {
            IEnumerable<EarnedAmount> items;
            if (filter != null && !filter.IsNullorEmpty)
            {
                items = unitOfWork.EarnedAmountRepository.Get(filter: f => f.EarnedAmountDate >= filter.FromDate && f.EarnedAmountDate <= filter.ToDate);
            }
            else
            {
                items = unitOfWork.EarnedAmountRepository.Get();
            }

            switch(sortOrder)
            {
                case "Date_Desc":
                    items = items.OrderByDescending(i => i.EarnedAmountDate);
                    break;
                case "Source":
                    items = items.OrderBy(i => i.EarnedAmountSource);
                    break;
                case "Source_Desc":
                    items = items.OrderByDescending(i => i.EarnedAmountSource);
                    break;
                case "Amount":
                    items = items.OrderBy(i => i.AmountEarned);
                    break;
                case "Amount_Desc":
                    items = items.OrderByDescending(i => i.AmountEarned);
                    break;
                default:
                    items = items.OrderBy(i => i.EarnedAmountDate);
                    break;
            }


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
