using AutoMapper;
using BusinessLogic.Dtos.Log;
using EntityFramework.Entities;
using EntityFrameworkExtension.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class LogMappers
    {
        internal static IMapper Mapper { get; set; }

        static LogMappers()
        {
            Mapper = new MapperConfiguration(
                cfg => cfg.AddProfile<LogMapperProfile>())
                .CreateMapper();
        }

        public static LogDto ToModel(this Log log)
        {
            return Mapper.Map<LogDto>(log);
        }

        public static LogsDto ToModel(this PagedList<Log> logs)
        {
            return Mapper.Map<LogsDto>(logs);
        }

        public static Log ToEntity(this LogDto log)
        {
            return Mapper.Map<Log>(log);
        }
    }
}
