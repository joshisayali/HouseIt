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
        
        public List<GetSpentAmountViewModel> GetSpentAmount(string sortOrder, ExpenseFilterViewModel filter)
        {
            List<GetSpentAmountViewModel> spentAmounts = new List<GetSpentAmountViewModel>();

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
                cfg.CreateMap<SpentAmount, GetSpentAmountViewModel>();
                cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>();
                cfg.CreateMap<PaymentMode, PaymentModeViewModel>();
                cfg.CreateMap<ExpenseSubCategory, ExpenseSubCategoryViewModel>();
                cfg.CreateMap<ExpenseRepeatFrequency, ExpenseRepeatFrequencyViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();
            spentAmounts = mapper.Map<List<GetSpentAmountViewModel>>(items.ToList());


            return spentAmounts;
        }

        private IEnumerable<SpentAmount> FilterSpentAmount(IEnumerable<SpentAmount> items, ExpenseFilterViewModel filter)
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

        public GetSpentAmountViewModel UpdateSpentAmount(GetSpentAmountViewModel spentAmountToUpdate)
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetSpentAmountViewModel, SpentAmount>()
                .ForMember("ExpenseTypeID", conf => conf.MapFrom(src => src.ExpenseType.ExpenseTypeId))
                .ForMember("PaymentModeID", conf => conf.MapFrom(src => src.PaymentMode.PaymentModeId))
                .ForMember("ExpenseType", opt => opt.Ignore())
                .ForMember("PaymentMode", opt => opt.Ignore());
                cfg.CreateMap<SpentAmount, GetSpentAmountViewModel>();
                cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>();
                cfg.CreateMap<PaymentMode, PaymentModeViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();

            SpentAmount itemToUpdate = mapper.Map<SpentAmount>(spentAmountToUpdate);
            unitOfWork.SpentAmountRepository.Update(itemToUpdate);
            unitOfWork.Save();

            SpentAmount updatedItem = unitOfWork.SpentAmountRepository.GetById(filter: q => q.SpentAmountId == itemToUpdate.SpentAmountId, includeProperties: "ExpenseType, PaymentMode");
            GetSpentAmountViewModel updatedSpentAmount = mapper.Map<GetSpentAmountViewModel>(updatedItem);

            return updatedSpentAmount;
        }

        public void CreateSpentAmount(CreateSpentAmountViewModel createSpentAmount)
        {

            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateSpentAmountViewModel, SpentAmount>()
                //.ForMember("ExpenseTypeID", conf => conf.MapFrom(src => src.ExpenseType.ExpenseTypeId))                
                .ForMember("PaymentModeID", conf => conf.MapFrom(src => src.PaymentMode.PaymentModeId))
                .ForMember("ExpenseSubCategoryId", conf => conf.MapFrom(src => src.ExpenseSubCategory.ExpenseSubCategoryId))
                .ForMember ("RepeatId", conf => conf.MapFrom(src => src.ExpenseRepeatFrequency.RepeatId))
                .ForMember("ExpenseType", opt => opt.Ignore())
                .ForMember("PaymentMode", opt => opt.Ignore())
                .ForMember("ExpenseSubCategory", opt => opt.Ignore())
                .ForMember("ExpenseRepeatFrequency", opt => opt.Ignore());
                //cfg.CreateMap<SpentAmount, CreateSpentAmountViewModel>();
                //cfg.CreateMap<ExpenseType, ExpenseTypeViewModel>();
                //cfg.CreateMap<PaymentMode, PaymentModeViewModel>();
            });
            var mapper = mapperConfig.CreateMapper();

            SpentAmount itemToCreate = mapper.Map<SpentAmount>(createSpentAmount);
            //To Do: delete below line of code
            itemToCreate.ExpenseTypeID = 5;
            unitOfWork.SpentAmountRepository.Insert(itemToCreate);
            unitOfWork.Save();
        }

        public void DeleteSpentAmount(int id)
        {
            unitOfWork.SpentAmountRepository.Delete(id);
            unitOfWork.Save();
        }



    }
}
