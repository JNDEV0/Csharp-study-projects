using FileAndDirectory;
using System.Text.Json;
using System.Xml.Serialization;

namespace FileAndDictionary
{
    class Program
    {
        public static void Main()
        {
            //these classes are used to manipulate files and directories(folders and drives).
            //get details, read, write, copy, create, delete files and folder and their content.
            FileClass();
            FileInfoClass();
            DirectoryClass();
            DirectoryInfoClass();
            DriveInfoClass();
            FileStreamClass();
            StreamWriterAndStreamReader();
            BinaryWriterAndBinaryReader();
            JsonSerialization();
            XmlSerialization();
        }

        private static void FileClass()
        {
            //all methods in File class are static
            //the path contains the name of the file and extension
            //the @ symbol disables escape sequence characters in the string, so \ is not seen as an escape sequence
            string fileName = "TextFile.txt";
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{fileName}";
            
            //when a file is created, a FileStream is opened, so the file is being accessed
            //Create() will by default override the file even if it exists
            //thus Close() method needs to be called to close the file
            File.Create(path).Close();

            bool fileExists = File.Exists(path);
            Console.WriteLine($"File {fileName} exists: {fileExists}");

            string copyFileName = "CopyTextFile.txt";
            string copyPath = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{copyFileName}";
            
            //File class does not store the path
            //so the path is passed for the file being copied and where the copy is to be stored.
            //the third parameter is the override boolean, if not true Copy() will throw an error if file copy already exists
            File.Copy(path,copyPath, true);
            Console.WriteLine($"File {fileName} copied into {copyFileName}");

            string moveFileName = "MovedCopyTextFile.txt";
            string movePath = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{moveFileName}";

            //Can move a file or rename it, source and destination paths must be in same drive
            File.Move(path, movePath, true);
            Console.WriteLine($"File {copyFileName} moved/renamed to {moveFileName}");

            //deletes the named file at path
            File.Delete(copyPath);
            Console.WriteLine($"{copyFileName} deleted.");

            //WriteAllText will create a file and write the second param "content" string into the file
            //if the file already exists, it will override the file and close the file
            string stringSaveTextToFile = "The quick brown fox jumps over the lazy dog";
            string stringSaveFileName = "stringFile.txt";
            string stringSavePath = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{stringSaveFileName}";
            File.WriteAllText(stringSavePath, stringSaveTextToFile);

            //ReadAllText() reads the content of a file and returns a string
            string retrievedFileContent = File.ReadAllText(stringSavePath);
            
            //note that the string displayed in console below is retrieved from the file, not from memory
            //if the file content is modified externally, it will also modify the content of the string retrieved
            Console.WriteLine(retrievedFileContent); 

            //WriteAllLines() will read the IEnumerable collection and write each element to a new line in the file
            //in the example below, actual name, dob, address etc could be interpolated into the string to save custom data into the file for example
            List<string> list = new List<string>() { "Name: ", "DOB: ", "Address: ", "Phone: "};
            string lineSaveFileName = "LineFile.txt";
            string lineSavePath = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{lineSaveFileName}";
            File.WriteAllLines(lineSavePath, list);

            //ReadAllLines() will read each line of the file and return a string array
            //note the lines shown in console were retrieved from the file, not from memory
            string[] retrievedLines = File.ReadAllLines(lineSavePath);
            foreach (string line in retrievedLines)
            {
                Console.WriteLine(line);
            }

            //created files are being deleted passing the path to each
            File.Delete(lineSavePath);
            File.Delete(stringSavePath);
            File.Delete(movePath);
        }

        private static void FileInfoClass()
        {
            //The FileInfo class is not static so must be instantiated, filepath is given to the constructor
            //it is also a sealed class, so cannot have methods extend it
            string fileName = "FileInfo.txt";
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{fileName}";
            FileInfo fileInfo = new FileInfo(path);

            //when FileInfo is instantiated, it does not create any file
            //must be called manually, and since Create() opens the file, must call Close() as well to close created file
            //note that unlike File class, the path is not passed to Create(), since FileInfo stores the path given in constructor
            fileInfo.Create().Close();

            //since FileInfo stores the original path when instantiated it only needs the destination path for operations
            //here the filename is modified to copy the original file to its copy in the same folder
            //path is also reassigned value so that it no longer stores the fileName of the original
            //the second param is to allow override of the destination file
            fileName = "CopyFileInfo.txt";
            path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{fileName}";
            FileInfo copyFileInfo = fileInfo.CopyTo(path, true);

            //here once again the original path is not given, since it is stored in the original FileInfo object when instantiated
            fileName = "MovedFileInfo.txt";
            path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\{fileName}";
            copyFileInfo.MoveTo(path, true);


            //FileInfo has various properties that can provide information about the file referenced to
            Console.WriteLine(copyFileInfo.Exists); //bool if file at saved path exists
            Console.WriteLine(copyFileInfo.FullName); //full path to file, including file name and extension
            Console.WriteLine(copyFileInfo.Name); //file name and extension only
            Console.WriteLine(copyFileInfo.Directory); //name of folder that contains file, method 1
            Console.WriteLine(copyFileInfo.DirectoryName); //full path to folder that contains the file method 2
            Console.WriteLine(copyFileInfo.Extension); //only the file type extension, such as .txt
            Console.WriteLine(copyFileInfo.Length); //length/size in bytes of the file
            Console.WriteLine(copyFileInfo.CreationTime); //DateTime of file creation
            Console.WriteLine(copyFileInfo.LastWriteTime); //DateTime of last time file was written to
            Console.WriteLine(copyFileInfo.LastAccessTime); //DateTime of last time file was opened

            //here the fileInfo objects are deleted directly with no path given
            fileInfo.Delete();
            copyFileInfo.Delete();
        }

        private static void DirectoryClass()
        {
            //the directory class is static, and so it does not store the path of the directory
            //and does not need to be instantiated
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\Countries";
            Directory.CreateDirectory(path); //creates the folder at given path
            Directory.CreateDirectory(path + @"\UK"); //folder name is being added to the path of the parent folder created above
            Directory.CreateDirectory(path + @"\USA"); //these folders are being created inside Countries folder
            Console.WriteLine("Countries folder created. UK and USA folders created inside.");

            //files can be created directly inside folders as well
            string filePath;
            filePath = path + @"\UK\CapitalInfo.exe";
            File.Create(filePath).Close(); //File.Create() opens a FileStream to write into the file, and so .Close() must be called to close the filestream
            filePath = path + @"\UK\PopulationSize.exe";
            File.Create(filePath).Close();
            filePath = path + @"\UK\PopularSports.exe";
            File.Create(filePath).Close();

            //three files are being created in each of the two folders
            filePath = path + @"\USA\Capital.exe";
            File.Create(filePath).Close();
            filePath = path + @"\USA\PopulationSize.exe";
            File.Create(filePath).Close();
            filePath = path + @"\USA\PopularSports.exe";
            File.Create(filePath).Close();

            //will return a string array with the paths of each subdirectory folder
            string[] subDirectories = Directory.GetDirectories(path);
            foreach(string subDirName in subDirectories)
            {
                Console.WriteLine(subDirName);
            }

            //will return a string array of the path of each file in the directory
            //the second param is the "pattern" to search for, in this case, any .txt file
            string[] filesInDirectory = Directory.GetFiles(path + @"\USA", "*.txt");
            foreach (string fileName in filesInDirectory)
            {
                Console.WriteLine(fileName);
            }

            //a directory can be deleted using Delete(), however it will throw an error if the directory is not empty
            //unless the second param "recursive" is true, in which case it will recursively delete all files and folders in the named directory
            Directory.Delete(path + @"\USA", true);

            Console.WriteLine("USA Subdirectory deleted, now:");

            //will return a string array with the paths of each subdirectory folder
            subDirectories = Directory.GetDirectories(path);
            foreach (string subDirName in subDirectories)
            {
                Console.WriteLine(subDirName);
            }

            //a directory can be moved to another path location within same drive, or renamed in same location
            Directory.Move(path + @"\UK", path + @"\Brazil");

            //deletes the directory found at the path, and second param true to recursively delete folders and files inside.
            Directory.Delete(path, true);
        }

        private static void DirectoryInfoClass()
        {
            //DirectoryInfo like FileInfo stores the initial path inside the instantiated object
            //using the "Info" variant is better when performing multiple operations on the same folder or file
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\CountriesDirectoryInfo";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            dirInfo.CreateSubdirectory("UK");
            dirInfo.CreateSubdirectory("USA");

            //getting the subdirectories paths and converting to a list, then creating files inside each subdirectory
            dirInfo.GetDirectories().ToList().ForEach(dir =>
            {
                File.Create($@"{dir}\Capital.txt").Close(); //if the created file is not closed
                File.Create($@"{dir}\Population.txt").Close(); //it will cause access denied error
                File.Create($@"{dir}\Sports.txt").Close(); //when the program attempts to move the directory below
            });

            //moving/renaming the created parent directory, effectively altering the path to all internal folders and files
            dirInfo.MoveTo($@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\DirInfoCountries");

            FileInfo[] filesInDirInfo = dirInfo.GetFiles();
            Console.WriteLine("Display files in DirInfoCountries");
            foreach (FileInfo filePath in filesInDirInfo)
            {
                Console.WriteLine(filePath);
            }

            DirectoryInfo[] dirsInDirInfo = dirInfo.GetDirectories();
            Console.WriteLine("Display directories in DirInfoCountries");
            foreach (DirectoryInfo dirPath in dirsInDirInfo)
            {
                Console.WriteLine(dirPath);
            }

            //the DirectoryInfo class will fill various properties,
            //with information about the stored directory path, same as the FileInfo stores about the given file
            Console.WriteLine("dirInfo Exists: " + dirInfo.Exists);
            Console.WriteLine("dirInfo FullName: " + dirInfo.FullName);
            Console.WriteLine("dirInfo Name:  " + dirInfo.Name);
            Console.WriteLine("dirInfo Parent: " + dirInfo.Parent);
            Console.WriteLine("dirInfo CreationTime: " + dirInfo.CreationTime);
            Console.WriteLine("dirInfo LastAccessTime: " + dirInfo.LastAccessTime);
            Console.WriteLine("dirInfo LastWriteTime: " + dirInfo.LastWriteTime);
            Console.WriteLine("dirInfo Root: " + dirInfo.Root);


            //recursively delete the created directory, including internal files and folders
            dirInfo.Delete(true);
        }

        private static void DriveInfoClass()
        {
            //DriveInfo is rarely used, maybe during a program installation
            //returns various informations about the target drive letter, such as total drive space and available space
            DriveInfo driveInfo = new DriveInfo("C:");
            Console.WriteLine("DriveType: " + driveInfo.DriveType); //fixed == hdd or ssd, other types as well
            Console.WriteLine("Name: " + driveInfo.Name); //the letter name of the drive
            Console.WriteLine("VolumeLabel: " + driveInfo.VolumeLabel); //the label/name of the volume on the drive, can be assigned
            Console.WriteLine("TotalSize: " + (driveInfo.TotalSize / 1024 / 1024 / 1024) + "GB"); //size of the drive in bytes, here its divided to get GBs
            Console.WriteLine("TotalFreeSpace: " + (driveInfo.TotalFreeSpace / 1024 / 1024 / 1024) + "GB"); //available free space in bytes method 1, here its divided to get GBs
            Console.WriteLine("AvailableFreeSpace: " + (driveInfo.AvailableFreeSpace / 1024 / 1024) + "MB"); //available free space in bytes method 2, here its divided to get MBs
            Console.WriteLine("IsReady: " + driveInfo.IsReady); //bool value to confirm if drive is "ready"
            Console.WriteLine("DriveFormat: " + driveInfo.DriveFormat); //ntfs or fat32 format?
        }

        //FileStream will write the content in plaintext as passed to it to the target file
        private static void FileStreamClass()
        {
            //remember that the path must include filename and file extension
            //if file extension is ommitted when creating the file even in filestream, it will create a file with no extension type
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\fileStreamPractice.txt";
            FileStream fileStream;
            //using the FileMode.CreateNew param, will create a file or THROW AN IOException ERROR if file already exists
            if (!File.Exists(path))
            {
                fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite);
                fileStream.Close();
            }

            //File.Create returns a FileStream, with FileMode.Create and FileAccess.Write
            fileStream = File.Create(path);
            fileStream.Close();

            //FileMode.OpenOrCreate will open or create the file if it does not exist.
            fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Close();

            //FileMode.Append will open an existing file (check that file exists first) and seek the end of the file, to write into
            //useful with FileAccess.Write
            fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
            fileStream.Close();

            //FileMode defines the mode of the filestream. ie OpenOrCreate, CreateNew etc, see F12 on FileMode To see others
            //FileAccess defines mode of access. ie ReadWrite, Write, Read.
            fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Close();

            //File.Open also has an overload that takes FileMode and FileAccess, and so can also create and open a file
            fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
            fileStream.Close();

            //another way to create a file if file does not exist, and open file with write access
            fileStream = File.OpenWrite(path);

            //multiple write operations can take place into an opened file,
            string content = path;
            byte[] contentBytes = System.Text.Encoding.ASCII.GetBytes(content);

            //to write to a file, the content needs to be converted into a byte array representation of the data
            //the second param buffer, is the starting position IN THE FILE not the array
            //and the third param count is the length of the bytes to write into the file
            //here the file path string is written into the file fileStreamPractice
            fileStream.Write(contentBytes, 0, content.Length);
            
            //Close() must be called to close it to avoid IOException
            //preferably in the same method that opens the file
            fileStream.Close();

            //it is better to write and read the file in separate streams to avoid complication of searching out to certain bytes
            //or unexpectedly reading incorrect information due to reading newly written information into the file
            FileStream readFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            readFileStream.Close();

            //if a file path is already stored in a FileInfo instance
            //there are some methods to access the file to read it
            FileInfo fileInfo = new FileInfo(path);
            readFileStream = fileInfo.OpenRead();
            readFileStream.Close();

            //here the path is not given, since fileInfo already stores it
            readFileStream = fileInfo.Open(FileMode.Open, FileAccess.Read);
            readFileStream.Close();

            //here the static File.Open() method is used, along with defined FileMode and FileAccess
            readFileStream = File.Open(path, FileMode.Open, FileAccess.Read);
            readFileStream.Close();

            //here the static File.OpenRead() method is called, which is shorthand to open the file in FileMode.Open and FileAccess.Read
            readFileStream = File.OpenRead(path);

            //to actually read the file into runtime memory, the data from the file must have its bytes fetched
            //the Read() method of the FileAccess.Read filestream will fill the "buffer" byte[] with the bytes of the file's content
            //starting at offset until the count, this enables multiple read operations, perhaps between other code logic
            byte[] bytesFromFile = new byte[readFileStream.Length];
            readFileStream.Read(bytesFromFile, 0, bytesFromFile.Length);

            //then the data is converted from byte using Encoding
            string fileBytesToString = System.Text.Encoding.ASCII.GetString(bytesFromFile);
            Console.WriteLine($"data retrieved from file: {fileBytesToString}");
            
            //here the filestream needs to be closed after all work is done
            //not closing the filestream may cause operating system error
            readFileStream.Close();
        }

        //StreamWriter will write the data in plaintext to the file as passed to it
        private static void StreamWriterAndStreamReader()
        {
            //streamwriter and streamreader internally perform the byte conversion of strings passed to and from these classes and files
            //however they can ONLY work with text files, NOT images, videos etc.
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\streamWriterAndReader.txt";
            
            //streamwriter can receive just the file path directly, if file already exists
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.Close();

            //constructor overloads for StreamWriter can also receive a Stream,
            //which a FileStream is also derived from, it uses the stored path of the filestream
            FileStream fileStreamWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            streamWriter = new StreamWriter(fileStreamWrite);

            //here the StreamWriter is receiving a string directly and internally performs byte array conversion
            streamWriter.WriteLine(path);

            //closing the file access is important to make sure changes are saved and there are no file access denied errors later on
            streamWriter.Close();

            //Calling Dispose, or using the using construct which automatically calls Dispose() at the end
            //which internally calls Close(), is another way to close the file connection
            streamWriter.Dispose();

            //CreateText() of a FileInfo instance will return a StreamWriter instance
            //and the using keyword will internally call the Dispose() method,
            //which in turn calls Close() method when the scope operations are finalized
            FileInfo fileInfo = new FileInfo(path);
            using (streamWriter = fileInfo.CreateText())
            {
                streamWriter.WriteLine(path);
            }

            //the AppendText() will return a StreamWriter instance same as the above method,
            //however it will open the file in FileMode.Append, which will look to the end of the file to write data to
            streamWriter = fileInfo.AppendText();
            streamWriter.Close();

            //FileInfo instance has OpenText() that will return a StreamReader instance
            using (StreamReader streamReader = fileInfo.OpenText())
            {
                //the using construct automatically calls Dispose which calls Close at the end of the statement
                //however it can also be called explicitly, thought not required
                //outside a using construct, Close() must be called on a FileStream or StreamReader or SteamWriter
                //to avoid file access conflicts
                streamReader.Close();
            } ;
            

            //FileStream can also be passed to StreamReader, which then uses the stored path, filemode and fileaccess
            //using construct will automatically call Dispose once its done, thus calling Close() on the file access
            //streamreader will open the file at path with fileMode Open and fileaccess Read
            FileStream fileStreamRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStreamRead))
            {
                //attempts to read a single line of content in the file
                string? singleFileLineToString = streamReader.ReadLine();
                if (singleFileLineToString is not null)
                Console.WriteLine(singleFileLineToString);

                //note that the using construct will call Dispose and then Close ONLY on the StreamReader
                //which by itself does not close the FileStream used by StreamReader
                fileStreamRead.Close();
            }

            fileStreamRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStreamRead))
            {
                //read entire file content return as string
                //will also return whitespace in the file, so Trim() is being called
                string entireFileContentToString = streamReader.ReadToEnd().Trim();
                Console.WriteLine(entireFileContentToString);
            }

            fileStreamRead = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStreamRead))
            {
                //a buffer is a temporary place in memory for data
                //ReadBlock() will return data into the buffer array param1, starting at index param2, for length param3
                char[] buffer = new char[1];
                streamReader.ReadBlock(buffer, 0, 1);

                //note that even though it is not shown in console, because ReadBlock() is called before the Read() methods below
                //it internally is moving the cursor of StreamReader for the next operations
                //Read() method also has an iteration cursor internally
                //and will call the next set of characters start from one character after the end of the last stipulated set
                buffer = new char[10];
                streamReader.Read(buffer, 0, 10);
                Console.WriteLine(new string(buffer));

                //here calling Read() again will read the next set of 10 characters in the file
                streamReader.Read(buffer, 0, 10);
                Console.WriteLine(new string(buffer));
            }
        }

        //Binary encoded data will look like nonsense text, since it in fact stored the binary reference to each data type
        //and is thus not human readable, except for string data type content
        private static void BinaryWriterAndBinaryReader()
        {
            //where StreamWriter and StreamReader can write or read string data only
            //BinaryWriter and BinaryReader can encode or decode any other type of data as well
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\binaryWriterAndReader.txt";
            var user = 
                new {
                        age = 32,
                        weight = 120.24f, 
                        name = "Billy Bob",
                        height = 180.2m,
                        evaluated = true,
                    };

            //a filestream is passed to the BinaryWriter to access the opened or created file with Write access
            //note that the using construct is used, and so Close() does not need to be manually called
            //since using will call Dispose() which in turn calls Close()
            FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            using (BinaryWriter bw = new BinaryWriter(fsWrite))
            {
                //Write() method in BinaryWriter has many overloads to write various data types
                bw.Write(user.age);
                bw.Write(user.weight);
                bw.Write(user.name);
                bw.Write(user.height);
                bw.Write(user.evaluated);
            }

            //better to use a new FileStream to read from the file to avoid having to search and move the cursor
            FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (BinaryReader br = new BinaryReader(fsRead))
            {
                //Binaryreader has various methods specific to read each data type
                //these will read and return the stored data in the file and move the cursor same as number of bytes used by that data type
                //if an attempt is made to read the WRONG datatype, as in, one that is not stored in the file, the Read method will throw an error
                //or if an attempt is made to read beyond the file's end
                var retrievedUser = new
                {
                    age = br.ReadInt32(),
                    weight = br.ReadSingle(),
                    name = br.ReadString(),
                    height = br.ReadDecimal(),
                    evaluated = br.ReadBoolean(),
                };

                Console.WriteLine($"Retrieved user data from BinaryWriter storage file: " +
                    $"{retrievedUser.age} {retrievedUser.weight} {retrievedUser.name} {retrievedUser.height} {retrievedUser.evaluated}");
            }
        }

        //serialization is the process of transforming and storing runtime data into another format
        //that can be retrieved and deserealized into their original runtime data formats
        //Json is a format used to encode data into readable key/value pairs 
        //Json is often used with APIs, and comunicatins between frontend/backend on online apps
        //Json can also be used to pass data between applications written in different programming languages
        //internally the JSON data looks like this:
        //{"Id":1,"Name":"Juan","City":"Fake Town","StreetAddress":"123 street"}
        private static void JsonSerialization()
        {
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\jsonSerialized.txt";
            Customer cs = new Customer(1, "Juan", "Fake Town", "123 street");

            //note that all the properties in the Customer class are public
            //if a property is not accessible due to access permission level,
            //JsonSerializer will omit it from the serialized JSON string, and consequently what gets written to the file.
            //fields by default are not included, can be included with Serialize() options second param
            string jsonSerialized = JsonSerializer.Serialize(cs);
            Console.WriteLine($"runtime Customer object serialized into JSON string: {jsonSerialized}");
            File.WriteAllText(path, jsonSerialized);


            Customer? jsonDeserialized;
            
            //while the FileStream is used by the JsonSerializer.Deserialize<T>() method
            //to read the file, it then automatically calls Dispose() at the end of the using construct,
            //which in turn calls Close() on the file connection
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                //the Type of the object that the JSON data will be deserealized into must have matching properties to the data being deserealized
                jsonDeserialized = JsonSerializer.Deserialize<Customer>(fs);
            }

            if (jsonDeserialized is not null)
            {
                Console.WriteLine($"data retrived from JSON serialized file: Id: {jsonDeserialized.Id} Name: {jsonDeserialized.Name} City: {jsonDeserialized.City} StreetAddress: {jsonDeserialized.StreetAddress}");
            }
        }

        private static void XmlSerialization()
        {
            //serialization is the process of transforming and storing runtime data into another format
            //that can be retrieved and deserealized into their original runtime data formats
            //eXtensible Markup Language is a format that is human readable and machine readable
            //XML resembles HTML in the use of opening and closing tags and nesting tags within each other to create "objects"
            //even though XML is older than JSON it can still be used to serialize complex objects, with nested lists and such
            string path = $@"C:\Users\usuario\source\repos\c#Course\Section27\FileAndDirectory\practiceFiles\xmlSerialized.txt";
            Customer cs = new Customer(1, "Juan", "Fake Town", "123 street");

            //in order to serialize a class with XML, the class MUST have a parameterless constructor, even if it is private
            //the reason for this is because XmlSerializer will create an instance of that class and populate the properties while reading the XML file
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));
            
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                //the XmlSerializer.Serialize() method uses the Stream from FileStream to directly store the
                //xml serialized data into the file, unlike JSON it does not return a string version in runtime
                xmlSerializer.Serialize(fs, cs);
            }

            Customer? retrievedCustomer;

            //note that the using construct can create a new filestream with the same name as the one above
            //since its scope is only within the using block
            //internally the XML file looks like this:
            //<?xml version="1.0" encoding="utf-8"?>
            //< Customer xmlns: xsi = "http://www.w3.org/2001/XMLSchema-instance" xmlns: xsd = "http://www.w3.org/2001/XMLSchema" >
            //  < Id > 1 </ Id >
            //  < Name > Juan </ Name >
            //  < City > Fake Town </ City >
            //  < StreetAddress > 123 street </ StreetAddress >
            //</ Customer >
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                //the XmlSerializer.Deserialize() method does not contain an overload with a generic class type like JSON serializer does
                //and so the resulting object must be typecast
                retrievedCustomer = xmlSerializer.Deserialize(fs) as Customer;
            }

            if (retrievedCustomer is not null)
            {
                Console.WriteLine($"data deserialized from XML: Id: {retrievedCustomer.Id} Name: {retrievedCustomer.Name} City: {retrievedCustomer.City} StreetAddress: {retrievedCustomer.StreetAddress}");
            }
        }
    }
}