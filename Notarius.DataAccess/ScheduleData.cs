using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Notarius.Shared.DataModel;

namespace Notarius.DataAccess
{
    public static class ScheduleData
    {
        const int _key = 0;
        const int _mrn = 1;
        const int _providerId = 2;
        const int _ScheduleTime = 3;
        public static async Task<ObservableCollection<Schedule>> GetSchedulesAsync()
        {
            ObservableCollection<Schedule> returnList = new ObservableCollection<Schedule>();


            try
            {
                using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
                {
                    string commandText = string.Empty;

                    commandText = SQLResources.GetAllSchedules;


                    //this will be faster than just getting a list of EntityIds and then call GetEntity for each

                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    cmd.CommandText = commandText;


                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Schedule ent = new Schedule();
                        ent.Key = reader.GetInt32(_key);
                        ent.MRN = GetString(reader.GetString(_mrn)) ;
                        ent.ProviderId = GetString(reader.GetString(_providerId));
                        ent.ScheduleTime = GetString(reader.GetString(_ScheduleTime));
                        ent.Patient = PatientData.GetPatientAsync(ent.MRN);
                        returnList.Add(ent);
                    }
                    return returnList;


                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
                return new ObservableCollection<Schedule>();
            }
        }

        private static string GetString(object dbData)
        {
            if (dbData == DBNull.Value)
                return "";
            else
                return (string) dbData;
        }
        public static async Task<bool> SaveAsync(Schedule entity)
        {

            try
            {
                Schedule dto = Get(entity.Key);

                using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
                {

                    string commandText = string.Empty;
                    int Key = 0;
                    if (dto == null)
                    {
                        commandText = SQLResources.InsertSchedule;
                        SQLiteCommand cmdSeq = new SQLiteCommand(SQLResources.GetNextSheduleSEQ, conn);
                        SQLiteDataReader reader = cmdSeq.ExecuteReader();
                        Key = 1;
                        while (reader.Read())
                        {
                            if (reader[0] is DBNull)
                                break;
                           Key = reader.GetInt32(0);
                           Key += 1;
                        }
                    }
                    else
                    {
                        commandText = SQLResources.UpdateSchedule;
                        Key = dto.Key;
                    }


                    using (SQLiteCommand cmdSave = new SQLiteCommand(commandText, conn))
                    {
                        cmdSave.Parameters.AddWithValue("@Key", Key);
                        cmdSave.Parameters.AddWithValue("@MRN", entity.MRN);
                        cmdSave.Parameters.AddWithValue("@ProviderId", entity.ProviderId);
                        cmdSave.Parameters.AddWithValue("@ScheduleTime", entity.ScheduleTime);

                        cmdSave.ExecuteNonQuery();


                        entity.IsDirty = false;

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Schedule Get(int key)
        {

            using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
            {
                Schedule schedule = null;

                // hydrate the object here
                using (SQLiteCommand cmd = new SQLiteCommand(SQLResources.GetSchedule, conn))
                {
                    cmd.Parameters.AddWithValue("@KEY", key);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        schedule = new Schedule();
                        schedule.MRN = reader.GetString(_mrn).ToString();
                        schedule.ProviderId = reader.GetString(_providerId).ToString();
                        schedule.ScheduleTime = reader.GetString(_ScheduleTime).ToString();
                        schedule.Patient = PatientData.GetPatientAsync(reader.GetString(_mrn));

                    }
                }
                if (schedule != null)
                    schedule.IsDirty = false;

                return schedule;
            }

        }

        public static async Task<bool> DeleteAsync(int key)
        {
            using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
            {
                Schedule schedule = null;

                // hydrate the object here
                using (SQLiteCommand cmd = new SQLiteCommand(SQLResources.DeleteSchedule, conn))
                {
                    cmd.Parameters.AddWithValue("@KEY", key);
                    cmd.ExecuteNonQuery();

                }

                return true;
            }
        }
    }
}