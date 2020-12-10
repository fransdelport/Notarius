using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Threading.Tasks;
using Notarius.Shared.DataModel;

namespace Notarius.DataAccess
{
    public static class PatientData
    {
       const int _mrn = 0;
        const int _firstname = 1;
        const int _lastname = 2;
        const int _address = 3;
        const int _city = 4;
        const int _state = 5;
        const int _zip = 6;

        public static async Task<ObservableCollection<Patient>> GetPatientsAsync(bool deleted = false)
        {
            ObservableCollection<Patient> returnList = new ObservableCollection<Patient>();


            try
            {
                using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
                {
                    string commandText = string.Empty;

                    commandText = SQLResources.GetAllPatients;
                    if (deleted)
                        commandText = commandText.Replace("SleutelWord", "SleutelWordDeleted");

                    //this will be faster than just getting a list of EntityIds and then call GetEntity for each

                    SQLiteCommand cmd = new SQLiteCommand(conn);
                    cmd.CommandText = commandText;

                    // cmd.Parameters.AddWithValue("@EntityId", entityId);
                    // ObservableCollection<Patient> returnList = new ObservableCollection<Patient>();


                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Patient ent = new Patient();
                        ent.MRN = reader.GetString(_mrn).ToString();
                        ent.Address = reader.GetString(_address).ToString();
                        ent.Firstname = reader.GetString(_firstname).ToString();
                        ent.Lastname = reader.GetString(_lastname).ToString();
                        ent.City = reader.GetString(_city).ToString();
                        ent.State = reader.GetString(_state).ToString();
                        ent.Zip = reader.GetString(_zip).ToString();
                        ent.IsDirty = false;
                        returnList.Add(ent);
                    }

                    return returnList;


                }
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
                return new ObservableCollection<Patient>();
            }
        }

        public static  Patient GetPatientAsync(string key)
        {

            using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
            {
                Patient patient = null;

                // hydrate the object here
                using (SQLiteCommand cmd = new SQLiteCommand(SQLResources.GetPatient, conn))
                {
                    cmd.Parameters.AddWithValue("@MRN", key);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        patient = new Patient();
                        patient.MRN = reader.GetString(_mrn).ToString();
                        patient.Address = reader.GetString(_address).ToString();
                        patient.Firstname = reader.GetString(_firstname).ToString();
                        patient.Lastname = reader.GetString(_lastname).ToString();
                        patient.City = reader.GetString(_city).ToString();
                        patient.State = reader.GetString(_state).ToString();
                        patient.Zip = reader.GetString(_zip).ToString();

                    }
                }
                if (patient != null)
                    patient.IsDirty = false;

                return patient;
            }


        }

        public static async Task<bool> SavePatientAsync(Patient entity)
        {

            try
            {
                Patient dto = GetPatientAsync(entity.MRN);

                using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
                {

                    string commandText = string.Empty;

                    if (dto == null)
                        commandText = SQLResources.InsertPatient;
                    else
                        commandText = SQLResources.UpdatePatient;


                    using (SQLiteCommand cmdSave = new SQLiteCommand(commandText, conn))
                    {
                        cmdSave.Parameters.AddWithValue("@MRN", entity.MRN);
                        cmdSave.Parameters.AddWithValue("@FirstName", entity.Firstname);
                        cmdSave.Parameters.AddWithValue("@LastName", entity.Lastname);
                        cmdSave.Parameters.AddWithValue("@Address", entity.Address);
                        cmdSave.Parameters.AddWithValue("@City", entity.City);
                        cmdSave.Parameters.AddWithValue("@State", entity.State);
                        cmdSave.Parameters.AddWithValue("@Zip", entity.Zip);

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

        public static  async Task<bool> DeletePatientAsync(string MRN )
        {
            using (SQLiteConnection conn = DatabaseMigration.OpenConnection())
            {
                Patient patient = null;

                // hydrate the object here
                using (SQLiteCommand cmd = new SQLiteCommand(SQLResources.DeletePatient, conn))
                {
                    cmd.Parameters.AddWithValue("@MRN", MRN);
                    cmd.ExecuteNonQuery();
                   
                }
                
                return true;
            }
        }
    }
}
