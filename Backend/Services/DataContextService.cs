using Backend.Data;

namespace Backend.Services
{
    /// <summary>A service for data context operations.</summary>
    public abstract class DataContextService
    {
        protected readonly DataContext _dataContext;

        /// <summary>Initializes a new instance of the <see cref="DataContextService"/> class.</summary>
        /// <param name="dataContext">The data context.</param>
        public DataContextService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
