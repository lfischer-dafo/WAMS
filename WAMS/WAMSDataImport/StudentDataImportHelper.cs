using ExcelDataReader;

namespace WAMSDataImport
{
   public class StudentDataImportHelper
   {

      public StudentDataImportHelper() { }

      public void ReadXLSX()
      {
         string filePath = "C:\\Users\\leonf\\Downloads\\Dummy_Schuelerdaten.xlsx";
         using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
         {
            // Auto-detect format, supports:
            //  - Binary Excel files (2.0-2003 format; *.xls)
            //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
               // Choose one of either 1 or 2:

               // 1. Use the reader methods
               do
               {
                  while (reader.Read())
                  {
                     reader.
                  }
               } while (reader.NextResult());

               // 2. Use the AsDataSet extension method

               // The result of each spreadsheet is in result.Tables
            }
         }
      }

   }
}
