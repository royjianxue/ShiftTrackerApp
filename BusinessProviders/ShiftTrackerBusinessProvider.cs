using Common.Contracts.Model;
using DataProviders.Data;
namespace BusinessProviders
{
    public class ShiftTrackerBusinessProvider : IShiftTrackerBusinessProvider
    {
        private readonly IShiftTrackerDataProvider _shiftTrackerDataProvider;
        public ShiftTrackerBusinessProvider(IShiftTrackerDataProvider shiftTrackerDataProvider)
        {
            _shiftTrackerDataProvider = shiftTrackerDataProvider;
            _shiftTrackerDataProvider.CreateDatabase();
        }


        public List<Shift> GetAllRecord()
        {
            return _shiftTrackerDataProvider.SelectAll();
        }

        public List<Shift> GetRecordByID(int id)
        {
            return _shiftTrackerDataProvider.SelectAll().Where(p => p.ShiftId == id).ToList();
        }

        public void PostRecord(Shift shifts)
        {
            _shiftTrackerDataProvider.Post(shifts);
        }


        public void DeleteAllRecord()
        {
            _shiftTrackerDataProvider.DeleteALL();
        }

        public void DeleteRecordById(int id)
        {
            _shiftTrackerDataProvider.DeleteById(id);
        }
    }
}
