using Common.Contracts.Model;

namespace BusinessProviders
{
    public interface IShiftTrackerBusinessProvider
    {
        List<Shift> GetAllRecord();
        List<Shift> GetRecordByID(int id);
        void PostRecord(Shift shifts);
        void DeleteAllRecord();
        void DeleteRecordById(int id);
    }
}