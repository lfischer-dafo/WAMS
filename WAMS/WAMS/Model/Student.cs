using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;
using WAMS.Backend.Data;
using WAMSDataImport.StudentData;

namespace WAMS.Components.Model
{
   public class Student
   {
      [Key]
      public int UserId { get; set; }
      public string? Username { get; set; }
      public byte[]? Password { get; set; } 
      public string? FirstName { get; set; }
      public string? LastName { get; set; }
      public DateTime? LastLogin { get; set; }
      public Status Status { get; set; } = Status.Present;
      public Room? Room { get; set; }
      public int RoomId { get; set; }
      public string? MailAdress { get; set; }
      public Class? Class { get; set; }
      public int ClassId { get; set; }

   }


   public enum Status
   {
      Missing = 0,
      Sick = 1,
      Present = 2,
   }
}