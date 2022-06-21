using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Common.Contracts.Model;
using Dapper;

namespace DataProviders.Data
{
    public class ShiftTrackerDataProvider : IShiftTrackerDataProvider
    {
        private IConfiguration configuration;
        public ShiftTrackerDataProvider(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public SqlConnection CreatConnection()
        {
            var connectionString = configuration.GetConnectionString("Defaultconnection");
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public void CreateDatabase()
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ShiftTrackerDb')
                                        BEGIN
                                        CREATE DATABASE ShiftTrackerDb;
                                        END;";
                    cmd.ExecuteNonQuery();
                }
            }
            CreateTable();
        }

        public void CreateTable()
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"USE ShiftTrackerDb IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ShiftTracker')
                        CREATE TABLE ShiftTracker (
	                      shiftId int IDENTITY(1,1) NOT NULL,
                          StartTime datetime NOT NULL,
                          EndTime datetime NOT NULL,
	                      Pay decimal(5,2) NOT NULL,
                          Minutes decimal(5,2) NOT NULL ,
                          Location varchar(100) NOT NULL,
	                      PRIMARY KEY (shiftId)
                );";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Shift> SelectAll()
        {
            using var conn = CreatConnection();

            var shifts = conn.Query<Shift>("USE ShiftTrackerDb SELECT * FROM ShiftTracker").ToList();

            return shifts;

        }
        public Shift SelectById(Shift shifts)
        {
            using var conn = CreatConnection();

            var shift = conn.QueryFirst<Shift>("USE ShiftTrackerDb SELECT from ShiftTracker WHERE shiftId = @id", new { id = shifts.ShiftId});

            return shift;
        }

        public void Post(Shift shifts)
        {
            using var conn = CreatConnection();
            var query = "USE ShiftTrackerDb INSERT INTO ShiftTracker(StartTime, EndTime,Pay, Minutes, Location ) VALUES(@StartTime, @EndTime, @Pay, @Minutes, @Location)";
            var dp = new DynamicParameters();
            dp.Add("@StartTime", shifts.Start);
            dp.Add("@EndTime", shifts.End);
            dp.Add("@Pay", shifts.Pay);
            dp.Add("@Minutes", shifts.Minutes);
            dp.Add("@Location", shifts.Location);
            conn.Execute(query, dp);
        }
        
        public void DeleteALL()
        {     
            using var conn = CreatConnection();
            conn.Execute(@"USE ShiftTrackerDb DELETE FROM ShiftTracker");
        }

        public void DeleteById(int id)
        {
            using var conn = CreatConnection();
            conn.Execute(@"USE ShiftTrackerDb DELETE FROM ShiftTracker WHERE shiftId = @shiftId", new { shiftId = id });
        }
    }
}
