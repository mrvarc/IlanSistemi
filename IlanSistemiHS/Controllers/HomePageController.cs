using IlanSistemiHS.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using IlanSistemiHS.ViewModels;
using System.Collections.Generic;

namespace IlanSistemiHS.Controllers
{
	public class HomePageController : Controller
	{
		public IActionResult Index()
		{
			List<HomePageVM> list = new List<HomePageVM>();
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT e.Id, e.Description, e.Price, e.Currency, e.ImageUrl, c.Name as CategoryName, e.PublishDate FROM Editors e LEFT JOIN Categories c ON c.Id=e.CategoryId ORDER BY e.PublishDate DESC", conn);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				list.Add(new HomePageVM {
					Id = (int)dr["Id"],
					Description = (string)dr["Description"], 
					Price = (decimal)dr["Price"], 
					Currency = (string)dr["Currency"], 
					ImageUrl = (string)dr["ImageUrl"], 
					CategoryName = (string)dr["CategoryName"], 
					PublishDate = (DateTime)dr["PublishDate"] 
				});
			}
			conn.Close();
			return View(list);
		}
	}
}
