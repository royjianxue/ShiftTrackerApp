using Common.Contracts.Model;
using Microsoft.Data.SqlClient;

namespace DataProviders.Data
{
    public interface IShiftTrackerDataProvider
    {
        SqlConnection CreatConnection();
        void CreateDatabase();
        void CreateTable();
        List<Shift> SelectAll();
        void Post(Shift shifts);
        void DeleteALL();
        void DeleteById(int id);
    }
}