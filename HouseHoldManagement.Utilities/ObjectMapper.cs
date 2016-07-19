using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseHoldManagement.Utilities
{
    public class ObjectMapper
    {
        public static K MapObjects<T, K>(T source)
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<T, K>());
            var mapper = mapperConfig.CreateMapper();
            K target = mapper.Map<K>(source);
            return target;
        }

        public static List<K> MapObjects<T, K>(List<T> source)
        {
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<T, K>());
            var mapper = mapperConfig.CreateMapper();
            List<K> target = mapper.Map<List<K>>(source);
            return target;                        
        }
    }
}
