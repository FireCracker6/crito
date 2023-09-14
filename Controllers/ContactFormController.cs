using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Crito.Controllers
{
    public class ContactFormController : Controller
    {
        [HttpPost("ContactForm/SubmitForm")]
        public ActionResult SubmitForm(string name, string email, string message)
        {
            string connectionString = "Data Source=|DataDirectory|/Umbraco.sqlite.db;Cache=Shared;Foreign Keys=True;Pooling=True";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS ContactMessages (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Email TEXT NOT NULL,
                    MessageText TEXT NOT NULL
                )";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var cmd = new SqliteCommand(connection.ToString()))
                {
                    cmd.CommandText = "INSERT INTO ContactMessages (Name, Email, MessageText) VALUES (@name, @email, @message)";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "Your message has been submitted successfully!";
            return RedirectToAction("Contact", "Home"); // Assuming your contact page is the "Contact" action of "Home" controller
        }
    }
}
