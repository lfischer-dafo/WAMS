using ExcelDataReader;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using WAMS.Components.Model;

namespace WAMS.Backend.Data
{
   public static class ImportInteropHelper
   {

      public static List<Student> UserFromStudent()
      {
         using (var context = new ImportDbContext())
         {
            string filePath = "C:\\Users\\leonf\\Downloads\\Dummy_Schuelerdaten.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            List<Student> students = new List<Student>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
               using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
               {
                  DataRowCollection dataRows = reader.AsDataSet().Tables[0].Rows;
                  for (int i = 1; i < reader.AsDataSet().Tables[0].Rows.Count; i++)
                  {
                     Student student = new Student();
                     student.FirstName = dataRows[i].ItemArray[8].ToString();
                     student.LastName = dataRows[i].ItemArray[9].ToString();
                     var classes = context.Classes.Where(x => x.Name == dataRows[i].ItemArray[3].ToString());
                     if (classes.Any())
                     {
                        student.Class = classes.FirstOrDefault();
                        student.ClassId = classes.FirstOrDefault().ClassId;
                        classes.FirstOrDefault().Students.Add(student);
                     }
                     SHA256 SHA256 = SHA256.Create();
                     student.Password = SHA256.ComputeHash(Encoding.ASCII.GetBytes((student.FirstName + "." + student.LastName + "." + student.Class.Name.ToString())));
                     students.Add(student);
                  }
               }
            }
            return students;

         }
      }

   }

}

