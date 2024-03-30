using IlanSistemiHS.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using IlanSistemiHS.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace IlanSistemiHS.Controllers
{
	public class EditorController : Controller
	{
		// EDITOR INDEX GÖRÜNÜMÜ
		public IActionResult Index()
		{
			List<EditorVM> list = new List<EditorVM>();
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT e.Id, e.Name, e.Description, e.Price, e.Currency, e.ImageUrl, c.Name as CategoryName, e.PublishDate FROM Editors e JOIN Categories c ON c.Id=e.CategoryId", conn);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				list.Add(new EditorVM { Id = (int)dr["Id"], Name = (string)dr["Name"], Description = (string)dr["Description"], Price = (decimal)dr["Price"], Currency = (string)dr["Currency"], ImageUrl = (string)dr["ImageUrl"], CategoryName = (string)dr["CategoryName"], PublishDate = (DateTime)dr["PublishDate"] });
			}
			conn.Close();
			return View(list);
		}




		// DELETE-SİL BUTONU
		public IActionResult Delete(int id)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("DELETE FROM Editors WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", id);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			return RedirectToAction("Index");
		}




		public IActionResult Add()
		{
			ViewData["categories"] = GetAllSelectListItem("Categories");
			return View();
		}



		[HttpPost]
		public IActionResult Add(Models.Editor editor)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("INSERT INTO Editors (Name, Description, Price, Currency, ImageUrl, CategoryId, PublishDate) OUTPUT inserted.Id values(@name, @description, @price, @currency, @imageUrl, @categoryId, @publishDate)", conn);
			cmd.Parameters.AddWithValue("@name", editor.Name);
			cmd.Parameters.AddWithValue("@description", editor.Description);
			cmd.Parameters.AddWithValue("@price", editor.Price);
			cmd.Parameters.AddWithValue("@currency", editor.Currency);
			cmd.Parameters.AddWithValue("@imageUrl", editor.ImageUrl);
			cmd.Parameters.AddWithValue("@categoryId", editor.CategoryId);
			cmd.Parameters.AddWithValue("@publishDate", editor.PublishDate);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			conn.Close();
			return RedirectToAction("Index");
		}




		public IActionResult Update(int id)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT * FROM Editors WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", id);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			dr.Read();
			Models.Editor editor = new Models.Editor { Name = (string)dr["Name"], Description = (string)dr["Description"], Price = (decimal)dr["Price"], Currency = (string)dr["Currency"], ImageUrl = (string)dr["ImageUrl"], CategoryId = (int)dr["CategoryId"], PublishDate = (DateTime)dr["PublishDate"] };
			conn.Close();
			dr.Close();
			ViewData["categories"] = GetAllSelectListItem("Categories");
			return View(editor);
		}




		public List<SelectListItem> GetAllSelectListItem(string tableName)
		{
			List<SelectListItem> list = new List<SelectListItem>();
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT Id,Name FROM " + tableName, conn);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				list.Add(new SelectListItem { Text = (string)dr["Name"], Value = dr["Id"].ToString() });
			}
			conn.Close();
			dr.Close();
			return list;

		}




		[HttpPost]
		public IActionResult Update(Editor editor)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("UPDATE Editors SET Name=@name, Description=@description, Price=@price, Currency=@currency, ImageUrl=@imageUrl, CategoryId=@categoryId, PublishDate=@publishDate WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", editor.Id);
			cmd.Parameters.AddWithValue("@name", editor.Name);
			cmd.Parameters.AddWithValue("@description", editor.Description);
			cmd.Parameters.AddWithValue("@price", editor.Price);
			cmd.Parameters.AddWithValue("@currency", editor.Currency);
			cmd.Parameters.AddWithValue("@imageUrl", editor.ImageUrl);
			cmd.Parameters.AddWithValue("@categoryId", editor.CategoryId);
			cmd.Parameters.AddWithValue("@publishDate", editor.PublishDate);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			return RedirectToAction("Index");
		}


		public IActionResult KategoriFiltre(string category)
		{
			List<EditorVM> list = new List<EditorVM>();
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT e.Id, e.Name, e.Description, e.Price, e.Currency, e.ImageUrl, c.Name as CategoryName, e.PublishDate FROM Editors e JOIN Categories c ON c.Id=e.CategoryId WHERE c.Name = @category", conn);
			cmd.Parameters.AddWithValue("@category", category);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				list.Add(new EditorVM { Id = (int)dr["Id"], Name = (string)dr["Name"], Description = (string)dr["Description"], Price = (decimal)dr["Price"], Currency = (string)dr["Currency"], ImageUrl = (string)dr["ImageUrl"], CategoryName = (string)dr["CategoryName"], PublishDate = (DateTime)dr["PublishDate"] });
			}
			conn.Close();
			return View(list);
		}





		public IActionResult Details(int id)
		{
			DetailsVM model = null; // Tek bir ilan için model
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT e.Description, e.Price, e.Currency, e.ImageUrl, c.Name as CategoryName, e.PublishDate FROM Editors e LEFT JOIN Categories c ON c.Id=e.CategoryId WHERE e.Id=@Id", conn);
			// Parametre olarak ilanın ID'sini ekliyoruz
			cmd.Parameters.AddWithValue("@Id", id);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
			{
				model = new DetailsVM
				{
					Description = dr["Description"].ToString(),
					Price = (decimal)dr["Price"],
					Currency = dr["Currency"].ToString(),
					ImageUrl = dr["ImageUrl"].ToString(),
					CategoryName = dr["CategoryName"].ToString(),
					PublishDate = (DateTime)dr["PublishDate"]
				};
			}
			conn.Close();

			return View(model);



		}

	}
}
