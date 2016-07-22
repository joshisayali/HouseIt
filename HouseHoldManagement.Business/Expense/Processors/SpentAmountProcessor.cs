using HouseHoldManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseHoldManagement.Utilities;
using HouseHoldManagement.Business.Shared;

namespace HouseHoldManagement.Business.Expense
{
    public class SpentAmountProcessor
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        
        public List<SpentAmountViewModel> GetSpentAmount(string sortOrder, FilterResultViewModel filter)
        {
            List<SpentAmountViewModel> spentAmounts = new List<SpentAmountViewModel>();

            IEnumerable<SpentAmount> items = unitOfWork.SpentAmountRepository.Get(includeProperties: "ExpenseType, PaymentMode");
            if (filter != null && !filter.IsNullorEmpty)
            {
                items = FilterSpentAmount(items, filter);
            }
            switch (sortOrder)
            {
                case "amount":
                    items = items.OrderBy(d => d.AmountSpent);
                    break;
                case "amount_desc":
                    items = items.OrderByDescending(d => d.AmountSpent);
                    break;
                case "date":
                    items = items.OrderBy(d => d.SpentAmountDate);
                    break;
                case "date_desc":
                    items = items.OrderByDescending(d => d.AmountSpent);
                    break;
                default:
                    items = items.OrderByDescending(d => d.SpentAmountDate);
                    break;
            }

            //spentAmounts = ObjectMapper.MapObjects<SpentAmount, SpentAmountViewModel>(items.ToList());

            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SpentAmount, SpentAmountViewModel>();
                cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>();
                cfg.CreateMap<PaymentMode, PaymentModeViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();
            spentAmounts = mapper.Map<List<SpentAmountViewModel>>(items.ToList());


            return spentAmounts;
        }

        private IEnumerable<SpentAmount> FilterSpentAmount(IEnumerable<SpentAmount> items, FilterResultViewModel filter)
        {
            //List<SpentAmountViewModel> spentAmounts = new List<SpentAmountViewModel>();

            //var items = unitOfWork.SpentAmountRepository.Get();

            if (filter.FromDate != null && filter.FromDate != ApplicationDateTime.DefaultDateTime && filter.FromDate != ApplicationDateTime.MinDate)
            {
                items = items.Where(i => i.SpentAmountDate >= filter.FromDate);
            }

            if (filter.ToDate != null && filter.ToDate != ApplicationDateTime.DefaultDateTime && filter.FromDate != ApplicationDateTime.MinDate)
            {
                items = items.Where(i => i.SpentAmountDate <= filter.ToDate);
            }

            if (filter.ExpenseTypeId != 0)
            {
                items = items.Where(i => i.ExpenseTypeID == filter.ExpenseTypeId);
            }
            if (filter.PaymentModeId != 0)
            {
                items = items.Where(i => i.PaymentModeID == filter.PaymentModeId);
            }

            return items;
        }

        public SpentAmountViewModel UpdateSpentAmount(SpentAmountViewModel spentAmountToUpdate)
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SpentAmountViewModel, SpentAmount>()
                .ForMember("ExpenseTypeID", conf => conf.MapFrom(src => src.ExpenseType.ExpenseTypeId))
                .ForMember("PaymentModeID", conf => conf.MapFrom(src => src.PaymentMode.PaymentModeId))
                .ForMember("ExpenseType", opt => opt.Ignore())
                .ForMember("PaymentMode", opt => opt.Ignore());
                cfg.CreateMap<SpentAmount, SpentAmountViewModel>();
                cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>();
                cfg.CreateMap<PaymentMode, PaymentModeViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();

            SpentAmount itemToUpdate = mapper.Map<SpentAmount>(spentAmountToUpdate);
            unitOfWork.SpentAmountRepository.Update(itemToUpdate);
            unitOfWork.Save();

            SpentAmount updatedItem = unitOfWork.SpentAmountRepository.GetById(filter: q => q.SpentAmountId == itemToUpdate.SpentAmountId, includeProperties: "ExpenseType, PaymentMode");
            SpentAmountViewModel updatedSpentAmount = mapper.Map<SpentAmountViewModel>(updatedItem);

            return updatedSpentAmount;
        }

        public void CreateSpentAmount(CreateSpentAmountViewModel createSpentAmount)
        {

            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateSpentAmountViewModel, SpentAmount>()
                .ForMember("ExpenseTypeID", conf => conf.MapFrom(src => src.ExpenseType.ExpenseTypeId))
                .ForMember("PaymentModeID", conf => conf.MapFrom(src => src.PaymentMode.PaymentModeId))
                .ForMember("ExpenseType", opt => opt.Ignore())
                .ForMember("PaymentMode", opt => opt.Ignore());
                //cfg.CreateMap<SpentAmount, CreateSpentAmountViewModel>();
                //cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>();
                //cfg.CreateMap<PaymentMode, PaymentModeViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();

            SpentAmount itemToCreate = mapper.Map<SpentAmount>(createSpentAmount);

            unitOfWork.SpentAmountRepository.Insert(itemToCreate);
            unitOfWork.Save();
        }



    }
}
