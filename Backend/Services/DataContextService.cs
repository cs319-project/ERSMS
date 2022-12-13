using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;

namespace Backend.Services
{
    public abstract class DataContextService
    {
        protected readonly DataContext _dataContext;

        public DataContextService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
