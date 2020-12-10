using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Notarius.DataAccess.Configuration
{
    [Serializable]
    public class Confguration
    {

        public string Database = @"C:\TEMP\Notarius.db3";
        public void SaveState()
        {
            string FilePath = @"SleutelSettings.cfg";

            Confguration myObject; // = New clsLicense
            myObject = this;
            // Insert code to set properties and fields of the object.
            XmlSerializer mySerializer = new XmlSerializer(typeof(Confguration));
            // To write to a file, create a StreamWriter object.
            StreamWriter myWriter = new StreamWriter(FilePath);
            mySerializer.Serialize(myWriter, myObject);

            myWriter.Close();

            myWriter = null;
        }

        public Confguration LoadState()
        {
            Confguration objTask;

            Confguration o = new Confguration();

            string FilePath = @"SleutelSettings.cfg";

            o = null;

            if (!File.Exists(FilePath))
            {
                SaveState();
            }

            // Constructs an instance of the XmlSerializer with the type
            // of object that is being deserialized.
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(Confguration));
                // To read the file, creates a FileStream.
                FileStream myFileStream = new FileStream(FilePath, FileMode.Open);
                // Calls the Deserialize method and casts to the object type.
                objTask = (Confguration)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                myFileStream = null;
            }
            catch
            {
                throw new System.Exception("Could not load license file");
            }


            return objTask;
        }
    }
}
