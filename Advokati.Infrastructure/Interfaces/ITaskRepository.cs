using System.Collections.Generic;
using Advokati.Infrastructure.Model.ViewModel;
using Task = Advokati.Infrastructure.Model.Task;


namespace Advokati.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        void Add(Task t);
        void Edit(Task t);
        void Remove(int id);
        IEnumerable<Task> GetTasks();
        Task FindById(int id);
        IEnumerable<AdvokatKlijentVM> FindByAdvokat(int advokatId);
        IEnumerable<Task> FindByKlijent(int klijentId);
        IEnumerable<StatistikaVM> GetStatistika();
    }
}
