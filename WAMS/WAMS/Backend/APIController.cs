using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace WAMS.Backend
{

	[Route("api")]
	[ApiController]
	public class APIController : ControllerBase
	{
		private static int counter = 1;

		[HttpGet]
		[Route("GetAllUser")]
		public ActionResult GetAllUser()
		{
			string search_value = HttpContext.Request.Query["search[value]"].ToString();
			string start = HttpContext.Request.Query["start"].ToString();
			string length = HttpContext.Request.Query["length"].ToString();

			string sortingColum = HttpContext.Request.Query["order[0][column]"].ToString(); //
			string sorting = HttpContext.Request.Query["order[0][dir]"].ToString(); //asc / desc

			//foreach(var search in HttpContext.Request.Query)
			//{
			//    Console.WriteLine("----======----" + search);
			//}

			// Getting all Customer data    
			dataTableSQLFeedback tableSQLFeedback = Database.GetQuestionsSeite(start, length, search_value, sortingColum, sorting);
			int _recordsTotal = tableSQLFeedback.TotalCount;
			int _recordsFiltered = tableSQLFeedback.SearchCount;

			Debug.WriteLine("-TotalCount--" + tableSQLFeedback.TotalCount);
			Debug.WriteLine("-SearchCount--" + tableSQLFeedback.SearchCount);
			Debug.WriteLine("-_recordsFiltered--" + _recordsFiltered);

			if (String.IsNullOrEmpty(search_value)) {
				_recordsFiltered = _recordsTotal;
			}

			Debug.WriteLine("-_recordsFiltered--" + _recordsFiltered);
			ContentResult contentResult = Content(JsonSerializer.Serialize(new { draw = counter, recordsTotal = _recordsTotal, recordsFiltered = _recordsFiltered, data = tableSQLFeedback.JsonQuestion }));
			counter = counter + 1;
			return contentResult;
		}

		[HttpGet]
		[Route("getAvatarImage")]
		public ActionResult getImage(string url)
		{
			Debug.WriteLine("getImage=" + url);
			string location = @"\\srvshare\" + url;
			if (System.IO.File.Exists(location)) {
				var image = System.IO.File.OpenRead(@"\\srvshare\" + url);
				return File(image, "image/jpeg");
			} else {
				return Content("File not found");
			}
		}
	}
}
