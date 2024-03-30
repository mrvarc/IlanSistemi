using IlanSistemiHS.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using IlanSistemiHS.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IlanSistemiHS.Controllers
{
	public class CategoryController : Controller
	{
		// 1. KISIM INDEX - KATEGORİ \\
		public IActionResult Index()
		{
			List<Category> list = new List<Category>();
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", conn);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				list.Add(new Category { Id = (int)dr["Id"], Name = (string)dr["Name"], Smallimg = (string)dr["Smallimg"] });
			}
			dr.Close();
			conn.Close();
			return View(list);
		}


		// 2. KISIM DELETE - SİLME İŞLEMİ \\
		public IActionResult Delete(int id)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", id);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			return RedirectToAction("Index");
		}



		// 3. KISIM ADD - EKLEME İŞLEMİ \\

		// İlk Add metodu GET işlemi için yazılır. ADD butonuna tıklandığında yeni kategori eklemek için boş bir ekranın açılmasını sağlar. HttpGet kodunun yazılmasına gerek yoktur.

		// İkinci Add metodu [HttpPost] ile belirtildiği gibi Add/Ekleme işlemini yaptıktan sonra Kaydet dediğimizde veritabanına kaydetmek/post etmek için yazılır. Kayıt işleminden sonra RedirectToAction işlemiyle belirtilen sayfaya yönlendirme sağlanır.
		public IActionResult Add()
		{
			return View();
		}



		[HttpPost]
		public IActionResult Add(Category category)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("INSERT INTO Categories(Name, Smallimg) VALUES(@name, @smallimg)", conn);
			cmd.Parameters.AddWithValue("@name", category.Name);
			cmd.Parameters.AddWithValue("@smallimg", category.Smallimg);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			return RedirectToAction("Index");
		}




		//4. KISIM UPDATE - GÜNCELLEME İŞLEMİ\\

		// ADD işleminde olduğu gibi kaydı güncellemek için açılan ekranın görüntülenmesi ve ilgili kaydın ekrana gelmesi için yazılmaktadır. HttpGet kodunun yazılmasına gerek yoktur.

		// İkinci Update metodu [HttpPost] ile güncelleme işlemini yaptıktan sonra yeni kategoriyi veritabanına kaydetmek/post etmek için yazılır.

		public IActionResult Update(int id)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("SELECT * FROM Categories WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", id);
			conn.Open();
			SqlDataReader dr = cmd.ExecuteReader();
			dr.Read();
			Category category = new Category { Id = (int)dr["Id"], Name = (string)dr["Name"], Smallimg = (string)dr["Smallimg"] };
			conn.Close();
			return View(category);
		}



		[HttpPost]
		public IActionResult Update(Category category)
		{
			SqlConnection conn = Db.Conn();
			SqlCommand cmd = new SqlCommand("UPDATE Categories SET Name=@name WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@name", category.Name);
			cmd.Parameters.AddWithValue("@id", category.Id);
			cmd.Parameters.AddWithValue("@smallimg", category.Smallimg);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			return RedirectToAction("Index");
		}
	}
}